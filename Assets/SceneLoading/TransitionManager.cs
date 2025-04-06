using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SceneLoading
{
    public class TransitionManager : MonoBehaviour
    {
        [SerializeField] private float transitionDuration;
        [SerializeField] private Image blackScreen;
        [SerializeField] private GameObject oliveTransition;
        [SerializeField] private GameObject teaTransition;
        [SerializeField] private GameObject computerTransition;
        [SerializeField] private GameObject mouthTransition;
        [SerializeField] private GameObject yourtTransition;
        [SerializeField] private GameObject socksTransition;
        [SerializeField] private GameObject ashesTransition;

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
            else if (sceneName.Equals("Glotte"))
                yield return MouthTransition(transitionIN);
            else if (sceneName.Equals("Yogurt"))
                yield return YogurtTransition(transitionIN);
            else if (sceneName.Equals("Nail in the Foot"))
                yield return SaucetteTransition(transitionIN);
            else if (sceneName.Equals("Mom's Ashes"))
                yield return AshesTransition(transitionIN);
            else
                yield return Tools.Fade(blackScreen, 0.7f, transitionIN);
        }

        private IEnumerator AshesTransition(bool transitionIn)
        {
            if (transitionIn)
            {
                Vector2 startPosition = new Vector2(-0.36f, -9.81f);
                currentTransition = Instantiate(ashesTransition, startPosition, Quaternion.identity, transform).transform;
                yield return new WaitForSeconds(Mathf.Max(0.5f, transitionDuration));
            }
            else
            {
                currentTransition.GetComponent<Animator>().Play("CloudsOpen");
                yield return new WaitForSeconds(Mathf.Max(0.5f, transitionDuration));
                Destroy(currentTransition.gameObject);
            }
        }

        private IEnumerator MouthTransition(bool transitionIn)
        {
            if (transitionIn)
            {
                currentTransition = Instantiate(mouthTransition, Vector2.zero, Quaternion.identity, transform).transform;

                Transform mouthUp = currentTransition.GetChild(0);
                Transform mouthDown = currentTransition.GetChild(1);
                
                float duration = transitionDuration;
                float deltaUp = 6.5f / duration;
                float deltaDown = 8.5f / duration;
                while (duration >= 0.0f)
                {
                    mouthUp.position = new Vector3(0.0f, mouthUp.position.y - deltaUp * Time.deltaTime, 0.0f);
                    mouthDown.position = new Vector3(0.0f, mouthDown.position.y + deltaDown * Time.deltaTime, 0.0f);
                    yield return null;
                    duration -= Time.deltaTime;
                }
            }
            else
            {
                Transform mouthUp = currentTransition.GetChild(0);
                Transform mouthDown = currentTransition.GetChild(1);
                
                float duration = transitionDuration;
                float deltaUp = 6.5f / duration;
                float deltaDown = 8.5f / duration;
                while (duration >= 0.0f)
                {
                    mouthUp.position = new Vector3(0.0f, mouthUp.position.y + deltaUp * Time.deltaTime, 0.0f);
                    mouthDown.position = new Vector3(0.0f, mouthDown.position.y - deltaDown * Time.deltaTime, 0.0f);
                    yield return null;
                    duration -= Time.deltaTime;
                }
                Destroy(currentTransition.gameObject);
            }
        }

        private IEnumerator ComputerTransition(bool transitionIn)
        {
            if (transitionIn)
            {
                Vector2 startPosition = new Vector2(0.0f, -5.1f);
                currentSpriteRenderer = Instantiate(computerTransition, startPosition, Quaternion.identity, transform).GetComponent<SpriteRenderer>();

                currentSpriteRenderer.size = new Vector2(10.0f, 0.0f);
                
                float duration = transitionDuration;
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
                float duration = transitionDuration;
                float delta = 5.625f / duration;
                while (duration >= 0.0f)
                {
                    currentSpriteRenderer.size = new Vector2(10.0f, currentSpriteRenderer.size.y - delta * Time.deltaTime);
                    yield return null;
                    duration -= Time.deltaTime;
                }
                Destroy(currentSpriteRenderer.gameObject);
            }
        }
        
        private IEnumerator YogurtTransition(bool transitionIn)
        {
            if (transitionIn)
            {
                Vector2 startPosition = new Vector2(0.0f, 13.0f);
                currentTransition = Instantiate(yourtTransition, startPosition, Quaternion.identity, transform).transform;

                float duration = transitionDuration;
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
                float duration = transitionDuration;
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

        private IEnumerator TeaTransition(bool transitionIn)
        {
            if (transitionIn)
            {
                Vector2 startPosition = new Vector2(0.0f, 13.0f);
                currentTransition = Instantiate(teaTransition, startPosition, Quaternion.identity, transform).transform;

                float duration = transitionDuration;
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
                float duration = transitionDuration;
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
        
        private IEnumerator SaucetteTransition(bool transitionIn)
        {
            if (transitionIn)
            {
                Vector2 startPosition = new Vector2(20.5f, 0.0f);
                currentTransition = Instantiate(socksTransition, startPosition, Quaternion.identity, transform).transform;

                float duration = transitionDuration;
                float delta = 20.5f / duration;
                while (duration >= 0.0f)
                {
                    currentTransition.position = new Vector3(currentTransition.position.x - delta * Time.deltaTime, 0.0f, 0.0f);
                    yield return null;
                    duration -= Time.deltaTime;
                }
            }
            else
            {
                float duration = transitionDuration;
                float delta = 20.5f / duration;
                while (duration >= 0.0f)
                {
                    currentTransition.position = new Vector3(currentTransition.position.x + delta * Time.deltaTime, 0.0f, 0.0f);
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

                float duration = transitionDuration;
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
                float duration = transitionDuration;
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
