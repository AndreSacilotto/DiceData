using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Dices
{
    public static class Clipboard
    {

        #region DLL


        [DllImport("kernel32.dll")]
        private static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

        [DllImport("kernel32.dll")]
        private static extern uint GetLastError();

        [DllImport("kernel32.dll")]
        private static extern IntPtr LocalFree(IntPtr hMem);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GlobalFree(IntPtr hMem);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GlobalUnlock(IntPtr hMem);

        [DllImport("kernel32.dll", EntryPoint = "RtlCopyMemory", SetLastError = false)]
        private static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        private static extern IntPtr SetClipboardData(uint uFormat, IntPtr data);

        //READ ONLY

        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsClipboardFormatAvailable(uint format);

        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern int GlobalSize(IntPtr hMem);

        [DllImport("User32.dll", SetLastError = true)]
        private static extern IntPtr GetClipboardData(uint uFormat);

        #endregion

        private const uint CF_TEXT = 1U;
        private const uint CF_UNICODETEXT = 13U;

        [STAThread]
        public static bool PushString(string message)
        {
            var isAscii = message != null && (message == Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(message)));
            return isAscii ? PushUnicodeString(message) : PushASCIIString(message);
        }
        [STAThread]
        public static bool PushUnicodeString(string message) => PushStringToClipboard(message, CF_UNICODETEXT);
        [STAThread]
        public static bool PushASCIIString(string message) => PushStringToClipboard(message, CF_TEXT);

        [STAThread]
        private static bool PushStringToClipboard(string message, uint format)
        {
            if (message == null || !OpenClipboard(IntPtr.Zero))
                return false;
            try
            {
                var sizeOfChar = format switch
                {
                    CF_TEXT => 1,
                    CF_UNICODETEXT => 2,
                    _ => throw new Exception("Not Reachable"),
                };
                var characters = (uint)message.Length;
                uint bytes = (characters + 1u) * (uint)sizeOfChar;

                const int GMEM_MOVABLE = 0x0002;
                const int GMEM_ZEROINIT = 0x0040;
                const int GHND = GMEM_MOVABLE | GMEM_ZEROINIT;

                // IMPORTANT: SetClipboardData requires memory that was acquired with GlobalAlloc using GMEM_MOVABLE.
                var hGlobal = GlobalAlloc(GHND, (UIntPtr)bytes);
                if (hGlobal == IntPtr.Zero)
                    return false;

                try
                {
                    var source = format switch
                    {
                        CF_TEXT => Marshal.StringToHGlobalAnsi(message),
                        CF_UNICODETEXT => Marshal.StringToHGlobalUni(message),
                        _ => throw new Exception("Not Reachable"),
                    };
                    try
                    {
                        var target = GlobalLock(hGlobal);
                        if (target == IntPtr.Zero)
                            return false;

                        try
                        {
                            CopyMemory(target, source, bytes);
                        }
                        finally
                        {
                            GlobalUnlock(target);
                        }

                        if (SetClipboardData(format, hGlobal).ToInt64() != 0)
                            hGlobal = IntPtr.Zero;
                        else
                            return false;
                    }
                    finally
                    {
                        // Marshal.StringToHGlobalUni actually allocates with LocalAlloc, thus we should theorhetically use LocalFree to free the memory...
                        // ... but Marshal.FreeHGlobal actully uses a corresponding version of LocalFree internally, so this works, even though it doesn't
                        //  behave exactly as expected.
                        Marshal.FreeHGlobal(source);
                    }
                }
                finally
                {
                    if (hGlobal != IntPtr.Zero)
                        GlobalFree(hGlobal);
                }
            }
            finally
            {
                CloseClipboard();
            }

            return true;
        }

        public static string GetText()
        {
            if (!IsClipboardFormatAvailable(CF_UNICODETEXT))
                return null;
            try
            {
                if (!OpenClipboard(IntPtr.Zero))
                    return null;
                IntPtr handle = GetClipboardData(CF_UNICODETEXT);
                if (handle == IntPtr.Zero)
                    return null;
                IntPtr pointer = IntPtr.Zero;
                try
                {
                    pointer = GlobalLock(handle);
                    if (pointer == IntPtr.Zero)
                        return null;

                    int size = GlobalSize(handle);
                    byte[] buff = new byte[size];

                    Marshal.Copy(pointer, buff, 0, size);

                    return Encoding.Unicode.GetString(buff).TrimEnd('\0');
                }
                finally
                {
                    if (pointer != IntPtr.Zero)
                        GlobalUnlock(handle);
                }
            }
            finally
            {
                CloseClipboard();
            }
        }


    }
}