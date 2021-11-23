using UnityEngine;

public class Player_Enemy_Collision : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController = null;

    private float _slideSpeed = 20f;
    private bool _collisionFlag = false;

    private void OnTriggerEnter(Collider other)
    {
        _collisionFlag = true;
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 moveDirection = transform.forward;

        if (_collisionFlag)
        {
            if(other.gameObject.layer == 8)
            {
                _characterController.Move(moveDirection * _slideSpeed * Time.deltaTime);
                _collisionFlag = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _collisionFlag = false;
    }
}
