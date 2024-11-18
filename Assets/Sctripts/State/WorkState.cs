using UnityEngine;

public class WorkState : BaseState
{
    private float workDuration;
    private float timer;

    public WorkState(NPC npc) : base(npc)
    {
        this.workDuration = 8 * 60 * 60; // ��������, 8 ����� � ��������
        this.timer = 0f;
    }

    public override void Enter()
    {
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer >= workDuration)
        {
            npc.StateManager.ChangeState(new GoHomeState(npc)); // ����� ������ ��� �����
        }
    }

    public override void Exit()
    {
    }
}
