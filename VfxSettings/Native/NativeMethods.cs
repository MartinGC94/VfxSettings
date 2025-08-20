using MartinGC94.VfxSettings.Native.User32Enums;
using MartinGC94.VfxSettings.Native.User32Structs;
using System.Runtime.InteropServices;

namespace MartinGC94.VfxSettings.Native
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SystemParametersInfoW(
            [In] SystemParametersAction uiAction,
            [In] uint uiParam,
            [In] object pvParam,
            [In] UpdateFlags fWinIni
        );

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SystemParametersInfoW(
            [In] SystemParametersAction uiAction,
            [In] uint uiParam,
            [In] bool pvParam,
            [In] UpdateFlags fWinIni
        );

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SystemParametersInfoW(
            [In] SystemParametersAction uiAction,
            [In] uint uiParam,
            ref bool pvParam,
            [In] UpdateFlags fWinIni
        );

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SystemParametersInfoW(
            [In] SystemParametersAction uiAction,
            [In] bool uiParam,
            [In] object pvParam,
            [In] UpdateFlags fWinIni
        );

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SystemParametersInfoW(
            [In] SystemParametersAction uiAction,
            [In] uint uiParam,
            ref uint pvParam,
            [In] UpdateFlags fWinIni
        );

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SystemParametersInfoW(
            [In] SystemParametersAction uiAction,
            [In] uint uiParam,
            [In] uint pvParam,
            [In] UpdateFlags fWinIni
        );

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SystemParametersInfoW(
            [In] SystemParametersAction uiAction,
            [In] uint uiParam,
            ref ANIMATIONINFO pvParam,
            [In] UpdateFlags fWinIni
        );
    }
}