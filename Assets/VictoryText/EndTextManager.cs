using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VictoryText
{
    public class EndTextManager : MonoBehaviour
    {
        [SerializeField] private GameObject textPrefab;
        [SerializeField] private GameObject faceSuccessPrefab;
        [SerializeField] private GameObject faceFailurePrefab;

        public static EndTextManager instance;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                SpawnText("Yay !", 0.0f, 1.5f, true);
            if (Input.GetMouseButtonDown(1))
                SpawnText("Nay !", 0.0f, 1.5f, false);
        }

        public void SpawnText(string text, float delay, float displayDuration, bool success)
        {
            StartCoroutine(SpawnTextCoroutine(text, delay, displayDuration, success));
        }

        private IEnumerator SpawnTextCoroutine(string text, float delay, float displayDuration, bool success)
        {
            yield return new WaitForSeconds(delay);

            GameObject face = Instantiate(success ? faceSuccessPrefab : faceFailurePrefab, Vector2.zero + (Random.insideUnitCircle.normalized * 2.5f), Quaternion.identity);
            face.transform.rotation = Vector2.right.AddRandomAngleToDirection(-25.0f, 25.0f).ToRotation();
            
            GameObject textObject = Instantiate(textPrefab, transform.position, Quaternion.identity, transform);

            TextMeshProUGUI textMeshProUGUI = textObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            textMeshProUGUI.text = text;

            textObject.transform.rotation = Vector2.right.AddRandomAngleToDirection(-25.0f, 25.0f).ToRotation();

            Destroy(textObject, displayDuration);
            Destroy(face, displayDuration);
        }
    }
}
