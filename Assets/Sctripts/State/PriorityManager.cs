using System.Collections.Generic;

public class PriorityManager
{
    private List<PriorityTask> tasks = new List<PriorityTask>();

    public void AddTask(PriorityTask task)
    {
        tasks.Add(task);
        tasks.Sort((a, b) => a.Priority.CompareTo(b.Priority));
    }

    public BaseState GetTopPriorityTask()
    {
        return tasks.Count > 0 ? tasks[0].TaskState : null;
    }

}
