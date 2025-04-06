using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoading
{
    public class SceneLoaderManager : MonoBehaviour
    {
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
#endif

        public void LoadNextScene(float delay)
        {
            StartCoroutine(LoadNextSceneCoroutine(delay));
        }

        private IEnumerator LoadNextSceneCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            
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
