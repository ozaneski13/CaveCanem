using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    private Player _player = null;
    private GameObject[] _enemies = null;

    public Action<string, EEnemy> OnWrongFeed;

    private void Awake()
    {
        GatherEnemies();

        RegisterToEvents();
    }

    private void Start()
    {
        _player = Player.Instance;
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void GatherEnemies()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void RegisterToEvents()
    {
        for (int i = 0; i < _enemies.Length; i++)
            _enemies[i].GetComponent<Enemy>().OnWrongFeed += OnWrongFeedGiven;
    }

    private void UnregisterFromEvents()
    {
        for (int i = 0; i < _enemies.Length; i++)
            _enemies[i].GetComponent<Enemy>().OnWrongFeed -= OnWrongFeedGiven;
    }

    public GameObject GetClosest()
    {
        GameObject closestEnemy = null;
        float newDistance;
        float oldDistance = 100f;

        foreach (GameObject go in _enemies)
        {
            newDistance = Vector3.Distance(_player.gameObject.transform.position, go.transform.position);

            if (newDistance < oldDistance)
            {
                oldDistance = newDistance;
                closestEnemy = go;
            }
        }

        return closestEnemy;
    }

    private void OnWrongFeedGiven(string collectable, EEnemy enemyType) => OnWrongFeed?.Invoke(collectable, enemyType);
}