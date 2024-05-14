using UnityEngine;

namespace Player
{
    public class PlayerLook : MonoBehaviour
    {
        public Camera playerCamera;
        private float _xRotation;

        public float xSensitivity = 40f;
        public float ySensitivity = 40f;

        public void ProcessLook(Vector2 input)
        {
            var mouseX = input.x;
            var mouseY = input.y;

            _xRotation -= mouseY * Time.deltaTime * ySensitivity;
            _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);

            playerCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            transform.Rotate(Vector3.up * mouseX * Time.deltaTime * xSensitivity);
        }
    }
}