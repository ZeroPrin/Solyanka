using UnityEngine;

public class TestNewInputSystem : MonoBehaviour
{
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        Debug.Log("Jump!");
        _rb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
    }
}
