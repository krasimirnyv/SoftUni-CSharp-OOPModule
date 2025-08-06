using System;
using System.Text;

public static class ConsoleWindow
{
#if WINDOWS
            [DllImport("kernel32.dll", ExactSpelling = true)]
            private static extern IntPtr GetConsoleWindow();
            private static IntPtr ThisConsole = GetConsoleWindow();
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
            private const int Maximize = 3;
#endif
        
            public static void CustomizeConsole()
            {
                Console.OutputEncoding = Encoding.Unicode;

#if WINDOWS
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                ShowWindow(ThisConsole, Maximize);
                ShowWindow(ThisConsole, Maximize);
#endif
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Clear();
                Console.CursorVisible = false;
            }
}