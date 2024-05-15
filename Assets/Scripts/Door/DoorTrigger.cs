using UnityEngine;
using UnityEngine.Serialization;

namespace Door
{
    public class DoorTrigger : MonoBehaviour
    { 
        [SerializeField]
        private DoorBehavior doorBehavior;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CharacterController>(out CharacterController controller))
            {
                if (!doorBehavior.isOpen)
                {
                    doorBehavior.Open(other.transform.position);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<CharacterController>(out CharacterController controller))
            {
                if (doorBehavior.isOpen)
                {
                    doorBehavior.Close();
                }
            }
        }
    }
}
