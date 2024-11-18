using UnityEngine;

public class StateManager
{
    private BaseState currentState; // ������� ���������
    private BaseState previousState; // ���������� ��������� (���� �����)
    private NPC npc; // ������ �� NPC, ����� ��������� ����� ��������� ��

    public StateManager(NPC npc)
    {
        this.npc = npc;
    }

    // ������������� ������� ���������
    public void SetInitialState(BaseState initialState)
    {
        currentState = initialState;
        currentState.Enter(); // ������ � ��������� ���������
    }

    // ����� ���������
    public void ChangeState(BaseState newState)
    {
        if (currentState != null)
        {
            currentState.Exit(); // �������� Exit � �������� ���������
            previousState = currentState; // ��������� ���������� ���������
        }

        currentState = newState; // ������������� ����� ���������
        currentState.Enter(); // ������ � ����� ���������
    }

    // ���������� �������� ���������
    public void UpdateState()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    // ��������� ������� ������� ����������
    public void HandleEvent(Event e)
    {
        if (currentState != null)
        {
            currentState.HandleEvent(e);
        }
    }

    // ������� � ����������� ��������� (���� �����)
    public void ReturnToPreviousState()
    {
        if (previousState != null)
        {
            ChangeState(previousState);
        }
    }
}
