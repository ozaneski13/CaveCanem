using UnityEngine;

public class Player_Enemy_Collision : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController = null;
    [SerializeField] private float _slideSpeed = 20f;

    private bool _collisionFlag = false;

    private void OnTriggerEnter(Collider other)
    {
        _collisionFlag = true;
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 moveDirection = transform.forward;

        if (Player.Instance.transform.position.y > other.gameObject.transform.position.y)
        {
            if (_collisionFlag && other.gameObject.layer == 8)
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