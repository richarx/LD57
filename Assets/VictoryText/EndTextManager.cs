using System.Collections;
using TMPro;
using UnityEngine;

namespace VictoryText
{
    public class EndTextManager : MonoBehaviour
    {
        [SerializeField] private GameObject textPrefab;

        public static EndTextManager instance;

        private void Awake()
        {
            instance = this;
        }

        public void SpawnText(string text, float delay, float displayDuration)
        {
            StartCoroutine(SpawnTextCoroutine(text, delay, displayDuration));
        }

        private IEnumerator SpawnTextCoroutine(string text, float delay, float displayDuration)
        {
            yield return new WaitForSeconds(delay);
            
            GameObject textObject = Instantiate(textPrefab, transform.position, Quaternion.identity, transform);

            TextMeshProUGUI textMeshProUGUI = textObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            textMeshProUGUI.text = text;

            textObject.transform.rotation = Vector2.right.AddRandomAngleToDirection(-25.0f, 25.0f).ToRotation();

            Destroy(textObject, displayDuration);
        }
    }
}
