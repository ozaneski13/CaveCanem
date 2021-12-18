using System;
using UnityEngine;

public class CollectableListener : MonoBehaviour
{
    private GameObject[] _collectables = null;

    private int _coinCount = 0;
    public int CoinCount => _coinCount;

    private int _boneCount = 0;
    public int BoneCount => _boneCount;

    private int _healthCount = 0;
    public int HealthCount => _healthCount;

    public Action OnCoinCollected;
    public Action OnHealthCollected;
    public Action OnBoneCollected;

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

    private void CollectableCollected(Collectable collectable)
    {
        if (collectable is Coin)
        {
            _coinCount++;
            OnCoinCollected?.Invoke();
        }

        else if (collectable is Bone)
        {
            _boneCount++;
            OnBoneCollected?.Invoke();
        }

        else if (collectable is Health)
        {
            _healthCount++;
            OnHealthCollected?.Invoke();
        }
    }
}