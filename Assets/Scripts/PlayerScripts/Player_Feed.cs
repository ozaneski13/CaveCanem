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

    public Action OnInsufficientBone;
    public Action OnInsufficientFood;

    public Action OnBoneUsed;
    public Action OnFoodUsed;

    private void Awake()
    {
        _bones = new List<Bone>();
        _foods = new List<Food>();
    }

    private void Start()
    {
        _player = Player.Instance;
    }

    private void Update()
    {
        Collectable feed = null;

        if (Input.GetKeyDown(KeyCode.B))
        {
            feed = Instantiate(_bone, _parentObject).GetComponent<Collectable>();
            _bones.Add((Bone)feed);
        }

        else if (Input.GetKeyDown(KeyCode.N))
        {
            feed = Instantiate(_food, _parentObject).GetComponent<Collectable>();
            _foods.Add((Food)feed);
        }

        else
            return;

        feed.gameObject.SetActive(false);

        GameObject closestEnemy = _enemyController.GetClosest();
        
        float distance = Vector3.Distance(closestEnemy.transform.position, transform.position);

        if (distance <= _distanceVar)
            Feed(closestEnemy, feed);
        else
            Destroy(feed.gameObject);
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

            return;//You don't have any bones UI.

        if (feedCount == 0)
        {
            if (isFood)
                OnInsufficientFood?.Invoke();

            else
                OnInsufficientBone?.Invoke();

            Destroy(feed.gameObject);

            return;
        }

        if (isFood)
        {
            _player.FoodCount--;
            OnBoneUsed?.Invoke();

            Food trashFood = _foods[_foods.Count - 1];
            _foods.RemoveAt(_foods.Count - 1);
            Destroy(trashFood.gameObject);
        }

        else
        {
            _player.BoneCount--;
            OnFoodUsed?.Invoke();

            Bone trashBone = _bones[_bones.Count - 1];
            _bones.RemoveAt(_bones.Count - 1);
            Destroy(trashBone.gameObject);
        }

        enemy.GetFeeded(feed);
    }
}