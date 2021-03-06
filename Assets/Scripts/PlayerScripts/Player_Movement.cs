using UnityEngine;
using UnityEngine.SceneManagement;

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

    private Player _player = null;
    private SFXManager _sfxManager = null;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _direction = Vector3.zero;

    private bool _canWalk = true;
    private bool _canJump = true;
    private bool _canSlow = true;
    private bool _canSprint = true;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "TutorialLevel")
        {
            _canWalk = false;
            _canJump = false;
            _canSlow = false;
            _canSprint = false;
        }
    }

    private void Start()
    {
        _player = Player.Instance;
        _sfxManager = SFXManager.Instance;

        RegisterToEvents();
    }

    private void Update()
    {
        if (_canWalk)
            Movement();
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        _player.OnDeath += PlayerDied;
    }

    private void UnregisterFromEvents()
    {
        if (_player != null)
            _player.OnDeath -= PlayerDied;
    }

    private void Movement()
    {
        if (_characterController == null)
        {
            Debug.Log("Player character controller is null.");
            return;
        }

        bool isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundLayerMask);
        AudioSource source = _sfxManager.GetHumanWalk();

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

        if (Input.GetKey(KeyCode.LeftShift) && _canSprint)
        {
            _animator.SetBool("Idle", false);
            _animator.SetBool("Run", true);
            defaultSpeed = _highSpeed;

            source = _sfxManager.GetHumanRun();
        }

        if (Input.GetKey(KeyCode.LeftControl) && _canSlow)
        {
            _animator.SetBool("Idle", false);
            _animator.SetBool("Walk", true);
            defaultSpeed = _lowSpeed;

            source = _sfxManager.GetHumanWalk(); 
        }

        if (defaultSpeed == _normalSpeed)
        {
            _animator.SetBool("Idle", false);
            _animator.SetBool("Walk", true);

            source = _sfxManager.GetHumanWalk();
        }

        float xPos = Input.GetAxis("Horizontal");
        float zPos = Input.GetAxis("Vertical");

        CheckDirection(xPos, zPos);
        
        _playerVisual.transform.rotation = Quaternion.RotateTowards(_playerVisual.transform.rotation, Quaternion.Euler(_direction.x, _direction.y, _direction.z), _rotationSpeed * Time.deltaTime);

        if (xPos != 0 || zPos != 0)
            _animator.SetBool("Idle", false);

        if (xPos == 0 && zPos == 0)
        {
            source = null;
            _animator.SetBool("Idle", true);
        }

        Vector3 moveDirection = transform.right * xPos + transform.forward * zPos;
        _characterController.Move(moveDirection * defaultSpeed * Time.deltaTime);

        float gravity = -9.81f;

        if (Input.GetButtonDown("Jump") && isGrounded && _canJump)
        {
            _animator.SetBool("Jump", true);
            _velocity.y = Mathf.Sqrt(_jumpForce * -2f * gravity);
        }

        _velocity.y += gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);

        if (source != null && !source.isPlaying)
            source.Play();
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

    public void CanDo(int popUpIndex)
    {
        if (popUpIndex == 0)
            _canWalk = true;
        else if (popUpIndex == 2)
            _canJump = true;
        else if (popUpIndex == 3)
            _canSprint = true;
        else if (popUpIndex == 4)
            _canSlow = true;
    }

    private void PlayerDied()
    {
        _canWalk = false;
        _canJump = false;
        _canSlow = false;
        _canSprint = false;
    }
}