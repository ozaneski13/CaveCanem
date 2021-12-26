using UnityEngine;

public class Player_Feed : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyController = null;
    [SerializeField] private float _distanceVar = 1f;

    private Player _player = null;

    private void Start()
    {
        _player = Player.Instance;
    }

    private void Update()
    {
        Collectable feed = null;

        if (Input.GetKeyDown(KeyCode.B))
            feed = new Bone();
        /*else if (Input.GetKeyDown(KeyCode.N))
            feed = new Food();*/
        else
            return;
        
        GameObject closestEnemy = _enemyController.GetClosest();
        
        float distance = Vector3.Distance(closestEnemy.transform.position, transform.position);
        
        if (distance <= _distanceVar)
            Feed(closestEnemy, feed);
    }

    private void Feed(GameObject closestEnemy, Collectable feed)
    {
        Enemy enemy = closestEnemy.GetComponent<Enemy>();
        int feedCount = 0;

        if (feed is Bone)
            feedCount = _player.BoneCount;
        //else if (feed is Food)
            //feedCount = _player.FoodCount;

        if (feedCount == 0)
        {
            //UI I don't have any bones.

            return;
        }

        _player.BoneCount--;
        enemy.GetFeeded(feed);
        //
        //Check bone count from player.
        //Check dog type.
    }
}