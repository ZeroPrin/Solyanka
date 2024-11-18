using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public StateManager StateManager { get; private set; }

    private void Start()
    {
        StateManager = new StateManager(this);

        // Устанавливаем начальное состояние
        StateManager.SetInitialState(new IdleState(this));
    }

    private void Update()
    {
        StateManager.UpdateState(); // Обновляем текущее состояние
    }

    public void SetDestination(Vector3 target)
    {
        // Логика передвижения NPC
    }

    public bool HasReachedDestination()
    {
        // Логика проверки, достиг ли NPC цели
        return Vector3.Distance(transform.position, destination) < 1.0f;
    }
}
