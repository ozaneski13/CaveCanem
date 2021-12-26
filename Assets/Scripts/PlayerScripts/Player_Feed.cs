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
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject closestEnemy = _enemyController.GetClosest();

            float distance = Vector3.Distance(closestEnemy.transform.position, transform.position);

            if (distance <= _distanceVar)
                Feed(closestEnemy);
        }
    }

    private void Feed(GameObject closestEnemy)
    {
        //Check bone count from player.
        //Check dog type.
    }
}