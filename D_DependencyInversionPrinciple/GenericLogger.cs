// high-level modules/classes should not depend on low-level modules/classes.
// First, both should depend upon abstractions.
// Secondly, abstractions should not rely upon details.
// Finally, details should depend upon abstractions.

// High-level modules/classes implement business rules or logic in a system (application).
// Low-level modules/classes deal with more detailed operations;


// WRONG CODE
using System.IO;
using System;

public class DbLogger
{
    public void LogMessage(string aMessage)
    {
        //Code to write message in the database.
    }
}

public class FileLogger
{
    public void LogMessage(string aStackTrace)
    {
        //code to log stack trace into a file.
    }
}

public class ExceptionLogger
{
    public void LogIntoFile(Exception aException)
    {
        FileLogger objFileLogger = new FileLogger();
        objFileLogger.LogMessage(GetUserReadableMessage(aException));
    }
    public void LogIntoDataBase(Exception aException)
    {
        DbLogger objDbLogger = new DbLogger();
        objDbLogger.LogMessage(GetUserReadableMessage(aException));
    }
    private string GetUserReadableMessage(Exception ex)
    {
        string strMessage = string.Empty;
        //code to convert Exception's stack trace and message to user readable format.
        ........
       return strMessage;
    }
}

public class DataExporter
{
    public void ExportDataFromFile()
    {
        try
        {
            //code to export data from files to database.
        }
        catch (IOException ex)
        {
            new ExceptionLogger().LogIntoDataBase(ex);
        }
        catch (Exception ex)
        {
            new ExceptionLogger().LogIntoFile(ex);
        }
    }
}

// 
// PROBLEM
// If the client wants to introduce a new logger, we must alter ExceptionLogger by adding a new method.
// Suppose we continue doing this after some time.
// In that case, we will see a fat ExceptionLogger class with a large set of practices that provide the functionality to log a message into various targets.
// Why does this issue occur? Because ExceptionLogger directly contacts the low-level classes FileLogger and DbLogger to log the exception. 
//



// CORRECT CODE
public interface ILogger
{
    void LogMessage(string aString);
}

public class DbLogger : ILogger
{
    public void LogMessage(string aMessage)
    {
        //Code to write message in database.
    }
}

public class EventLogger : ILogger
{
    public void LogMessage(string aMessage)
    {
        //Code to write a message in system's event viewer.
    }
}

public class FileLogger : ILogger
{
    public void LogMessage(string aStackTrace)
    {
        //code to log stack trace into a file.
    }
}

public class DataExporter
{
    public void ExportDataFromFile()
    {
        ExceptionLogger _exceptionLogger;
        try
        {
            //code to export data from files to database.
        }
        catch (IOException ex)
        {
            _exceptionLogger = new ExceptionLogger(new DbLogger());
            _exceptionLogger.LogException(ex);
        }
        catch (SqlException ex)
        {
            _exceptionLogger = new ExceptionLogger(new EventLogger());
            _exceptionLogger.LogException(ex);
        }
        catch (Exception ex)
        {
            _exceptionLogger = new ExceptionLogger(new FileLogger());
            _exceptionLogger.LogException(ex);
        }
    }
}