namespace SmartBusinessApp.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();

            // Global exception handling (already good – keep it)
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // Start with LOGIN form (Form1)
            Application.Run(new Form1());
        }

        private static void Application_ThreadException(object? sender, ThreadExceptionEventArgs e)
        {
            HandleGlobalException(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object? sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                HandleGlobalException(ex);
            }
        }

        private static void HandleGlobalException(Exception ex)
        {
            try
            {
                string logPath = Path.Combine(Application.StartupPath, "error.log");
                File.AppendAllText(logPath,
                    $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                    $"Message: {ex.Message}\n" +
                    $"StackTrace: {ex.StackTrace}\n" +
                    $"Inner: {ex.InnerException?.Message}\n" +
                    "----------------------------------------\n");
            }
            catch { /* ignore log failure */ }

            MessageBox.Show(
                "An unexpected error occurred.\n\n" +
                "The application encountered a problem.\n" +
                "Please try again or restart the application.\n\n" +
                "Technical details have been saved to error.log.",
                "Application Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}