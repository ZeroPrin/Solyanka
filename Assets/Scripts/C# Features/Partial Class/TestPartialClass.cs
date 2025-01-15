using UnityEngine;

public class TestPartialClass : MonoBehaviour
{
    private PartialClass _partialClass = new PartialClass();
    void Start()
    {
        _partialClass.Method1();
        _partialClass.Method2();
    }
}
