using UnityEngine;
using System;

public abstract class Collectable : MonoBehaviour, ICollectable
{
    [SerializeField] private MeshRenderer _meshRenderer = null;
    [SerializeField] private Collider _collider = null;

    public Action<Collectable> CollectableCollected;

    private bool _isCollected = false;

    public void GetCollected()
    {
        if (_isCollected)
            return;

        CollectableCollected?.Invoke(this);

        _isCollected = true;

        _collider.isTrigger = true;
        _meshRenderer.enabled = false;
    }
}