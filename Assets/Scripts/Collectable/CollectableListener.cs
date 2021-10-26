using System.Collections.Generic;
using UnityEngine;

public class CollectableListener : MonoBehaviour
{
    [SerializeField] private List<Collectable> _collectables = null;

    public int _collectableCount = 0;

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

    private void CollectableCollected(GameObject go)
    {
        _collectableCount++;
    }
}