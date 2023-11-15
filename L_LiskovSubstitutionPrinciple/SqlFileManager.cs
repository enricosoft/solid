// you should be able to use any derived class instead of a parent class and have it behave in the same manner without modification


// WRONG CODE
using System.Collections.Generic;
using System.Text;

public class SqlFile
{
    public string FilePath { get; set; }
    public string FileText { get; set; }
    public string LoadText()
    {
        /* Code to read text from sql file */
    }
    public string SaveText()
    {
        /* Code to save text into sql file */
    }
}

public class SqlFileManager
{
    public List<SqlFile? lstSqlFiles {get;set
}
public string GetTextFromFiles()
{
    StringBuilder objStrBuilder = new StringBuilder();
    foreach (var objFile in lstSqlFiles)
    {
        objStrBuilder.Append(objFile.LoadText());
    }
    return objStrBuilder.ToString();
}
public void SaveTextIntoFiles()
{
    foreach (var objFile in lstSqlFiles)
    {
        //Check whether the current file object is read-only or not.
        // If yes, skip calling it's SaveText() method to skip the exception.

        if (!objFile is ReadOnlySqlFile)
            objFile.SaveText();
    }
}
}


//
// PROBLEM:
// Here we altered the SaveTextIntoFiles() method in the SqlFileManager class to determine whether or not the instance is of ReadOnlySqlFile to avoid the exception.
// We can't use this ReadOnlySqlFile class as a substitute for its parent without altering the SqlFileManager code.
//



// CORRECT CODE:
public interface IReadableSqlFile
{
    string LoadText();
}

public interface IWritableSqlFile
{
    void SaveText();
}

public class SqlFile : IWritableSqlFile, IReadableSqlFile
{
    public string FilePath { get; set; }
    public string FileText { get; set; }

    public string LoadText()
    {
        /* Code to read text from sql file */
    }

    public void SaveText()
    {
        /* Code to save text into sql file */
    }
}

public class SqlFileManager
{
    public string GetTextFromFiles(List<IReadableSqlFile> aLstReadableFiles)
    {
        StringBuilder objStrBuilder = new StringBuilder();
        foreach (var objFile in aLstReadableFiles)
        {
            objStrBuilder.Append(objFile.LoadText());
        }
        return objStrBuilder.ToString();
    }

    public void SaveTextIntoFiles(List<IWritableSqlFile> aLstWritableFiles)
    {
        foreach (var objFile in aLstWritableFiles)
        {
            objFile.SaveText();
        }
    }
}