using System;
using UnityEngine;

public class SafeExecutor
{
    public void ExecuteSafely(Action action)
    {
        try
        {
            action?.Invoke();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error executing function: {ex.Message}");
        }
    }
}
