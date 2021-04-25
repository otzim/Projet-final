using StealthGame;
using UnityEngine;



[SelectionBase]
public class PlayerMovementController : MonoBehaviour
{
    #region Show in inspector

    [Header("Controllers")]

    [SerializeField] private PlayerInputController _inputController;

    [Header("Movement parameters")]

    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _sneakSpeed;
    [SerializeField] private float _turnSpeed;

    //[Header("Jump parameters")]

    //[SerializeField] private float _jumpForce;

    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    //[SerializeField] private float gravity;
    [SerializeField] private bool isGrounded;
    //[SerializeField] private float jumpHeight;
    //private Vector3 velocity;

    [Header("Gravity parameters")]

    [SerializeField] private float _gravityMultiplier;
    [SerializeField] private float _gravityFallMultiplier;

    [Header("Ground checker")]

    [SerializeField] private PlayerGroundChecker _groundChecker;

    [Header("Animator")]


    [SerializeField] private AnimationPlayer _animationController;


    #endregion


    #region Init

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        _cameraTransform = Camera.main.transform;
    }


    #endregion


    #region Update

    private void Update()
    {
        CheckGround();

        _movementDirection = _cameraTransform.TransformDirection(_inputController.MovementInput);
        // Equivalent à 
        /* _movementDirection = _cameraTransform.right * _inputController.HorizontalInput.Value +
                            _cameraTransform.forward * _inputController.VerticalInput.Value; */
        _movementDirection.y = 0;
        _movementDirection.Normalize();
        _movementDirection *= _inputController.MovementInput.magnitude;

        SetSpeed();

       //// Jumping
       // if (_isGrounded && _inputController.JumpInput.IsDown)
       // {
       //     _jumpTrigger = true;
       //     _isJumping = true;
       //     _isGrounded = false;
       // }

       // if (_isGrounded && _isJumping)
       // {
       //     _isJumping = false;
       // }

       // _speed = Vector3.Distance(_lastPosition, _transform.position) / Time.deltaTime;
       // _lastPosition = _transform.position;


    }

    private void CheckGround()
    {
        if (_rigidbody.velocity.y < 0.01f)
        {
            _isGrounded = _groundChecker.CheckGround(out _groundPosition);
        }
    }

    private void SetSpeed()
    {


        if (_inputController.SneakInput.IsActive && _inputController.HasMovementInput)
        {
            _currentSpeed = _sneakSpeed;
            //_animationController.AnimationSneak();
        }
        else if (_inputController.RunInput.IsActive && _inputController.HasMovementInput)
        {
            _currentSpeed = _runSpeed;
            _animationController.AnimationRun();
        }
        else if (_inputController.HasMovementInput)
        {
            _currentSpeed = _walkSpeed;
            _animationController.AnimationWalk();

        }
        else
        {
            _animationController.AnimationIdle();
        }

        //_animationController.AnimationJump(_rigidbody.velocity.y);

    }

    private void FixedUpdate()
    {
        // Déplace le joueur
        Move();

        // Tourne le joueur vers l'orientation de la caméra
        RotateTowardsCameraForward();

        // Gravité
        Gravity();

       //// Saut
       // Jump();

        // Coller au sol
        StickToGround();
    }

    private void Move()
    {
        Vector3 velocity = _movementDirection * _currentSpeed;

        // Le Y que le moteur physique a calculé
        velocity.y = _rigidbody.velocity.y;

        _rigidbody.velocity = velocity;

        Vector3 localmovement = transform.InverseTransformDirection(_movementDirection);

        _animationController.AnimationVelocity(localmovement);


        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

       



     

    }

   

    private void RotateTowardsCameraForward()
    {
        if (_inputController.HasMovementInput)
        {
            Vector3 lookDirection = _cameraTransform.forward;
            lookDirection.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            rotation = Quaternion.RotateTowards(_rigidbody.rotation, rotation, _turnSpeed * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(rotation);
        }
    }

    private void Gravity()
    {
        if (_isGrounded)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            return;
        }

        // Chute
        if (_rigidbody.velocity.y < -0.01f)
        {
            _rigidbody.AddForce(Physics.gravity * _gravityFallMultiplier, ForceMode.Acceleration);
        }
        else
        {
            _rigidbody.AddForce(Physics.gravity * _gravityMultiplier, ForceMode.Acceleration);
        }
    }

    //private void Jump()
    //{
    //    if (!_jumpTrigger)
    //    {
    //        return;
    //    }

    //    _jumpTrigger = false;
    //    _isGrounded = false;

    //    _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
    //}

    private void StickToGround()
    {
        if (!_isGrounded)
        {
            return;
        }

        Vector3 position = _transform.position;
        position.y = _groundPosition.y;
        _rigidbody.MovePosition(position);
    }

    #endregion


    #region Private

    private Transform _transform;
    private Transform _cameraTransform;
    private Rigidbody _rigidbody;

    public Vector3 _movementDirection;
    private float _currentSpeed;

    private bool _isGrounded;
    private bool _jumpTrigger;
    private bool _isJumping;

    private Vector3 _groundPosition;

    private Vector3 _lastPosition;
    private float _speed;

    #endregion
}


