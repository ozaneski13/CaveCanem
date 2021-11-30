using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private GameObject _playerVisual = null;
    [SerializeField] private CharacterController _characterController = null;

    [SerializeField] private Animator _animator = null;

    [SerializeField] private Transform _groundCheck = null;
    [SerializeField] private LayerMask _groundLayerMask;

    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private float _lowSpeed = 1f;
    [SerializeField] private float _normalSpeed = 2f;
    [SerializeField] private float _highSpeed = 4f;
    [SerializeField] private float _jumpForce = 2f;
    [SerializeField] private float _rotationSpeed = 100f;

    private Vector3 _velocity = Vector3.zero;

    private Vector3 _direction = Vector3.zero;

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (_characterController == null)
        {
            Debug.Log("Player character controller is null.");
            return;
        }

        bool isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundLayerMask);

        if (isGrounded)
        {
            _animator.SetBool("Jump", false);
            _animator.SetBool("Walk", false);
            _animator.SetBool("Run", false);
            _animator.SetBool("Idle", true);

            if (_velocity.y < 0)
                _velocity.y = -2f;
        }

        float defaultSpeed = _normalSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _animator.SetBool("Idle", false);
            _animator.SetBool("Run", true);
            defaultSpeed = _highSpeed;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            _animator.SetBool("Idle", false);
            _animator.SetBool("Walk", true);//Slow walk anim
            defaultSpeed = _lowSpeed;
        }

        float xPos = Input.GetAxis("Horizontal");//Yatay
        float zPos = Input.GetAxis("Vertical");//Dikey

        CheckDirection(xPos, zPos);
        
        _playerVisual.transform.rotation = Quaternion.RotateTowards(_playerVisual.transform.rotation, Quaternion.Euler(_direction.x, _direction.y, _direction.z), _rotationSpeed * Time.deltaTime);

        if (xPos != 0 || zPos != 0)
            _animator.SetBool("Idle", false);

        Vector3 moveDirection = transform.right * xPos + transform.forward * zPos;
        _characterController.Move(moveDirection * defaultSpeed * Time.deltaTime);

        float gravity = -9.81f;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _animator.SetBool("Jump", true);
            _velocity.y = Mathf.Sqrt(_jumpForce * -2f * gravity);
        }

        _velocity.y += gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void CheckDirection(float xPos, float zPos)
    {
        if (xPos == 0 && zPos == 0)
            return;

        if (xPos == 1 && zPos == 1)
            _direction.y = 45;
        else if (xPos == 1 && zPos == 0)
            _direction.y = 90;
        else if (xPos == 0 && zPos == 1)
            _direction.y = 0;
        else if (xPos == -1 && zPos == 0)
            _direction.y = -90;
        else if (xPos == 0 && zPos == -1)
            _direction.y = 180;
        else if (xPos == -1 && zPos == -1)
            _direction.y = -135;
        else if (xPos == 1 && zPos == -1)
            _direction.y = 135;
        else if (xPos == -1 && zPos == 1)
            _direction.y = -45;
    }
}