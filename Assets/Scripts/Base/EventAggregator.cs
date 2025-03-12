using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventAggregator : IEventAggregator
{
    private readonly Dictionary<Type, List<Delegate>> _subscribers = new();

    public void Subscribe<T>(Action<T> action)
    {
        if (!_subscribers.ContainsKey(typeof(T)))
            _subscribers[typeof(T)] = new List<Delegate>();

        _subscribers[typeof(T)].Add(action);
    }

    public void Unsubscribe<T>(Action<T> action)
    {
        if (_subscribers.ContainsKey(typeof(T)))
        {
            _subscribers[typeof(T)].Remove(action);

            if (_subscribers[typeof(T)].Count == 0)
                _subscribers.Remove(typeof(T));
        }
    }

    public void Publish<T>(T eventData)
    {
        if (_subscribers.ContainsKey(typeof(T)))
        {
            var actions = _subscribers[typeof(T)].Cast<Action<T>>().ToList();

            foreach (var action in actions)
            {
                try
                {
                    action(eventData);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error in subscriber for event {typeof(T)}: {ex.Message}");
                    //_subscribers[typeof(T)].Remove(action);
                }
            }

            //if (_subscribers[typeof(T)].Count == 0)
            //{
            //    _subscribers.Remove(typeof(T));
            //}
        }
    }

    public void RemoveEvent<T>()
    {
        if (_subscribers.ContainsKey(typeof(T)))
        {
            _subscribers.Remove(typeof(T));
            Debug.Log($"Event {typeof(T).Name} and all its subscribers were removed.");
        }
    }

    public void RemoveAllEvents()
    {
        _subscribers.Clear();
        Debug.Log("All events and subscribers were removed.");
    }

    public List<Type> GetAllEvents()
    {
        return _subscribers.Keys.ToList();
    }

    public List<Delegate> GetSubscribers<T>()
    {
        if (_subscribers.ContainsKey(typeof(T)))
        {
            return _subscribers[typeof(T)].ToList();
        }
        else
        {
            Debug.LogWarning($"No subscribers found for event {typeof(T).Name}.");
            return new List<Delegate>();
        }
    }
    public void LogAllEvents()
    {
        List<Type> events = GetAllEvents();
        string str = "";

        foreach (var e in events)
        {
            str += $"{e.Name}\n";
        }

        Debug.Log(str);
    }

    public void LogAllSubscribers()
    {
        if (_subscribers.Count == 0)
        {
            Debug.LogWarning("No events registered.");
            return;
        }

        string str = "";

        foreach (var kvp in _subscribers)
        {
            str += $"Event: {kvp.Key.Name}, Subscribers Count: {kvp.Value.Count} \n";

            foreach (var subscriber in kvp.Value)
            {
                str += $"  - Subscriber: {subscriber.Method.Name} in {subscriber.Target?.GetType().Name ?? "Static Method"} \n";
            }
        }

        Debug.Log(str);
    }
}