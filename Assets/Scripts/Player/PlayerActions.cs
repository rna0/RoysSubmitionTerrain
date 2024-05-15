using Door;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerActions : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text UseText;
        [SerializeField]
        private Transform Camera;
        [SerializeField]
        private float MaxUseDistance = 5f;
        [SerializeField]
        private LayerMask UseLayers;

        public void OnTake()
        {
            Debug.Log("Take");
            if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers))
            {
                if (hit.collider.TryGetComponent<DoorBehavior>(out DoorBehavior door))
                {
                    if (door.isOpen)
                    {
                        door.Close();
                    }
                    else
                    {
                        door.Open(transform.position);
                    }
                }
            }
        }

        private void Update()
        {
            if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers)
                && hit.collider.TryGetComponent<DoorBehavior>(out DoorBehavior door))
            {
                if (door.isOpen)
                {
                    UseText.SetText("פתח עם מקש \"E\"");
                }
                else
                {
                    UseText.SetText("סגור עם מקש \"E\"");
                }
                UseText.gameObject.SetActive(true);
                UseText.transform.position = hit.point - (hit.point - Camera.position).normalized * 0.01f;
                UseText.transform.rotation = Quaternion.LookRotation((hit.point - Camera.position).normalized);
            }
            else
            {
                UseText.gameObject.SetActive(false);
            }
        }
    }
}
