using System;
using System.Collections;
using System.Collections.Generic;
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

        [SerializeField] private List<AudioClip> victorySounds;
        [SerializeField] private List<AudioClip> tooDeepSounds;
        [SerializeField] private List<AudioClip> notEnoughSounds;

        public static EndTextManager instance;

        private GameObject currentFace;
        private GameObject currentTextObject;
        

        private void Awake()
        {
            instance = this;
        }

        public void ClearText()
        {
            StopAllCoroutines();
            
            Destroy(currentFace);
            Destroy(currentTextObject);

            currentFace = null;
            currentTextObject = null;
        }

        public void SpawnText(string text, float delay, float displayDuration, ScoreState state)
        {
            StartCoroutine(SpawnTextCoroutine(text, delay, displayDuration, state));
        }

        private IEnumerator SpawnTextCoroutine(string text, float delay, float displayDuration, ScoreState state)
        {
            yield return new WaitForSeconds(delay);

            currentFace = Instantiate(GetFace(state), Vector2.zero + (Random.insideUnitCircle.normalized * 2.5f), Quaternion.identity);
            currentFace.transform.rotation = Vector2.right.AddRandomAngleToDirection(-25.0f, 25.0f).ToRotation();

            if (state == ScoreState.Success)
                SFXManager.Instance.PlayRandomSFX(victorySounds.ToArray(), 1.0f);
            else if (state == ScoreState.TooMuch)
                SFXManager.Instance.PlayRandomSFX(tooDeepSounds.ToArray(), 1.0f);
            else
                SFXManager.Instance.PlayRandomSFX(notEnoughSounds.ToArray(), 1.0f);

            currentTextObject = Instantiate(textPrefab, transform.position, Quaternion.identity, transform);

            TextMeshProUGUI textMeshProUGUI = currentTextObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            textMeshProUGUI.text = text;

            currentTextObject.transform.rotation = Vector2.right.AddRandomAngleToDirection(-25.0f, 25.0f).ToRotation();

            yield return new WaitForSeconds(displayDuration);
            
            if (currentFace != null)
                Destroy(currentFace);
            if (currentTextObject != null)
                Destroy(currentTextObject);
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
