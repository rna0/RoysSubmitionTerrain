using UnityEngine;

namespace Player
{
    public class PlayerMotor : MonoBehaviour
    {
        private CharacterController _controller;
        private Vector3 _playerVelocity;
        private bool _isGrounded;
        public bool isMovementPressed;
        public bool isRunPressed;
        public float playerSpeed = 7f;
        public float gravity = -9.81f;
        public Vector3 movementDirection;
        public Vector3 runMovement;
        public float runMultiplier = 2f;
        private PlayerActions playerActions;

        public float jumpHeight = 0.75f;

        void Awake()
        {
            playerActions = GetComponent<PlayerActions>();
        }

        // Start is called before the first frame update
        void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            _isGrounded = _controller.isGrounded;
        }

        public void ProcessMovement(Vector2 input)
        {
            movementDirection.x = input.x;
            movementDirection.z = input.y;
            runMovement = movementDirection * runMultiplier;
            isMovementPressed = input.x != 0 || input.y != 0;

            _controller.Move(transform.TransformDirection(!isRunPressed ? movementDirection : runMovement) *
                             (playerSpeed * Time.deltaTime));
            _playerVelocity.y += gravity * Time.deltaTime;
            if (_isGrounded && _playerVelocity.y < 0)
            {
                _playerVelocity.y = -2f;
            }

            _controller.Move(_playerVelocity * Time.deltaTime);
            // Debug.Log(playerVelocity.y);
        }

        public void Jump()
        {
            if (_isGrounded)
            {
                _playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
            }
        }

        public void Take()
        {
            playerActions.CheckDoorOnTake();
        }
    }
}