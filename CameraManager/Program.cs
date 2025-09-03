using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace CameraManager
{
    internal static class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_SHOW = 5;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Force allocate console và redirect output
            AllocConsole();
            
            // Make sure console window is visible
            IntPtr consoleWindow = GetConsoleWindow();
            if (consoleWindow != IntPtr.Zero)
            {
                ShowWindow(consoleWindow, SW_SHOW);
            }

            // Redirect Console.Out to the allocated console
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
            Console.SetError(new StreamWriter(Console.OpenStandardError()) { AutoFlush = true });

            Console.WriteLine("=== Camera Manager Starting ===");
            Console.WriteLine($"Process ID: {System.Diagnostics.Process.GetCurrentProcess().Id}");
            Console.WriteLine($"Thread ID: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Current Directory: {Environment.CurrentDirectory}");
            Console.WriteLine($"Console allocated and redirected successfully!");
            
            try
            {
                Console.WriteLine("Initializing application configuration...");
                ApplicationConfiguration.Initialize();
                Console.WriteLine("✓ Application configuration initialized");
                
                Console.WriteLine("Creating Form2...");
                var form = new Form1();
                Console.WriteLine("✓ Form2 created successfully");
                
                Console.WriteLine("Starting application message loop...");
                Application.Run(form);
                Console.WriteLine("Application message loop ended");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"*** CRITICAL ERROR: {ex.Message} ***");
                Console.WriteLine($"Exception Type: {ex.GetType().Name}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner Stack trace: {ex.InnerException.StackTrace}");
                }
                
                MessageBox.Show($"Critical error: {ex.Message}\n\nSee console for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Console.WriteLine("Application exiting...");
                Console.WriteLine("Press any key to close console...");
                Console.ReadKey();
                FreeConsole();
            }
        }
    }
}