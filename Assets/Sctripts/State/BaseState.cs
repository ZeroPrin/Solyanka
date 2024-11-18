using UnityEngine;

public abstract class BaseState
{
    protected NPC npc;

    public BaseState(NPC npc)
    {
        this.npc = npc;
    }

    public virtual void Enter() { }

    public virtual void Update() { }

    public virtual void Exit() { }

    public virtual void HandleEvent(Event e) { }
}
