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

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("CollectableBone"))
            {
                _motor.collectibleBones.Add(other.gameObject);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("CollectableBone"))
            {
                _motor.collectibleBones.Remove(other.gameObject);
            }
        }
    }
}