using static Enums;

public class PriorityTask
{
    public TaskPriority Priority;
    public BaseState TaskState;

    public PriorityTask(TaskPriority priority, BaseState state)
    {
        Priority = priority;
        TaskState = state;
    }
}