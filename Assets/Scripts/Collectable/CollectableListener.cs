using System;
using System.Collections.Generic;
using UnityEngine;

//Add collectable controls after new types of collectables.

public class CollectableListener : MonoBehaviour
{
    [SerializeField] private List<Collectable> _collectables = null;

    [SerializeField] private int _collectableCount = 0;

    public Action OnCollectableCollected;

    private void Awake()
    {
        RegisterToEvents();
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        foreach (Collectable col in _collectables)
            col.CollectableCollected += CollectableCollected;
    }

    private void UnregisterFromEvents()
    {
        foreach (Collectable col in _collectables)
            col.CollectableCollected -= CollectableCollected;
    }

    private void CollectableCollected()
    {
        OnCollectableCollected?.Invoke();
        _collectableCount++;
    }
}