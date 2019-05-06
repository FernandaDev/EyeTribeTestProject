using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTheBallChallenge : Challenge
{
    [SerializeField] Transform[] targets;

    Vector3[] targetsInitialPosition;

    int deadTargetsAmount;

    void OnEnable()
    {
        DeathZone.OnTargetDeath += OnTargetDeath;
    }

    void OnDisable()
    {
        DeathZone.OnTargetDeath -= OnTargetDeath;
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        targetsInitialPosition = new Vector3[targets.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            targetsInitialPosition[i] = targets[i].position;
        }
    }

    void OnTargetDeath(Transform target)
    {
        target.gameObject.SetActive(false);
        deadTargetsAmount++;
        target.GetComponent<Rigidbody>().isKinematic = true;

        if (deadTargetsAmount >= targets.Length)
        {
            deadTargetsAmount = 0;
            StartChallenge();
        }
    }

    public override void StartChallenge()
    {
        base.StartChallenge();

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].gameObject.SetActive(true);
            targets[i].position = targetsInitialPosition[i];
            targets[i].rotation = Quaternion.Euler(Vector3.zero);
            targets[i].GetComponent<Rigidbody>().isKinematic = false;
        }
    }

}
