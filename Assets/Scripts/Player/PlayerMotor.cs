using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        public string winningSceneName;

        public float jumpHeight = 0.75f;

        public List<GameObject> collectibleBones = new();
        public int bonesLeft = 4;
        public TMP_Text boneCountText;

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
            foreach (var bone in collectibleBones.Where(bone =>
                         bone != null && bone.CompareTag("CollectableBone")))
            {
                bonesLeft--;
                Destroy(bone);
                boneCountText.text = bonesLeft + " עצמות נותרו!";
            }

            collectibleBones.Clear();
            if (bonesLeft == 0)
            {
                boneCountText.text = "כל העצמות נמצאו!";
                SceneManager.LoadScene(winningSceneName);
            }
        }
    }
}