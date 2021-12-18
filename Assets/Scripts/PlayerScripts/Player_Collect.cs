using UnityEngine;
using UnityEngine.SceneManagement;

//Integrate with collect animation.

public class Player_Collect : MonoBehaviour
{
    [SerializeField] private LayerMask _collectableLayerMask;

    private bool _canCollect = true;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "TutorialLevel")
            _canCollect = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (_collectableLayerMask == (_collectableLayerMask | (1 << hit.gameObject.layer)) && _canCollect)
            hit.gameObject.GetComponent<Collectable>().GetCollected();
    }

    public void CanCollect() => _canCollect = true;
}