using Door;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerActions : MonoBehaviour
    {
        [SerializeField] private TMP_Text UseText;
        [SerializeField] private Transform Camera;
        [SerializeField] private float MaxUseDistance = 5f;
        [SerializeField] private LayerMask UseDoorLayers;
        [SerializeField] private LayerMask UseBalloonLayers;
        [SerializeField] private int bonesLeft = 4;
        [SerializeField] private TMP_Text boneCountText;
        [SerializeField] private string winningSceneName;
            //184.2
        public void CheckDoorOnTake()
        {
            if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseDoorLayers))
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
            else if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hitBalloon, MaxUseDistance,
                         UseBalloonLayers))
            {
                Destroy(hitBalloon.collider.gameObject);
                bonesLeft--;
                boneCountText.text = bonesLeft + " עצמות נותרו!";

                if (bonesLeft == 0)
                {
                    SceneManager.LoadScene(winningSceneName);
                }
            }
        }

        private void Update()
        {
            if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseDoorLayers)
                && hit.collider.TryGetComponent<DoorBehavior>(out DoorBehavior door))
            {
                if (door.isOpen)
                {
                    UseText.SetText("סגור דלת");
                }
                else
                {
                    UseText.SetText("פתח דלת");
                }

                UseText.gameObject.SetActive(true);
                UseText.transform.position = hit.point - (hit.point - Camera.position).normalized * 0.02f;
                UseText.transform.rotation = Quaternion.LookRotation((hit.point - Camera.position).normalized);
            }
            else if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hitBalloon, MaxUseDistance,
                         UseBalloonLayers))
            {
                UseText.SetText("קח בלון");
                UseText.gameObject.SetActive(true);
                UseText.transform.position = hitBalloon.point - (hitBalloon.point - Camera.position).normalized * 0.03f;
                UseText.transform.rotation = Quaternion.LookRotation((hitBalloon.point - Camera.position).normalized);
            }
            else
            {
                UseText.gameObject.SetActive(false);
            }
        }
    }
}