using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VictoryText;

namespace SceneLoading
{
    public class SceneLoaderManager : MonoBehaviour
    {
        [SerializeField] private EndTextManager endTextManager;
        [SerializeField] private List<SceneField> scenes;
        
        public static SceneLoaderManager instance;

        public static bool IsTransitioning { get; private set; }

        private TransitionManager transitionManager;
        
        private int currentSceneIndex;

        private void Awake()
        {
            instance = this;
            transitionManager = GetComponent<TransitionManager>();
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                ReloadCurrentScene(0);
        }
#endif

        public void ReloadCurrentScene(float delay)
        {
            StartCoroutine(ReloadCurrentSceneCoroutine(delay));
        }

        private IEnumerator ReloadCurrentSceneCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);

            endTextManager.ClearText();

            string currentSceneName = SceneManager.GetActiveScene().name;

            IsTransitioning = true;
            yield return transitionManager.PlayTransition(currentSceneName, true);

            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(currentSceneName);
            yield return new WaitUntil(() => loadingOperation.isDone);

            yield return transitionManager.PlayTransition(currentSceneName, false);
            IsTransitioning = false;
        }

        public void LoadNextScene(float delay)
        {
            StartCoroutine(LoadNextSceneCoroutine(delay));
        }

        private IEnumerator LoadNextSceneCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            endTextManager.ClearText();
            
            string currentSceneName = SceneManager.GetActiveScene().name;
            
            currentSceneIndex += 1;

            if (currentSceneIndex >= scenes.Count)
                currentSceneIndex = 0;
            
            if (scenes[currentSceneIndex].SceneName.Equals(currentSceneName))
                currentSceneIndex += 1;

            if (currentSceneIndex >= scenes.Count)
                currentSceneIndex = 0;

            string sceneName = scenes[currentSceneIndex].SceneName;

            IsTransitioning = true;
            yield return transitionManager.PlayTransition(currentSceneName, true);
            
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneName);
            yield return new WaitUntil(() => loadingOperation.isDone);
            
            yield return transitionManager.PlayTransition(currentSceneName, false);
            IsTransitioning = false;
        }
    }
}
