using System.Runtime.InteropServices;

namespace Helpers;




public class Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
    public delegate bool EnumWindowsFunc(IntPtr windowPointer, int lParam);
}