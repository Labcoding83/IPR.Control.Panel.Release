﻿using System;
using System.IO;
using System.Runtime.InteropServices;

namespace DellFanManagement.DellSmbiozBzhLib.PInvoke
{
    static class ServiceMethods
    {
        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="hObject">A valid handle to an open object.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/handleapi/nf-handleapi-closehandle"/>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        /// <summary>
        /// Closes a handle to a service control manager or service object.
        /// </summary>
        /// <param name="scObject">A handle to the service control manager object or the service object to
        /// close.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-closeservicehandle"/>
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool CloseServiceHandle(IntPtr scObject);

        /// <summary>
        /// Sends a control code to a service.
        /// </summary>
        /// <param name="hService">A handle to the service.</param>
        /// <param name="control">Control code to send.</param>
        /// <param name="serviceStatus"></param>
        /// <returns></returns>
        /// <seealso cref="http://www.pinvoke.net/default.aspx/advapi32.ControlService"/>
        /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-controlservice"/>
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool ControlService(IntPtr hService, ServiceControl control, ref ServiceStatus serviceStatus);

        /// <summary>
        /// Creates or opens a file or I/O device.
        /// </summary>
        /// <param name="filename">The name of the file or device to be created or opened.</param>
        /// <param name="desiredAccess">The requested access to the file or device.</param>
        /// <param name="share">The requested sharing mode of the file or device.</param>
        /// <param name="securityAttributes">A pointer to a SECURITY_ATTRIBUTES structure that contains two separate but
        /// related data members: an optional security descriptor, and a Boolean value that determines whether the
        /// returned handle can be inherited by child processes.</param>
        /// <param name="creationDisposition">An action to take on a file or device that exists or does not
        /// exist.</param>
        /// <param name="flagsAndAttributes">The file or device attributes and flags.</param>
        /// <param name="templateFile">A valid handle to a template file with the GENERIC_READ access right.</param>
        /// <returns>If the function succeeds, the return value is an open handle to the specified file, device, named
        /// pipe, or mail slot.  If the function fails, the return value is InvalidHandleValue.</returns>
        /// <seealso cref="http://pinvoke.net/default.aspx/kernel32/CreateFile.html"/>
        /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilew"/>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateFile(
            [MarshalAs(UnmanagedType.LPTStr)] string filename,
            [MarshalAs(UnmanagedType.U4)] FileAccess desiredAccess,
            [MarshalAs(UnmanagedType.U4)] FileShare share,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes,
            IntPtr templateFile
        );

        /// <summary>
        /// Creates a service object and adds it to the specified service control manager database.
        /// </summary>
        /// <param name="hSCManager">A handle to the service control manager database.</param>
        /// <param name="serviceName">The name of the service to install.</param>
        /// <param name="displayName">The display name to be used by user interface programs to identify the
        /// service.</param>
        /// <param name="desiredAccess">The access to the service.</param>
        /// <param name="serviceType">The service type.</param>
        /// <param name="startType">The service start options.</param>
        /// <param name="errorControl">The severity of the error, and action taken, if this service fails to
        /// start.</param>
        /// <param name="binaryPathName">The fully qualified path to the service binary file.</param>
        /// <param name="loadOrderGroup">The names of the load ordering group of which this service is a member.</param>
        /// <param name="tagId">A pointer to a variable that receives a tag value that is unique in the group specified
        /// in the lpLoadOrderGroup parameter.</param>
        /// <param name="dependencies">A pointer to a double null-terminated array of null-separated names of services
        /// or load ordering groups that the system must start before this service.</param>
        /// <param name="serviceStartName">The name of the account under which the service should run.</param>
        /// <param name="password">The password to the account name specified by the serviceStartName parameter.</param>
        /// <returns>If the function succeeds, the return value is a handle to the service.  If the function fails, the
        /// return value is NULL.</returns>
        /// <seealso cref="https://www.pinvoke.net/default.aspx/advapi32.createservice"/>
        /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-createservicew"/>
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateService(
            IntPtr hSCManager,
            [MarshalAs(UnmanagedType.LPTStr)] string serviceName,
            [MarshalAs(UnmanagedType.LPTStr)] string displayName,
            [MarshalAs(UnmanagedType.U4)] ServiceAccess desiredAccess,
            [MarshalAs(UnmanagedType.U4)] ServiceType serviceType,
            [MarshalAs(UnmanagedType.U4)] ServiceStartType startType,
            [MarshalAs(UnmanagedType.U4)] ServiceErrorControl errorControl,
            [MarshalAs(UnmanagedType.LPTStr)] string binaryPathName,
            [MarshalAs(UnmanagedType.LPTStr)] string loadOrderGroup,
            IntPtr tagId,
            [MarshalAs(UnmanagedType.LPTStr)] string dependencies,
            [MarshalAs(UnmanagedType.LPTStr)] string serviceStartName,
            [MarshalAs(UnmanagedType.LPTStr)] string password
        );

        /// <summary>
        /// Marks the specified service for deletion from the service control manager database.
        /// </summary>
        /// <param name="hService">A handle to the service.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        /// <seealso cref="http://www.pinvoke.net/default.aspx/advapi32.deleteservice"/>
        /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-deleteservice"/>
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool DeleteService(IntPtr hService);

        /// <summary>
        /// Sends a control code directly to a specified device driver, causing the corresponding device to perform the
        /// corresponding operation.
        /// </summary>
        /// <param name="hDevice">A handle to the device on which the operation is to be performed.</param>
        /// <param name="IoControlCode">The control code for the operation.</param>
        /// <param name="InBuffer">A pointer to the input buffer that contains the data required to perform the
        /// operation.</param>
        /// <param name="nInBufferSize">The size of the input buffer, in bytes.</param>
        /// <param name="OutBuffer">A pointer to the output buffer that is to receive the data returned by the
        /// operation.</param>
        /// <param name="nOutBufferSize">The size of the output buffer, in bytes.</param>
        /// <param name="bytesReturned">A pointer to a variable that receives the size of the data stored in the output
        /// buffer, in bytes.</param>
        /// <param name="overlapped">A pointer to an OVERLAPPED structure.  (Not used in this application.)</param>
        /// <returns></returns>
        /// <seealso cref="https://www.pinvoke.net/default.aspx/kernel32.deviceiocontrol"/>
        /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/ioapiset/nf-ioapiset-deviceiocontrol"/>
        [DllImport("Kernel32.dll", SetLastError = false, CharSet = CharSet.Auto)]
        public static extern bool DeviceIoControl(
            IntPtr hDevice,
            int IoControlCode,
            ref SmbiosPackage InBuffer,
            int nInBufferSize,
            ref SmbiosPackage OutBuffer,
            int nOutBufferSize,
            ref uint bytesReturned,
            IntPtr overlapped
        );

        /// <summary>
        /// Establishes a connection to the service control manager on the specified computer and opens the specified
        /// service control manager database.
        /// </summary>
        /// <param name="machineName">The name of the target computer.</param>
        /// <param name="databaseName">The name of the service control manager database.</param>
        /// <param name="desiredAccess">The access to the service control manager.</param>
        /// <returns>If the function succeeds, the return value is a handle to the specified service control manager
        /// database. If the function fails, the return value is NULL.</returns>
        /// <seealso cref="http://www.pinvoke.net/default.aspx/advapi32/OpenSCManager.html"/>
        /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-openscmanagerw"/>
        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr OpenSCManager(
            [MarshalAs(UnmanagedType.LPTStr)] string machineName,
            [MarshalAs(UnmanagedType.LPTStr)] string databaseName,
            [MarshalAs(UnmanagedType.U4)] ServiceAccess desiredAccess
        );

        /// <summary>
        /// Opens an existing service.
        /// </summary>
        /// <param name="scManager">A handle to the service control manager database.</param>
        /// <param name="serviceName">The name of the service to be opened.</param>
        /// <param name="desiredAccess">The access to the service.</param>
        /// <returns>If the function succeeds, the return value is a handle to the service. If the function fails, the
        /// return value is NULL.</returns>
        /// <seealso cref="http://www.pinvoke.net/default.aspx/advapi32.openservice"/>
        /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-openservicew"/>
        [DllImport("advapi32.dll", EntryPoint = "OpenServiceW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr OpenService(
            IntPtr scManager,
            [MarshalAs(UnmanagedType.LPTStr)] string serviceName,
            [MarshalAs(UnmanagedType.U4)] ServiceAccess desiredAccess
        );

        /// <summary>
        /// Retrieves the optional configuration parameters of the specified service.
        /// </summary>
        /// <param name="hService">A handle to the service.</param>
        /// <param name="buffer">A pointer to the buffer that receives the service configuration information.</param>
        /// <param name="bufferSize">The size of the structure pointed to by the lpBuffer parameter, in bytes.</param>
        /// <param name="bytesNeeded">A pointer to a variable that receives the number of bytes required to store the
        /// configuration information, if the function fails with ERROR_INSUFFICIENT_BUFFER.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-queryserviceconfigw"/>
        [DllImport("advapi32.dll", EntryPoint = "QueryServiceConfigW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool QueryServiceConfig(
            IntPtr hService,
            IntPtr buffer,
            int bufferSize,
            out int bytesNeeded
        );

        /// <summary>
        /// Starts a service.
        /// </summary>
        /// <param name="hService">A handle to the service.</param>
        /// <param name="numServiceArgs">The number of strings in the serviceArgVectors array.</param>
        /// <param name="serviceArgVectors">The null-terminated strings to be passed to the ServiceMain function for the
        /// service as arguments.</param>
        /// <returns></returns>
        /// <seealso cref="http://pinvoke.net/default.aspx/advapi32.StartService"/>
        /// <seealso cref="https://docs.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-startservicew"/>
        [DllImport("advapi32", SetLastError = true)]
        public static extern bool StartService(
            IntPtr hService,
            int numServiceArgs,
            string[] serviceArgVectors
        );

        /// <summary>
        /// Represents an invalid device/file handle.
        /// </summary>
        public static readonly IntPtr InvalidHandleValue = new IntPtr(-1);
    }
}
