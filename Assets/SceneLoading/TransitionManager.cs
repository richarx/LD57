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
        [SerializeField] private GameObject chestTransition;
        [SerializeField] private GameObject drugTransition;
        [SerializeField] private GameObject spaceTransition;
        [SerializeField] private GameObject vampireTransition;
        [SerializeField] private GameObject ampliTransition;

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
            else if (sceneName.Equals("Creepy Chest"))
                yield return ChestTransition(transitionIN);
            else if (sceneName.Equals("Drug Injection"))
                yield return DrugTransition(transitionIN);
            else if (sceneName.Equals("Barbecue"))
                yield return SpaceTransition(transitionIN);
            else if (sceneName.Equals("Vampire"))
                yield return VampireTransition(transitionIN);
            else if (sceneName.Equals("Music"))
                yield return MusicTransition(transitionIN);
            else
                yield return Tools.Fade(blackScreen, 0.7f, transitionIN);
        }

        private IEnumerator MusicTransition(bool transitionIn)
        {
            if (transitionIn)
            {
                currentTransition = Instantiate(ampliTransition, Vector2.zero, Quaternion.identity, transform).transform;
                SpriteRenderer background = currentTransition.GetChild(0).GetComponent<SpriteRenderer>();
                Animator animator = currentTransition.GetComponent<Animator>();

                yield return Tools.Fade(background, 0.5f, true);
                
                animator.Play("AmpliClose");
                yield return new WaitForSeconds(2.0f);
                
            }
            else
            {
                SpriteRenderer background = currentTransition.GetChild(0).GetComponent<SpriteRenderer>();
                Animator animator = currentTransition.GetComponent<Animator>();
                
                animator.Play("AmpliOpen");
                yield return new WaitForSeconds(1.0f);
                
                yield return Tools.Fade(background, 0.5f, false);
                Destroy(currentTransition.gameObject);
            }
        }

        private IEnumerator VampireTransition(bool transitionIn)
        {
            if (transitionIn)
            {
                currentTransition = Instantiate(vampireTransition, Vector2.zero, Quaternion.identity, transform).transform;

                SpriteRenderer background = currentTransition.GetChild(0).GetComponent<SpriteRenderer>();
                yield return Tools.Fade(background, 0.5f, true);

                Transform edouardo = currentTransition.GetChild(1);
                Transform heart_1 = currentTransition.GetChild(2);
                Transform heart_2 = currentTransition.GetChild(3);
                
                Vector3 scale = Vector3.zero;

                float duration = transitionDuration;
                float delta = 2.5f / duration;
                while (duration >= 0.0f)
                {
                    scale.x += delta * Time.deltaTime;
                    scale.y += delta * Time.deltaTime;
                    scale.z += delta * Time.deltaTime;
                    edouardo.localScale = scale;
                    heart_1.localScale = scale;
                    heart_2.localScale = scale;

                    yield return null;
                    duration -= Time.deltaTime;
                }
            }
            else
            {
                SpriteRenderer background = currentTransition.GetChild(0).GetComponent<SpriteRenderer>();

                Transform edouardo = currentTransition.GetChild(1);
                Transform heart_1 = currentTransition.GetChild(2);
                Transform heart_2 = currentTransition.GetChild(3);
                
                Vector3 scale = new Vector3(2.5f, 2.5f, 2.5f);

                float duration = transitionDuration;
                float delta = 2.5f / duration;
                while (duration >= 0.0f)
                {
                    scale.x -= delta * Time.deltaTime;
                    scale.y -= delta * Time.deltaTime;
                    scale.z -= delta * Time.deltaTime;
                    edouardo.localScale = scale;
                    heart_1.localScale = scale;
                    heart_2.localScale = scale;

                    yield return null;
                    duration -= Time.deltaTime;
                }
                
                yield return Tools.Fade(background, 0.5f, false);
                Destroy(currentTransition.gameObject);
            }
        }

        private IEnumerator SpaceTransition(bool transitionIn)
        {
            if (transitionIn)
            {
                yield return Tools.Fade(blackScreen, 0.4f, true);
                currentTransition = Instantiate(spaceTransition, Vector2.zero, Quaternion.identity, transform).transform;
                yield return Tools.Fade(blackScreen, 0.1f, false);
                yield return new WaitForSeconds(Mathf.Max(transitionDuration, 0.3f + 1.8f));
            }
            else
            {
                yield return Tools.Fade(blackScreen, 0.3f, true);
                Destroy(currentTransition.gameObject);
                yield return Tools.Fade(blackScreen, 0.4f, false);
            }
        }

        private IEnumerator DrugTransition(bool transitionIn)
        {
            if (transitionIn)
            {
                currentTransition = Instantiate(drugTransition, Vector2.zero, Quaternion.identity, transform).transform;

                Vector3 scale = Vector3.zero;
                currentTransition.localScale = scale;

                float angle = 0.0f;
                float rotationSpeed = 45.0f;
                
                float duration = transitionDuration;
                float delta = 2.5f / duration;
                while (duration >= 0.0f)
                {
                    scale.x += delta * Time.deltaTime;
                    scale.y += delta * Time.deltaTime;
                    scale.z += delta * Time.deltaTime;
                    currentTransition.localScale = scale;

                    angle += rotationSpeed;
                    currentTransition.localRotation = Vector2.right.AddAngleToDirection(angle).ToRotation();
                    
                    yield return null;
                    duration -= Time.deltaTime;
                }
            }
            else
            {
                Vector3 scale = new Vector3(2.5f, 2.5f, 2.5f);
             
                float angle = 0.0f;
                float rotationSpeed = 180.0f;
                
                float duration = transitionDuration;
                float delta = 2.5f / duration;
                while (duration >= 0.0f)
                {
                    scale.x -= delta * Time.deltaTime;
                    scale.y -= delta * Time.deltaTime;
                    scale.z -= delta * Time.deltaTime;
                    currentTransition.localScale = scale;
                    
                    angle += rotationSpeed;
                    currentTransition.localRotation = Vector2.right.AddAngleToDirection(angle).ToRotation();
                    
                    yield return null;
                    duration -= Time.deltaTime;
                }
                Destroy(currentTransition.gameObject);
            }
        }

        private IEnumerator ChestTransition(bool transitionIn)
        {
            if (transitionIn)
            {
                currentTransition = Instantiate(chestTransition, Vector2.zero, Quaternion.identity, transform).transform;

                Transform chestUp = currentTransition.GetChild(0);
                Transform chestDown = currentTransition.GetChild(1);
                
                float duration = transitionDuration;
                float deltaUp = 7.5f / duration;
                float deltaDown = 6.0f / duration;
                while (duration >= 0.0f)
                {
                    chestUp.position = new Vector3(0.0f, chestUp.position.y - deltaUp * Time.deltaTime, 0.0f);
                    chestDown.position = new Vector3(0.0f, chestDown.position.y + deltaDown * Time.deltaTime, 0.0f);
                    yield return null;
                    duration -= Time.deltaTime;
                }
            }
            else
            {
                Transform chestUp = currentTransition.GetChild(0);
                Transform chestDown = currentTransition.GetChild(1);
                
                float duration = transitionDuration;
                float deltaUp = 7.5f / duration;
                float deltaDown = 6.0f / duration;
                while (duration >= 0.0f)
                {
                    chestUp.position = new Vector3(0.0f, chestUp.position.y + deltaUp * Time.deltaTime, 0.0f);
                    chestDown.position = new Vector3(0.0f, chestDown.position.y - deltaDown * Time.deltaTime, 0.0f);
                    yield return null;
                    duration -= Time.deltaTime;
                }
                Destroy(currentTransition.gameObject);
            }
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
