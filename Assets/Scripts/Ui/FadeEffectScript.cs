using TMPro;
using UnityEngine;

namespace Ui
{
    public class FadeEffectScript : MonoBehaviour
    {
        public TMP_Text hellText;
        public float fadeSpeed = 5;
        public bool entrance = true;

        private void Start()
        {
            hellText.color = Color.white;
        }

        private void FixedUpdate()
        {
            ColorChange();
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("Player")) entrance = true;
        }

        private void OnTriggerExit(Collider col)
        {
            if (col.gameObject.CompareTag("Player")) entrance = false;
        }

        private void ColorChange()
        {
            if (entrance)
            {
                hellText.color = Color.Lerp(hellText.color, Color.white, fadeSpeed * Time.deltaTime);
            }
            else
            {
                hellText.color = Color.Lerp(hellText.color, Color.clear, fadeSpeed * Time.deltaTime);
            }
        }
    }
}