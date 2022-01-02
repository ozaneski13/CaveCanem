using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Collect : MonoBehaviour
{
    [SerializeField] private LayerMask _collectableLayerMask;

    private Player _player = null;

    private bool _canCollect = true;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "TutorialLevel")
            _canCollect = false;
    }

    private void Start()
    {
        _player = Player.Instance;

        RegisterToEvents();
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (_collectableLayerMask == (_collectableLayerMask | (1 << hit.gameObject.layer)) && _canCollect)
            hit.gameObject.GetComponent<Collectable>().GetCollected();
    }

    public void CanCollect() => _canCollect = true;

    private void OnDeath() => _canCollect = false;
}