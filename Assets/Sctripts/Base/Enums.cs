public class Enums
{
    public enum SceneType
    {
        Load,
        Main,
        Gameplay
    }

    public enum TaskPriority
    {
        WorldEvent,  // Например, "работа".
        Survival,    // Еда, сон.
        Comfort,     // Отдых.
        LowPriority  // Развлечения.
    }
}
