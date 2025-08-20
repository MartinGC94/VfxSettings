using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Management.Automation;
using System.Security;

namespace MartinGC94.VfxSettings
{
    internal static class Utils
    {
        internal const string dwmKey = @"Registry::HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM";
        internal const string explorerAdvancedKey = @"Registry::HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";
        internal const string personalizeKey = @"Registry::HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        private const string setSettingsScript = @"$FixedInput = if ($args.Count -ne 0) {$args} else {$input}
$Path = $FixedInput[0]
$Settings = $FixedInput[1]
if (!(Test-Path $Path))
{
    $null = New-Item $Path -Force
}
foreach ($Key in $Settings.Keys)
{
    $null = New-ItemProperty -LiteralPath $Path -Name $Key -Value $Settings[$Key] -Force
}";
        public static void SetRegistrySettings<T>(CommandInvocationIntrinsics cmdInvocationIntrinsics, string registryPath, Dictionary<string, T> settings)
        {
            _ = cmdInvocationIntrinsics.InvokeScript(setSettingsScript, registryPath, settings);
        }
        public static PSObject GetRegistrySettings(CommandInvocationIntrinsics cmdInvocationIntrinsics, string registryPath)
        {
            var result = cmdInvocationIntrinsics.InvokeScript("$Path = if ($args.Count -ne 0) {$args} else {$input};Get-ItemProperty -LiteralPath $Path", registryPath);
            return result.Count > 0 ? result[0] : null;
        }

        public static ErrorCategory GetErrorCategory(Exception exception)
        {
            switch (exception)
            {
                case Win32Exception nativeError:
                    switch (nativeError.NativeErrorCode)
                    {
                        case 2:
                            return ErrorCategory.ObjectNotFound;

                        case 5:
                        case 1314:
                            return ErrorCategory.PermissionDenied;

                        case 6:
                        case 50:
                            return ErrorCategory.InvalidOperation;

                        case 87:
                        case 1610:
                            return ErrorCategory.InvalidArgument;

                        case -1071241844:
                            return ErrorCategory.ResourceUnavailable;

                        case -2147467259:
                        case 31:
                            return ErrorCategory.DeviceError;

                        default:
                            return ErrorCategory.NotSpecified;
                    }

                case ArgumentNullException _:
                case ArgumentException _:
                    return ErrorCategory.InvalidArgument;

                case IOException _:
                case ObjectDisposedException _:
                    return ErrorCategory.ResourceUnavailable;

                case InvalidOperationException _:
                    return ErrorCategory.InvalidOperation;

                case UnauthorizedAccessException _:
                case SecurityException _:
                    return ErrorCategory.PermissionDenied;

                case InvalidCastException _:
                    return ErrorCategory.InvalidType;

                default:
                    return ErrorCategory.NotSpecified;
            }
        }
    }
}