using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VictoryText
{
    public class EndTextManager : MonoBehaviour
    {
        public enum ScoreState
        {
            Success,
            TooMuch,
            NotEnough
        }
        
        [SerializeField] private GameObject textPrefab;
        [SerializeField] private GameObject faceSuccessPrefab;
        [SerializeField] private GameObject faceFailurePrefab;
        [SerializeField] private GameObject faceFailureMidPrefab;

        public static EndTextManager instance;

        private void Awake()
        {
            instance = this;
        }

        public void SpawnText(string text, float delay, float displayDuration, ScoreState state)
        {
            StartCoroutine(SpawnTextCoroutine(text, delay, displayDuration, state));
        }

        private IEnumerator SpawnTextCoroutine(string text, float delay, float displayDuration, ScoreState state)
        {
            yield return new WaitForSeconds(delay);

            GameObject face = Instantiate(GetFace(state), Vector2.zero + (Random.insideUnitCircle.normalized * 2.5f), Quaternion.identity);
            face.transform.rotation = Vector2.right.AddRandomAngleToDirection(-25.0f, 25.0f).ToRotation();
            
            GameObject textObject = Instantiate(textPrefab, transform.position, Quaternion.identity, transform);

            TextMeshProUGUI textMeshProUGUI = textObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            textMeshProUGUI.text = text;

            textObject.transform.rotation = Vector2.right.AddRandomAngleToDirection(-25.0f, 25.0f).ToRotation();

            Destroy(textObject, displayDuration);
            Destroy(face, displayDuration);
        }

        private GameObject GetFace(ScoreState state)
        {
            switch (state)
            {
                case ScoreState.Success:
                    return faceSuccessPrefab;
                case ScoreState.TooMuch:
                    return faceFailurePrefab;
                case ScoreState.NotEnough:
                    return faceFailureMidPrefab;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}
