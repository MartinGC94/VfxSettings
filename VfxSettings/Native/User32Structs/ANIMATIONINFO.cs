using System.Runtime.InteropServices;

namespace MartinGC94.VfxSettings.Native.User32Structs
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct ANIMATIONINFO
    {
        public uint cbSize;
        public int iMinAnimate;
    }
}