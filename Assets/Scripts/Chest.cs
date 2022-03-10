using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Chest : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Push(Vector3 direction, float pushForce)
    {
        Debug.Log("Bonjour");
        _rigidbody.AddForce(direction.normalized * pushForce, ForceMode.VelocityChange);
    }
}
