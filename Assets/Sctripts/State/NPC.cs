using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public StateManager StateManager { get; private set; }

    private void Start()
    {
        StateManager = new StateManager(this);

        // ������������� ��������� ���������
        StateManager.SetInitialState(new IdleState(this));
    }

    private void Update()
    {
        StateManager.UpdateState(); // ��������� ������� ���������
    }

    public void SetDestination(Vector3 target)
    {
        // ������ ������������ NPC
    }

    public bool HasReachedDestination()
    {
        // ������ ��������, ������ �� NPC ����
        return Vector3.Distance(transform.position, destination) < 1.0f;
    }
}
