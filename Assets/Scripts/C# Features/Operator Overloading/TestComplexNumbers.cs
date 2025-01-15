using System;
using System.Numerics;
using UnityEngine;

public class TestComplexNumbers : MonoBehaviour
{
    void Start()
    {
        ComplexNumber c1 = new ComplexNumber(3, 4);
        ComplexNumber c2 = new ComplexNumber(1, -2);

        ComplexNumber sum = c1 + c2;
        Debug.Log($"Sum: {sum}");

        ComplexNumber difference = c1 - c2;
        Debug.Log($"Difference: {difference}");

        ComplexNumber product = c1 * c2;
        Debug.Log($"Product: {product}");

        ComplexNumber quotient = c1 / c2;
        Debug.Log($"Quotient: {quotient}");

        bool areEqual = c1 == c2;
        Debug.Log($"Are Equal: {areEqual}");

        Debug.Log($"c1: {c1} | Expected: 3 + 4i");
        Debug.Log($"-c1: {-c1} | Expected: -3 - 4i");
    }
}

