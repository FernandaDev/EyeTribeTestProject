using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] GameObject breakablePrefab;
    public static event Action<GameObject> OnTargetDestroy;

    Rigidbody _rigidbody;
    Collider _collider;
    MeshRenderer _meshRenderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        Visible(true);
    }

    private void Visible(bool isVisible)
    {
        _collider.enabled = isVisible;
        _meshRenderer.enabled = isVisible;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Interactive"))
        {
            StartCoroutine(Break());
        }
    }

    private IEnumerator Break()
    {
        var obj = Instantiate(breakablePrefab, transform.position, breakablePrefab.transform.rotation);

        ChallengeManager.instance.CurrentChallenge.AddScore();

        Visible(false);

        yield return new WaitForSeconds(2f);

        Destroy(obj);

        if (OnTargetDestroy != null)
            OnTargetDestroy(this.transform.parent.gameObject);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
