using System;
using System.Collections.Generic;
using UnityEngine;

public class Player_Feed : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyController = null;
    [SerializeField] private float _distanceVar = 1f;

    [SerializeField] private Transform _parentObject = null;
    [SerializeField] private GameObject _bone = null;
    [SerializeField] private GameObject _food = null;

    private Player _player = null;

    private List<Bone> _bones = null;
    private List<Food> _foods = null;

    public Action OnUnnecessaryFoodUsed;

    public Action<string> OnInsufficientFeed;

    public Action OnBoneUsed;
    public Action OnFoodUsed;

    private bool _isPlayerDied = false;

    private void Awake()
    {
        _bones = new List<Bone>();
        _foods = new List<Food>();
    }

    private void Start()
    {
        _player = Player.Instance;

        RegisterToEvents();
    }

    private void Update()
    {
        if (_isPlayerDied)
            return;

        GameObject closestEnemy = _enemyController.GetClosest();
        Enemy enemy = closestEnemy.GetComponent<Enemy>();

        Collectable feed = null;

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (enemy.EnemyType == EEnemy.Friendly)
            {
                OnUnnecessaryFoodUsed?.Invoke();

                return;
            }

            feed = Instantiate(_bone, _parentObject).GetComponent<Collectable>();
            _bones.Add((Bone)feed);
        }

        else if (Input.GetKeyDown(KeyCode.N))
        {
            if (enemy.EnemyType == EEnemy.Friendly)
            {
                OnUnnecessaryFoodUsed?.Invoke();

                return;
            }

            feed = Instantiate(_food, _parentObject).GetComponent<Collectable>();
            _foods.Add((Food)feed);
        }

        else
            return;

        feed.gameObject.SetActive(false);
        
        float distance = Vector3.Distance(closestEnemy.transform.position, transform.position);

        if (distance <= _distanceVar)
            Feed(closestEnemy, feed);
        else
            Destroy(feed.gameObject);
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        _player.OnDeath += OnDeath;
    }

    private void UnregisterFromEvents()
    {
        if (_player != null)
            _player.OnDeath -= OnDeath;
    }

    private void Feed(GameObject closestEnemy, Collectable feed)
    {
        Enemy enemy = closestEnemy.GetComponent<Enemy>();
        int feedCount = 0;
        bool isFood = true;

        if (feed is Bone)
        {
            feedCount = _player.BoneCount;
            isFood = false;
        }

        else if (feed is Food)
            feedCount = _player.FoodCount;

        else
            return;

        if (feedCount == 0)
        {
            if (isFood)
                OnInsufficientFeed?.Invoke("Food");

            else
                OnInsufficientFeed?.Invoke("Bone");

            Destroy(feed.gameObject);

            return;
        }

        if (isFood)
        {
            _player.FoodCount--;
            OnFoodUsed?.Invoke();

            Food trashFood = _foods[_foods.Count - 1];
            _foods.RemoveAt(_foods.Count - 1);
            Destroy(trashFood.gameObject);
        }

        else
        {
            _player.BoneCount--;
            OnBoneUsed?.Invoke();

            Bone trashBone = _bones[_bones.Count - 1];
            _bones.RemoveAt(_bones.Count - 1);
            Destroy(trashBone.gameObject);
        }

        enemy.GetFeeded(feed);
    }

    private void OnDeath() => _isPlayerDied = true;
}