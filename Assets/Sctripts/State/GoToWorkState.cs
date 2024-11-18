using UnityEngine;

public class GoToWorkState : BaseState
{
    private Vector3 workLocation;

    public GoToWorkState(NPC npc, Vector3 workLocation) : base(npc)
    {
        this.workLocation = workLocation;
    }

    public override void Enter()
    {
        npc.SetDestination(workLocation); // ���������� NPC � ����� ������
    }

    public override void Update()
    {
        if (npc.HasReachedDestination())
        {
            npc.StateManager.ChangeState(new WorkState(npc)); // ������� � ��������� "������"
        }
    }

    public override void Exit()
    {
    }
}
