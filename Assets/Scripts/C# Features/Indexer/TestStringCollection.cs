using UnityEngine;

public class TestStringCollection : MonoBehaviour
{
    private StringCollection _stringCollection = new StringCollection();

    public void Start()
    {
        Debug.Log($"{_stringCollection[0]}, {_stringCollection[1]}, {_stringCollection[2]}");

        _stringCollection[1] = "New string!";

        Debug.Log($"{_stringCollection[0]}, {_stringCollection[1]}, {_stringCollection[2]}");
    }
}
