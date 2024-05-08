using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerMotor : MonoBehaviour
    {
        private CharacterController controller;
        private Vector3 playerVelocity;
        private bool isGrounded;
        public bool isMovementPressed;
        public bool _isRunPressed;
        public float playerSpeed = 7f;
        public float gravity = -9.81f;
        public Vector3 movementDirection;
        public Vector3 runMovement;
        public float runMultiplier = 2f;
        public float rotationSpeed = 1f;

        public float jumpHeight = 0.75f;

        public List<GameObject> collectibleBones = new();
        public int bonesLeft = 4;
        public TMP_Text boneCountText;

        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            isGrounded = controller.isGrounded;
        }

        public void ProcessMovement(Vector2 input)
        {
            movementDirection.x = input.x;
            movementDirection.z = input.y;
            runMovement = movementDirection * runMultiplier;
            isMovementPressed = input.x != 0 || input.y != 0;

            // Rotate the player model in the direction of movement
            if (isMovementPressed)
            {
                Quaternion toRotation = Quaternion.LookRotation(new Vector3(input.x, 0, input.y), Vector3.up);
                transform.rotation =
                    Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }

            controller.Move(transform.TransformDirection(!_isRunPressed ? movementDirection : runMovement) *
                            (playerSpeed * Time.deltaTime));
            playerVelocity.y += gravity * Time.deltaTime;
            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = -2f;
            }

            controller.Move(playerVelocity * Time.deltaTime);
            // Debug.Log(playerVelocity.y);
        }

        public void jump()
        {
            if (isGrounded)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
            }
        }

        public void Take()
        {
            foreach (var bone in collectibleBones.Where(bone =>
                         bone != null && bone.CompareTag("CollectableBone")))
            {
                bonesLeft--;
                Destroy(bone);
                boneCountText.text = bonesLeft + " Bones left to find!";
            }

            collectibleBones.Clear();
        }
    }
}