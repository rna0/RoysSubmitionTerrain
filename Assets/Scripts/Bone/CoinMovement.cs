using UnityEngine;

namespace Bone
{
    public class CoinMovement : MonoBehaviour
    {
        public float amplitude = 0.5f; // The amount the coin moves up and down
        public float speed = 2f; // The speed of the movement
        public float rotationSpeed = 100f; // The speed of the rotation

        private Vector3 _startPos;

        void Start()
        {
            _startPos = transform.position;
        }

        void Update()
        {
            // Move the coin up and down
            Vector3 newPos = _startPos;
            newPos.y += Mathf.Sin(Time.time * speed) * amplitude;
            transform.position = newPos;

            // Rotate the coin
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}