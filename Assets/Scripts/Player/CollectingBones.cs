using UnityEngine;

namespace Player
{
    public class CollectingBones : MonoBehaviour
    {
        private PlayerMotor _motor;

        private void Awake()
        {
            _motor = GetComponent<PlayerMotor>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("CollectableBone"))
            {
                _motor.collectableBones.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("CollectableBone"))
            {
                _motor.collectableBones.Remove(other.gameObject);
            }
        }
    }
}