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
        [SerializeField] private GameObject computerTransition;

        private Transform currentTransition;
        private SpriteRenderer currentSpriteRenderer;
        
        public IEnumerator PlayTransition(string sceneName, bool transitionIN)
        {
            Debug.Log($"Scene name : {sceneName}");
            
            if (sceneName.Equals("Bubble Tea"))
                yield return TeaTransition(transitionIN);
            else if (sceneName.Equals("Martini"))
                yield return OliveTransition(transitionIN);
            else if (sceneName.Equals("Computer"))
                yield return ComputerTransition(transitionIN);
            else
                yield return Tools.Fade(blackScreen, 0.7f, transitionIN);
        }

        private IEnumerator ComputerTransition(bool transitionIn)
        {
            if (transitionIn)
            {
                Vector2 startPosition = new Vector2(0.0f, -5.1f);
                currentSpriteRenderer = Instantiate(computerTransition, startPosition, Quaternion.identity, transform).GetComponent<SpriteRenderer>();

                currentSpriteRenderer.size = new Vector2(10.0f, 0.0f);
                
                float duration = 0.7f;
                float delta = 5.625f / duration;
                while (duration >= 0.0f)
                {
                    currentSpriteRenderer.size = new Vector2(10.0f, currentSpriteRenderer.size.y + delta * Time.deltaTime);
                    yield return null;
                    duration -= Time.deltaTime;
                }
            }
            else
            {
                float duration = 0.7f;
                float delta = 5.625f / duration;
                while (duration >= 0.0f)
                {
                    currentSpriteRenderer.size = new Vector2(10.0f, currentSpriteRenderer.size.y - delta * Time.deltaTime);
                    yield return null;
                    duration -= Time.deltaTime;
                }
            }
        }

        private IEnumerator TeaTransition(bool transitionIn)
        {
            if (transitionIn)
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

        private IEnumerator OliveTransition(bool transitionIn)
        {
            if (transitionIn)
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
