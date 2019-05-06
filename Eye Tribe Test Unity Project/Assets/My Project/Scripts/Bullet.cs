using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : InteractiveObjectController
{ 

    protected override void OnEnable()
    {
        base.OnEnable();
        Visible(false);
        _isControlling = true;
    }
        
    private void Update()
    {
        if(_isControlling)
        {
            MoveObject();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }

    private void Shoot()
    {
        Visible(true);
        _isControlling = false;
        ThrowObject();
    }

    public void Visible(bool _isVisible)
    {
        _meshRenderer.enabled = _isVisible;
    }
}
