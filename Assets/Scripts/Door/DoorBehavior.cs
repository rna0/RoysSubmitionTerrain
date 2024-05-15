using System.Collections;
using UnityEngine;

namespace Door
{
    public class DoorBehavior : MonoBehaviour
    {
        public bool isOpen = false;
        [SerializeField] private bool isRotatingDoor = true;
        [SerializeField] private float speed = 1f;

        [Header("Rotation Configs")] [SerializeField]
        private float rotationAmount = 90f;

        [SerializeField] private float forwardDirection = 0;

        [Header("Sliding Configs")] [SerializeField]
        private Vector3 slideDirection = Vector3.back;

        [SerializeField] private float slideAmount = 1.9f;

        private Vector3 _startRotation;
        private Vector3 _startPosition;
        private Vector3 _forward;

        private Coroutine _animationCoroutine;

        private void Awake()
        {
            _startRotation = transform.rotation.eulerAngles;
            // Since "Forward" actually is pointing into the door frame, choose a direction to think about as "forward" 
            _forward = transform.right;
            _startPosition = transform.position;
        }

        public void Open(Vector3 UserPosition)
        {
            if (!isOpen)
            {
                if (_animationCoroutine != null)
                {
                    StopCoroutine(_animationCoroutine);
                }

                if (isRotatingDoor)
                {
                    float dot = Vector3.Dot(_forward, (UserPosition - transform.position).normalized);
                    Debug.Log($"Dot: {dot.ToString("N3")}");
                    _animationCoroutine = StartCoroutine(DoRotationOpen(dot));
                }
                else
                {
                    _animationCoroutine = StartCoroutine(DoSlidingOpen());
                }
            }
        }

        private IEnumerator DoRotationOpen(float ForwardAmount)
        {
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation;

            if (ForwardAmount >= forwardDirection)
            {
                endRotation = Quaternion.Euler(new Vector3(0, _startRotation.y + rotationAmount, 0));
            }
            else
            {
                endRotation = Quaternion.Euler(new Vector3(0, _startRotation.y - rotationAmount, 0));
            }

            isOpen = true;

            float time = 0;
            while (time < 1)
            {
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
                yield return null;
                time += Time.deltaTime * speed;
            }
        }

        private IEnumerator DoSlidingOpen()
        {
            Vector3 endPosition = _startPosition + slideAmount * slideDirection;
            Vector3 startPosition = transform.position;

            float time = 0;
            isOpen = true;
            while (time < 1)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, time);
                yield return null;
                time += Time.deltaTime * speed;
            }
        }

        public void Close()
        {
            if (isOpen)
            {
                if (_animationCoroutine != null)
                {
                    StopCoroutine(_animationCoroutine);
                }

                if (isRotatingDoor)
                {
                    _animationCoroutine = StartCoroutine(DoRotationClose());
                }
                else
                {
                    _animationCoroutine = StartCoroutine(DoSlidingClose());
                }
            }
        }

        private IEnumerator DoRotationClose()
        {
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.Euler(_startRotation);

            isOpen = false;

            float time = 0;
            while (time < 1)
            {
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
                yield return null;
                time += Time.deltaTime * speed;
            }
        }

        private IEnumerator DoSlidingClose()
        {
            Vector3 endPosition = _startPosition;
            Vector3 startPosition = transform.position;
            float time = 0;

            isOpen = false;

            while (time < 1)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, time);
                yield return null;
                time += Time.deltaTime * speed;
            }
        }
    }
}