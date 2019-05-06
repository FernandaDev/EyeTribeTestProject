using EyeTribe.Unity;
using System;
using System.Collections;
using UnityEngine;
using VRStandardAssets.Utils;

public class InteractiveObjectController : MonoBehaviour 
{
    [SerializeField] protected float thrust = 50f;
	[SerializeField] protected float lifetime = 2f;
    [SerializeField] protected Camera _camera;
    [SerializeField] protected float grabDistance = 4f;

	protected Transform reticleTransform;
	protected MeshRenderer _meshRenderer;
	protected Rigidbody _rigidbody;
	protected VRInteractiveItem interactiveItem;
	protected Vector3 forwardVector = Vector3.zero;

	protected bool _isControlling = false;

	public static event Action OnDespawn;

	protected virtual void OnEnable()
	{
		_meshRenderer.material.color = Color.white;
    }

    protected void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Awake()
	{
		if(_camera == null)
			_camera = Camera.main;

		if(reticleTransform == null)
			reticleTransform = GameObject.FindGameObjectWithTag("Reticle").transform;

		interactiveItem = GetComponent<VRInteractiveItem>();
		_rigidbody = GetComponent<Rigidbody>();
		_meshRenderer = GetComponent<MeshRenderer>();
	}

    void Update () 
	{
        if (Input.GetButtonDown("Fire1"))
        {
            if (_isControlling)
            {
                _isControlling = false;

                ThrowObject();
                return;
            }

            if (interactiveItem.IsOver && !_isControlling)
            {
                _isControlling = true;
                _meshRenderer.material.color = Color.green;
            }
        }

        if (_isControlling)
		{
			MoveObject();
		}
	}

    protected virtual void ThrowObject()
    {
       if(!_rigidbody)
	   	return;

		_rigidbody.isKinematic = false;
		_rigidbody.AddForce(forwardVector * thrust, ForceMode.Impulse);
		_meshRenderer.material.color = Color.red;
		StartCoroutine(Despawn(lifetime));
    }

    protected void MoveObject()
	{
		Vector3 gazeDirection = (reticleTransform.position - _camera.transform.position).normalized;
		
		forwardVector = gazeDirection;

		this.transform.position =  new Vector3(reticleTransform.position.x, reticleTransform.position.y, grabDistance);
	}

	protected IEnumerator Despawn(float _lifetime)
	{
		yield return new WaitForSeconds(_lifetime);
        _rigidbody.isKinematic = true;
		
		if(OnDespawn != null)
			OnDespawn();

		yield return null;
	}

}
