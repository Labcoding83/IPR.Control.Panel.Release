﻿// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at http://mozilla.org/MPL/2.0/.
// Copyright (C) LibreHardwareMonitor and Contributors.
// Partial Copyright (C) Michael Möller <mmoeller@openhardwaremonitor.org> and Contributors.
// All Rights Reserved.

using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace IPR.Hardware.Tools.Hardware.CPU
{
    internal class CpuLoad
    {
        private readonly CpuId[][] _cpuid;
        private long[] _idleTimes;
        private float[] _threadLoads;
        private long[] _totalTimes;

        public CpuLoad(CpuId[][] cpuid)
        {
            _cpuid = cpuid;
            _threadLoads = new float[cpuid.Sum(x => x.Length)];
            try
            {
                GetTimes(out _idleTimes, out _totalTimes);
            }
            catch (Exception)
            {
                _idleTimes = null;
                _totalTimes = null;
            }

            if (_idleTimes != null)
                IsAvailable = true;
        }

        public bool IsAvailable { get; }

        private static bool GetTimes(out long[] idle, out long[] total)
        {
            idle = null;
            total = null;
            try
            {
                if (Software.OperatingSystem.IsUnix)
                {
                    var cpuInfo = File.ReadAllLines("/proc/stat")
                        .Where(x => x.StartsWith("cpu"))
                        .Skip(1)
                        .ToList();

                    var cpuInfoColumns = cpuInfo
                        .Select(x => x.Split(" "))
                        .ToList();

                    total = cpuInfoColumns
                        .Select(x =>
                            {
                                //return long.Parse(x[2]);
                                 try
                                 {
                                    return (long.Parse(x[1]) + long.Parse(x[3]) + long.Parse(x[4]));
                                 }
                                 catch (Exception ex)
                                 {
                                     return 0;
                                 }
                            }

                        ).ToArray();

                    
                    idle = cpuInfoColumns.Select(x => long.Parse(x[1]) +  long.Parse(x[3])).ToArray();
                }
                else
                {
                    Interop.NtDll.SYSTEM_PROCESSOR_PERFORMANCE_INFORMATION[] information =
                        new Interop.NtDll.SYSTEM_PROCESSOR_PERFORMANCE_INFORMATION[64];
                    int size = Marshal.SizeOf(typeof(Interop.NtDll.SYSTEM_PROCESSOR_PERFORMANCE_INFORMATION));

                    

                    if (Interop.NtDll.NtQuerySystemInformation(
                            Interop.NtDll.SYSTEM_INFORMATION_CLASS.SystemProcessorPerformanceInformation,
                            information,
                            information.Length * size,
                            out IntPtr returnLength) != 0)
                    {
                        return false;
                    }

                    idle = new long[(int)returnLength / size];
                    total = new long[(int)returnLength / size];

                    for (int i = 0; i < idle.Length; i++)
                    {
                        idle[i] = information[i].IdleTime;
                        total[i] = information[i].KernelTime + information[i].UserTime;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public float GetTotalLoad()
        {
            return _threadLoads.Average();
        }

        public float GetThreadLoad(int thread)
        {
            return _threadLoads[thread];
        }

        public void Update()
        {
            if (_idleTimes == null)
                return;

            if (!GetTimes(out long[] newIdleTimes, out long[] newTotalTimes))
                return;

            if (newIdleTimes == null)
                return;

            if (Software.OperatingSystem.IsUnix)
            {
                int i = 0;
                for (i = 0; i < _threadLoads.Length && i < _idleTimes.Length && i < newIdleTimes.Length; i++)
                {
                    float load = ((newIdleTimes[i]) - (float)(_idleTimes[i]))/
                                 (float)(newTotalTimes[i] - _totalTimes[i]) 
                                 * 100f;
                    _threadLoads[i] = load;
                }
            }
            else
            {
                int i = 0;
                for (i = 0; i < _threadLoads.Length && i < _idleTimes.Length && i < newIdleTimes.Length; i++)
                {
                    float idle = (newIdleTimes[i] - _idleTimes[i]) / (float)(newTotalTimes[i] - _totalTimes[i]);
                    _threadLoads[i] = 100f * (1.0f - Math.Min(idle, 1.0f));
                }
            }

            _totalTimes = newTotalTimes;
            _idleTimes = newIdleTimes;
        }
    }
}
