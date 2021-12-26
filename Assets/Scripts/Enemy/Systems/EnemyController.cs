using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Player _player = null;
    private GameObject[] _enemies = null;

    private void Awake()
    {
        GatherEnemies();
    }

    private void Start()
    {
        _player = Player.Instance;
    }

    private void GatherEnemies()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
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
}