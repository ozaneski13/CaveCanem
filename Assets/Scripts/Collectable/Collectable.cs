using System;
using UnityEngine;

//Add collectable interface.

public class Collectable : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer = null;
    [SerializeField] private BoxCollider _boxCollider = null;

    public Action CollectableCollected;

    private bool _isCollected = false;
    
    public void GetCollected()
    {
        if (_isCollected)
            return;

        CollectableCollected?.Invoke();

        _isCollected = true;

        _boxCollider.isTrigger = true;
        _meshRenderer.enabled = false;
    }
}