// You shouldn't be forced to implement an interface when your object doesn't share that purpose.
// The larger the interface, the more likely it includes methods not all implementers can use.



// WRONG CODE
using System;

public Interface ILead
{
   void CreateSubTask();
   void AssginTask();
   void WorkOnTask();
}

public class TeamLead : ILead
{
    public void AssignTask()
    {
        //Code to assign a task.
    }
    public void CreateSubTask()
    {
        //Code to create a sub task
    }
    public void WorkOnTask()
    {
        //Code to implement perform assigned task.
    }
}

public class Manager : ILead
{
    public void AssignTask()
    {
        //Code to assign a task.
    }
    public void CreateSubTask()
    {
        //Code to create a sub task.
    }
    public void WorkOnTask()
    {
        throw new Exception("Manager can't work on Task");
    }
}


// 
// PROBLEM:
// Since the Manager can't work on a task and, at the same time, no one can assign tasks to the Manager, this WorkOnTask() should not be in the Manager class.
// But we are implementing this class from the ILead interface; we must provide a concrete Method.
// Here we are forcing the Manager class to implement a WorkOnTask() method without a purpose.
//


// CORRECT CODE

public interface IProgrammer
{
    void WorkOnTask();
}

public interface ILead
{
    void AssignTask();
    void CreateSubTask();
}

public class Programmer : IProgrammer
{
    public void WorkOnTask()
    {
        //code to implement to work on the Task.
    }
}

public class Manager : ILead
{
    public void AssignTask()
    {
        //Code to assign a Task
    }
    public void CreateSubTask()
    {
        //Code to create a sub taks from a task.
    }
}

public class TeamLead : IProgrammer, ILead
{
    public void AssignTask()
    {
        //Code to assign a Task
    }
    public void CreateSubTask()
    {
        //Code to create a sub task from a task.
    }
    public void WorkOnTask()
    {
        //code to implement to work on the Task.
    }
}