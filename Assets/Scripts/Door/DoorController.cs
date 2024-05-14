using UnityEngine;

namespace Door
{
    public class DoorController  : MonoBehaviour
    {
        public float openAngle = 90f;
        public float closeAngle = 0f;
        public float smooth = 2.0f;
        public bool open = false;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && open)
            {
                open = false;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                open = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                open = false;
            }
        }

        void FixedUpdate()
        {
            if (open)
            {
                Quaternion targetRotation = Quaternion.Euler(0, openAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
            }
            else
            {
                Quaternion targetRotation = Quaternion.Euler(0, closeAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
            }
        }
    }
}
