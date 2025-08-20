using MartinGC94.VfxSettings.Native;
using MartinGC94.VfxSettings.Native.User32Enums;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MartinGC94.VfxSettings.API
{
    internal static class SettingsHandler
    {
        internal static void ThrowOnError(bool returnResult)
        {
            if (!returnResult)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        internal static bool GetFeatureToggle(SystemParametersAction feature)
        {
            bool value = false;
            ThrowOnError(NativeMethods.SystemParametersInfoW(feature, 0, ref value, UpdateFlags.DoNotPublish));
            return value;
        }

        internal static void SetFeatureToggle(SystemParametersAction feature, bool enabled, bool uiParam = false)
        {
            if (uiParam)
            {
                ThrowOnError(NativeMethods.SystemParametersInfoW(feature, enabled, null, UpdateFlags.SaveAndPublish));
            }
            else
            {
                ThrowOnError(NativeMethods.SystemParametersInfoW(feature, 0, enabled, UpdateFlags.SaveAndPublish));
            }
        }

        internal static uint GetFeatureValue(SystemParametersAction feature)
        {
            uint value = 0;
            ThrowOnError(NativeMethods.SystemParametersInfoW(feature, 0, ref value, UpdateFlags.DoNotPublish));
            return value;
        }

        internal static void SetFeatureValue(SystemParametersAction feature, uint value, bool uiParam = false)
        {
            if (uiParam)
            {
                ThrowOnError(NativeMethods.SystemParametersInfoW(feature, value, null, UpdateFlags.SaveAndPublish));
            }
            else
            {
                ThrowOnError(NativeMethods.SystemParametersInfoW(feature, 0, value, UpdateFlags.SaveAndPublish));
            }
        }
    }
}
