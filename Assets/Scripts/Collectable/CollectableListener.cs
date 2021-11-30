using System;
using UnityEngine;

//Add collectable controls after new types of collectables.

public class CollectableListener : MonoBehaviour
{
    private GameObject[] _collectables = null;

    private int _collectableCount = 0;
    public int CollectableCount => _collectableCount;

    public Action OnCollectableCollected;

    private void Awake()
    {
        GatherCollectables();
        RegisterToEvents();
    }

    private void GatherCollectables()
    {
        _collectables = GameObject.FindGameObjectsWithTag("Collectable");
    }

    private void RegisterToEvents()
    {
        foreach (GameObject go in _collectables)
            go.GetComponent<Collectable>().CollectableCollected += CollectableCollected;
    }

    private void CollectableCollected()
    {
        OnCollectableCollected?.Invoke();
        _collectableCount++;
    }
}