using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public static event Action OnScore;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactive"))
        {
            OnScore();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Interactive"))
        {
            OnScore();
        }
    }
}
