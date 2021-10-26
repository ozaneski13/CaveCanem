using UnityEngine;

public class Player_Collect : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController = null;
    [SerializeField] private LayerMask _collectableLayerMask;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (_collectableLayerMask == (_collectableLayerMask | (1 << hit.gameObject.layer)))
            hit.gameObject.GetComponent<Collectable>().GetCollected();
    }
}
