Option Strict On
Option Explicit On

Namespace Extensions
   Module ProcessExtension
      <Extension>
      Public Function ExitCodeResolved(pr As Process) As ProcessExitCodes
         If [Enum].IsDefined(GetType(ProcessExitCodes), pr.ExitCode) Then
            Return DirectCast(pr.ExitCode, ProcessExitCodes)
         Else
            Return ProcessExitCodes.UNDEFINED
         End If
      End Function

        <Extension, System.Diagnostics.DebuggerStepThrough()>
        Public Function ShowMessage(owner As Form, mensaje As String) As DialogResult

            If owner Is Nothing OrElse owner.IsDisposed Then
                Return MessageBox.Show(mensaje, owner.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Return MessageBox.Show(owner, mensaje, owner.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Function

      <Extension>
      Public Function ExitCodeSuccess(pr As Process) As Boolean
         Dim e As ProcessExitCodes = pr.ExitCodeResolved()
         Return e = ProcessExitCodes.ERROR_SUCCESS Or
                e = ProcessExitCodes.ERROR_SUCCESS_REBOOT_INITIATED Or
                e = ProcessExitCodes.ERROR_SUCCESS_REBOOT_REQUIRED 
      End Function

      <Extension>
      Public Function ExitCodeToString(pr As Process) As String
         Dim e As ProcessExitCodes = pr.ExitCodeResolved()
         If e = ProcessExitCodes.UNDEFINED Then
            Return String.Format("Exit Code: {0}", pr.ExitCode)
         Else
            Return String.Format("Exit Code: {0} - {1}: {2}", pr.ExitCode, e.ToString(), GetEnumString(e))
         End If
      End Function

      Public Function GetEnumString(value As System.Enum) As String
         Dim fi As FieldInfo = value.GetType().GetField(value.ToString())
         Dim attributes As DescriptionAttribute() = DirectCast(fi.GetCustomAttributes(GetType(DescriptionAttribute), False), DescriptionAttribute())
         If attributes IsNot Nothing AndAlso attributes.Length > 0 Then
            Return attributes(0).Description
         Else
            Return value.ToString()
         End If
      End Function

   End Module

   Public Enum ProcessExitCodes
      UNDEFINED = -1
      <Description("The action completed successfully.")> ERROR_SUCCESS = 0
      <Description("The data is invalid.")> ERROR_INVALID_DATA = 13
      <Description("One of the parameters was invalid.")> ERROR_INVALID_PARAMETER = 87
      <Description("This value is returned when a custom action attempts to call a function that cannot be called from custom actions. The function returns the value ERROR_CALL_NOT_IMPLEMENTED. Available beginning with Windows Installer version 3.0.")> ERROR_CALL_NOT_IMPLEMENTED = 120
      <Description("If Windows Installer determines a product may be incompatible with the current operating system, it displays a dialog box informing the user and asking whether to try to install anyway. This error code is returned if the user chooses not to try the installation.")> ERROR_APPHELP_BLOCK = 1259
      <Description("The Windows Installer service could not be accessed. Contact your support personnel to verify that the Windows Installer service is properly registered.")> ERROR_INSTALL_SERVICE_FAILURE = 1601
      <Description("The user cancels installation.")> ERROR_INSTALL_USEREXIT = 1602
      <Description("A fatal error occurred during installation.")> ERROR_INSTALL_FAILURE = 1603
      <Description("Installation suspended, incomplete.")> ERROR_INSTALL_SUSPEND = 1604
      <Description("This action is only valid for products that are currently installed.")> ERROR_UNKNOWN_PRODUCT = 1605
      <Description("The feature identifier is not registered.")> ERROR_UNKNOWN_FEATURE = 1606
      <Description("The component identifier is not registered.")> ERROR_UNKNOWN_COMPONENT = 1607
      <Description("This is an unknown property.")> ERROR_UNKNOWN_PROPERTY = 1608
      <Description("The handle is in an invalid state.")> ERROR_INVALID_HANDLE_STATE = 1609
      <Description("The configuration data for this product is corrupt. Contact your support personnel.")> ERROR_BAD_CONFIGURATION = 1610
      <Description("The component qualifier not present.")> ERROR_INDEX_ABSENT = 1611
      <Description("The installation source for this product is not available. Verify that the source exists and that you can access it.")> ERROR_INSTALL_SOURCE_ABSENT = 1612
      <Description("This installation package cannot be installed by the Windows Installer service. You must install a Windows service pack that contains a newer version of the Windows Installer service.")> ERROR_INSTALL_PACKAGE_VERSION = 1613
      <Description("The product is uninstalled.")> ERROR_PRODUCT_UNINSTALLED = 1614
      <Description("The SQL query syntax is invalid or unsupported.")> ERROR_BAD_QUERY_SYNTAX = 1615
      <Description("The record field does not exist.")> ERROR_INVALID_FIELD = 1616
      <Description("Another installation is already in progress. Complete that installation before proceeding with this install.For information about the mutex, see _MSIExecute Mutex.")> ERROR_INSTALL_ALREADY_RUNNING = 1618
      <Description("This installation package could not be opened. Verify that the package exists and is accessible, or contact the application vendor to verify that this is a valid Windows Installer package.")> ERROR_INSTALL_PACKAGE_OPEN_FAILED = 1619
      <Description("This installation package could not be opened. Contact the application vendor to verify that this is a valid Windows Installer package.")> ERROR_INSTALL_PACKAGE_INVALID = 1620
      <Description("There was an error starting the Windows Installer service user interface. Contact your support personnel.")> ERROR_INSTALL_UI_FAILURE = 1621
      <Description("There was an error opening installation log file. Verify that the specified log file location exists and is writable.")> ERROR_INSTALL_LOG_FAILURE = 1622
      <Description("This language of this installation package is not supported by your system.")> ERROR_INSTALL_LANGUAGE_UNSUPPORTED = 1623
      <Description("There was an error applying transforms. Verify that the specified transform paths are valid.")> ERROR_INSTALL_TRANSFORM_FAILURE = 1624
      <Description("This installation is forbidden by system policy. Contact your system administrator.")> ERROR_INSTALL_PACKAGE_REJECTED = 1625
      <Description("The function could not be executed.")> ERROR_FUNCTION_NOT_CALLED = 1626
      <Description("The function failed during execution.")> ERROR_FUNCTION_FAILED = 1627
      <Description("An invalid or unknown table was specified.")> ERROR_INVALID_TABLE = 1628
      <Description("The data supplied is the wrong type.")> ERROR_DATATYPE_MISMATCH = 1629
      <Description("Data of this type is not supported.")> ERROR_UNSUPPORTED_TYPE = 1630
      <Description("The Windows Installer service failed to start. Contact your support personnel.")> ERROR_CREATE_FAILED = 1631
      <Description("The Temp folder is either full or inaccessible. Verify that the Temp folder exists and that you can write to it.")> ERROR_INSTALL_TEMP_UNWRITABLE = 1632
      <Description("This installation package is not supported on this platform. Contact your application vendor.")> ERROR_INSTALL_PLATFORM_UNSUPPORTED = 1633
      <Description("Component is not used on this machine.")> ERROR_INSTALL_NOTUSED = 1634
      <Description("This patch package could not be opened. Verify that the patch package exists and is accessible, or contact the application vendor to verify that this is a valid Windows Installer patch package.")> ERROR_PATCH_PACKAGE_OPEN_FAILED = 1635
      <Description("This patch package could not be opened. Contact the application vendor to verify that this is a valid Windows Installer patch package.")> ERROR_PATCH_PACKAGE_INVALID = 1636
      <Description("This patch package cannot be processed by the Windows Installer service. You must install a Windows service pack that contains a newer version of the Windows Installer service.")> ERROR_PATCH_PACKAGE_UNSUPPORTED = 1637
      <Description("Another version of this product is already installed. Installation of this version cannot continue. To configure or remove the existing version of this product, use Add/Remove Programs in Control Panel.")> ERROR_PRODUCT_VERSION = 1638
      <Description("Invalid command line argument. Consult the Windows Installer SDK for detailed command-line help.")> ERROR_INVALID_COMMAND_LINE = 1639
      <Description("The current user is not permitted to perform installations from a client session of a server running the Terminal Server role service.")> ERROR_INSTALL_REMOTE_DISALLOWED = 1640
      <Description("The installer has initiated a restart. This message is indicative of a success.")> ERROR_SUCCESS_REBOOT_INITIATED = 1641
      <Description("The installer cannot install the upgrade patch because the program being upgraded may be missing or the upgrade patch updates a different version of the program. Verify that the program to be upgraded exists on your computer and that you have the correct upgrade patch.")> ERROR_PATCH_TARGET_NOT_FOUND = 1642
      <Description("The patch package is not permitted by system policy.")> ERROR_PATCH_PACKAGE_REJECTED = 1643
      <Description("One or more customizations are not permitted by system policy.")> ERROR_INSTALL_TRANSFORM_REJECTED = 1644
      <Description("Windows Installer does not permit installation from a Remote Desktop Connection.")> ERROR_INSTALL_REMOTE_PROHIBITED = 1645
      <Description("The patch package is not a removable patch package. Available beginning with Windows Installer version 3.0.")> ERROR_PATCH_REMOVAL_UNSUPPORTED = 1646
      <Description("The patch is not applied to this product. Available beginning with Windows Installer version 3.0.")> ERROR_UNKNOWN_PATCH = 1647
      <Description("No valid sequence could be found for the set of patches. Available beginning with Windows Installer version 3.0.")> ERROR_PATCH_NO_SEQUENCE = 1648
      <Description("Patch removal was disallowed by policy. Available beginning with Windows Installer version 3.0.")> ERROR_PATCH_REMOVAL_DISALLOWED = 1649
      <Description("The XML patch data is invalid. Available beginning with Windows Installer version 3.0.")> ERROR_INVALID_PATCH_XML = 1650
      <Description("Administrative user failed to apply patch for a per-user managed or a per-machine application that is in advertise state. Available beginning with Windows Installer version 3.0.")> ERROR_PATCH_MANAGED_ADVERTISED_PRODUCT = 1651
      <Description("Windows Installer is not accessible when the computer is in Safe Mode. Exit Safe Mode and try again or try using System Restore to return your computer to a previous state. Available beginning with Windows Installer version 4.0.")> ERROR_INSTALL_SERVICE_SAFEBOOT = 1652
      <Description("Could not perform a multiple-package transaction because rollback has been disabled. Multiple-Package Installations cannot run if rollback is disabled. Available beginning with Windows Installer version 4.5.")> ERROR_ROLLBACK_DISABLED = 1653
      <Description("The app that you are trying to run is not supported on this version of Windows. A Windows Installer package, patch, or transform that has not been signed by Microsoft cannot be installed on an ARM computer.")> ERROR_INSTALL_REJECTED = 1654
      <Description("A restart is required to complete the install. This message is indicative of a success. This does not include installs where the ForceReboot action is run.")> ERROR_SUCCESS_REBOOT_REQUIRED = 3010
   End Enum

End Namespace