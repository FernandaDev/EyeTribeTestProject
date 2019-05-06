using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathZone : MonoBehaviour {

	public static event Action<Transform> OnTargetDeath;

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Target"))
		{
			if(OnTargetDeath != null)
			{
				OnTargetDeath(other.transform);
			}
		}
	}
}
