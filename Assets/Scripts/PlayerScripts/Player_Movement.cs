using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController = null;

    [SerializeField] private Transform _groundCheck = null;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _lowSpeed = 1f;
    [SerializeField] private float _normalSpeed = 2f;
    [SerializeField] private float _highSpeed = 4f;

    [SerializeField] private float _jumpForce = 2f;

    private float _defaultSpeed = 0f;

    private bool _isGrounded = true;

    private float _xPos = 0f;
    private float _zPos = 0f;
    private float _gravity = -9.81f;

    private Vector3 _moveDirection = Vector3.zero;

    private Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        _defaultSpeed = _normalSpeed;
    }

    void Update()
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

        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0)
            _velocity.y = -2f;

        _defaultSpeed = _normalSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            _defaultSpeed = _highSpeed;
        if (Input.GetKey(KeyCode.LeftControl))
            _defaultSpeed = _lowSpeed;

        _xPos = Input.GetAxis("Horizontal");
        _zPos = Input.GetAxis("Vertical");
        _moveDirection = transform.right * _xPos + transform.forward * _zPos;
        _characterController.Move(_moveDirection * _defaultSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && _isGrounded)
            _velocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravity);

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }
}