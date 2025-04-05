using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SceneLoading
{
    public class TransitionManager : MonoBehaviour
    {
        [SerializeField] private Image blackScreen;
        [SerializeField] private GameObject oliveTransition;
        [SerializeField] private GameObject teaTransition;

        private Transform currentTransition;
        
        public IEnumerator PlayTransition(string sceneName, bool transitionIN)
        {
            Debug.Log($"Scene name : {sceneName}");
            
            if (sceneName.Equals("Bubble Tea"))
                yield return TeaTransition(transitionIN);
            else if (sceneName.Equals("Martini"))
                yield return OliveTransition(transitionIN);
            else
                yield return Tools.Fade(blackScreen, 0.7f, transitionIN);
        }
        
        private IEnumerator TeaTransition(bool transitionIN)
        {
            if (transitionIN)
            {
                Vector2 startPosition = new Vector2(0.0f, 13.0f);
                currentTransition = Instantiate(teaTransition, startPosition, Quaternion.identity, transform).transform;

                float duration = 0.7f;
                float delta = 13.0f / duration;
                while (duration >= 0.0f)
                {
                    currentTransition.position = new Vector3(0.0f, currentTransition.position.y - delta * Time.deltaTime, 0.0f);
                    yield return null;
                    duration -= Time.deltaTime;
                }
            }
            else
            {
                float duration = 0.7f;
                float delta = 13.0f / duration;
                while (duration >= 0.0f)
                {
                    currentTransition.position = new Vector3(0.0f, currentTransition.position.y + delta * Time.deltaTime, 0.0f);
                    yield return null;
                    duration -= Time.deltaTime;
                }
                Destroy(currentTransition.gameObject);
            }
        }

        private IEnumerator OliveTransition(bool transitionIN)
        {
            if (transitionIN)
            {
                Vector2 startPosition = new Vector2(-25.5f, 0.0f);
                currentTransition = Instantiate(oliveTransition, startPosition, Quaternion.identity, transform).transform;

                float duration = 0.7f;
                float delta = 25.5f / duration;
                while (duration >= 0.0f)
                {
                    currentTransition.position = new Vector3(currentTransition.position.x + delta * Time.deltaTime, 0.0f, 0.0f);
                    yield return null;
                    duration -= Time.deltaTime;
                }
            }
            else
            {
                float duration = 0.7f;
                float delta = 25.5f / duration;
                while (duration >= 0.0f)
                {
                    currentTransition.position = new Vector3(currentTransition.position.x - delta * Time.deltaTime, 0.0f, 0.0f);
                    yield return null;
                    duration -= Time.deltaTime;
                }
                Destroy(currentTransition.gameObject);
            }
        }
    }
}
