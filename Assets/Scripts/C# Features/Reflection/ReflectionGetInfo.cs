using System;
using System.Reflection;
using UnityEngine;

public class ReflectionGetInfo : MonoBehaviour
{
    public class Person : IEater, IMovable
    {
        public string Name { get; }
        public Person(string name) => Name = name;
        public void Eat() => Debug.Log($"{Name} eats");
        public void Move() => Debug.Log($"{Name} moves");
        public void Jump() { }
    }
    interface IEater
    {
        void Eat();
    }
    interface IMovable
    {
        void Move();
    }

    void Start()
    {
        Type type;

        // Method 1
        type = typeof(Person);

        // Method 2
        //Person tom = new Person("Tom");
        //myType = tom.GetType();

        // Method 3
        //myType = Type.GetType("Person", false, true);

        Debug.Log($"Name: {type.Name}");
        Debug.Log($"Full Name: {type.FullName}");
        Debug.Log($"Namespace: {type.Namespace}");
        Debug.Log($"Is struct: {type.IsValueType}");
        Debug.Log($"Is class: {type.IsClass}");

        string tmp = "";
        foreach (Type i in type.GetInterfaces())
        {
            tmp += $"{i.Name} ";
        }
        Debug.Log($"Interfaces: {tmp}");

        tmp = "";
        foreach (var prop in type.GetProperties())
        {
            tmp += ($"{prop.Name}:({prop.PropertyType}) ");
        }
        Debug.Log($"Properties: {tmp}");

        tmp = "";
        foreach (var method in type.GetMethods())
        {
            tmp += ($"{method.Name} ");
        }
        Debug.Log($"Methods: {tmp}");

        tmp = "";
        foreach (MemberInfo member in type.GetMembers())
        {
            tmp += ($"{member.DeclaringType}:{member.MemberType}:{member.Name} ");
        }
        Debug.Log($"Members: {tmp}");
    }
}
