using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController = null;

    [SerializeField] private Transform _groundCheck = null;
    [SerializeField] private LayerMask _groundLayerMask;

    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private float _lowSpeed = 1f;
    [SerializeField] private float _normalSpeed = 2f;
    [SerializeField] private float _highSpeed = 4f;
    [SerializeField] private float _jumpForce = 2f;

    private Vector3 _velocity = Vector3.zero;

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

        if (isGrounded && _velocity.y < 0)
            _velocity.y = -2f;

        float defaultSpeed = _normalSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            defaultSpeed = _highSpeed;
        if (Input.GetKey(KeyCode.LeftControl))
            defaultSpeed = _lowSpeed;

        float xPos = Input.GetAxis("Horizontal");
        float zPos = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.right * xPos + transform.forward * zPos;
        _characterController.Move(moveDirection * defaultSpeed * Time.deltaTime);

        float gravity = -9.81f;

        if (Input.GetButtonDown("Jump") && isGrounded)
            _velocity.y = Mathf.Sqrt(_jumpForce * -2f * gravity);

        _velocity.y += gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }
}