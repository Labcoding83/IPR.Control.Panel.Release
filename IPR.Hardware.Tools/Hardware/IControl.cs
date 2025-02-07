﻿// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at http://mozilla.org/MPL/2.0/.
// Copyright (C) LibreHardwareMonitor and Contributors.
// Partial Copyright (C) Michael Möller <mmoeller@openhardwaremonitor.org> and Contributors.
// All Rights Reserved.

namespace IPR.Hardware.Tools.Hardware
{
    public interface IControl : IElement
    {
        ControlType ControlType { get; }
        int Index { get; }
        double Value { get; set; }
        UnitType UnitType { get; }
        /// <summary>
        /// Theoretical minimum <see cref="Value"/>
        /// </summary>
        float MinValue { get; }

        /// <summary>
        /// Theoretical maximum <see cref="Value"/>
        /// </summary>
        float MaxValue { get; }

        double ChangeValue(double value);
        void SetDefaultValue(double value);
    }
}
