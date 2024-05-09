using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInput playerInput;
        private PlayerInput.OnFootActions onFoot;
        private PlayerMotor motor;
        private PlayerLook look;
        public Animator _animator;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");

        // Start is called before the first frame update
        void Awake()
        {
            playerInput = new PlayerInput();
            onFoot = playerInput.OnFoot;
            motor = GetComponent<PlayerMotor>();
            look = GetComponent<PlayerLook>();
            onFoot.Jump.performed += ctx => motor.jump();
            onFoot.Take.performed += ctx => motor.Take();
            playerInput.OnFoot.Run.started += onRun;
            playerInput.OnFoot.Run.canceled += onRun;
        }

        void onRun(InputAction.CallbackContext context)
        {
            motor.isRunPressed = context.ReadValueAsButton();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            HandleAnimation();
            motor.ProcessMovement(onFoot.Movement.ReadValue<Vector2>());
        }

        void LateUpdate()
        {
            look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
        }

        void HandleAnimation()
        {
            bool isWalking = _animator.GetBool(IsWalking);
            bool isRunning = _animator.GetBool(IsRunning);

            if (motor.isMovementPressed && !isWalking)
            {
                _animator.SetBool(IsWalking, true);
            }
            else if (!motor.isMovementPressed && isWalking)
            {
                _animator.SetBool(IsWalking, false);
            }

            if (motor.isMovementPressed && motor.isRunPressed && !isRunning)
            {
                _animator.SetBool(IsRunning, true);
            }
            else if (!motor.isMovementPressed && !motor.isRunPressed && isRunning)
            {
                _animator.SetBool(IsRunning, false);
            }
        }

        private void OnEnable()
        {
            onFoot.Enable();
        }

        private void OnDisable()
        {
            onFoot.Disable();
        }
    }
}