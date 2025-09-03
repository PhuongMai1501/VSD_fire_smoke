using System;
using System.IO;

public static class FileLogger
{
    private static readonly object _lock = new object();
    private static string _logFilePath;
    private static volatile bool _isShuttingDown = false; // Flag ?? track shutdown state
    
    static FileLogger()
    {
        string logDirectory = Path.Combine(Environment.CurrentDirectory, "Logs");
        Directory.CreateDirectory(logDirectory);
        
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmms");
        _logFilePath = Path.Combine(logDirectory, $"CameraManager_{timestamp}.log");
        
        // Write initial header
        WriteToFile($"=== Camera Manager Log Started at {DateTime.Now:yyyy-MM-dd HH:mm:ss} ===");
        WriteToFile($"Process ID: {System.Diagnostics.Process.GetCurrentProcess().Id}");
        WriteToFile($"Working Directory: {Environment.CurrentDirectory}");
        WriteToFile($"Log File: {_logFilePath}");
        WriteToFile($"=== End Header ===\n");
    }
    
    public static void Log(string message)
    {
        string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
        string logEntry = $"[{timestamp}] {message}";
        
        // Safe console write - tránh l?i handle invalid trong shutdown
        if (!_isShuttingDown)
        {
            try
            {
                Console.WriteLine(logEntry);
            }
            catch (IOException)
            {
                // Console handle invalid - ignore during shutdown
                _isShuttingDown = true;
            }
            catch (Exception)
            {
                // Other console errors - ignore
                _isShuttingDown = true;
            }
        }
        
        // Write to file - luôn luôn ghi file
        WriteToFile(logEntry);
    }
    
    public static void LogError(string message)
    {
        string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
        string logEntry = $"[{timestamp}] ERROR: {message}";
        
        // Safe console write
        if (!_isShuttingDown)
        {
            try
            {
                Console.WriteLine(logEntry);
            }
            catch (IOException)
            {
                _isShuttingDown = true;
            }
            catch (Exception)
            {
                _isShuttingDown = true;
            }
        }
        
        // Write to file
        WriteToFile(logEntry);
    }
    
    public static void LogException(Exception ex, string context = "")
    {
        string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
        string logEntry = $"[{timestamp}] EXCEPTION {context}: {ex.Message}\n" +
                         $"                    Type: {ex.GetType().Name}\n" +
                         $"                    Stack: {ex.StackTrace}";
        
        if (ex.InnerException != null)
        {
            logEntry += $"\n                    Inner: {ex.InnerException.Message}";
        }
        
        // Safe console write
        if (!_isShuttingDown)
        {
            try
            {
                Console.WriteLine(logEntry);
            }
            catch (IOException)
            {
                _isShuttingDown = true;
            }
            catch (Exception)
            {
                _isShuttingDown = true;
            }
        }
        
        // Write to file
        WriteToFile(logEntry);
    }
    
    private static void WriteToFile(string content)
    {
        try
        {
            lock (_lock)
            {
                File.AppendAllText(_logFilePath, content + Environment.NewLine);
            }
        }
        catch (Exception ex)
        {
            // Fallback to console only if file writing fails
            if (!_isShuttingDown)
            {
                try
                {
                    Console.WriteLine($"[FileLogger Error]: {ex.Message}");
                }
                catch
                {
                    // Can't even write to console - ignore
                }
            }
        }
    }
    
    public static string GetLogFilePath()
    {
        return _logFilePath;
    }
    
    // Ph??ng th?c ?? mark shutdown state
    public static void MarkShutdown()
    {
        _isShuttingDown = true;
        WriteToFile("=== FileLogger: Shutdown mode activated - Console output disabled ===");
    }
}