using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] GameObject breakablePrefab;
    public static event Action<GameObject> OnTargetDestroy;

    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Interactive"))
        {
            Break();
        }
    }

    private void Break()
    {
        var obj = Instantiate(breakablePrefab, transform.position, breakablePrefab.transform.rotation);

        Destroy(obj, 2f);

        if (OnTargetDestroy != null)
            OnTargetDestroy(this.gameObject);
    }
}
