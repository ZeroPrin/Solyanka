using UnityEngine;

public class StateManager
{
    private BaseState currentState; // Текущее состояние
    private BaseState previousState; // Предыдущее состояние (если нужно)
    private NPC npc; // Ссылка на NPC, чтобы состояния могли управлять им

    public StateManager(NPC npc)
    {
        this.npc = npc;
    }

    // Инициализация первого состояния
    public void SetInitialState(BaseState initialState)
    {
        currentState = initialState;
        currentState.Enter(); // Входим в начальное состояние
    }

    // Смена состояния
    public void ChangeState(BaseState newState)
    {
        if (currentState != null)
        {
            currentState.Exit(); // Вызываем Exit у текущего состояния
            previousState = currentState; // Сохраняем предыдущее состояние
        }

        currentState = newState; // Устанавливаем новое состояние
        currentState.Enter(); // Входим в новое состояние
    }

    // Обновление текущего состояния
    public void UpdateState()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    // Обработка событий текущим состоянием
    public void HandleEvent(Event e)
    {
        if (currentState != null)
        {
            currentState.HandleEvent(e);
        }
    }

    // Возврат к предыдущему состоянию (если нужно)
    public void ReturnToPreviousState()
    {
        if (previousState != null)
        {
            ChangeState(previousState);
        }
    }
}
