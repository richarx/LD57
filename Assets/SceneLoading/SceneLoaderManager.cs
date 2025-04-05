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

        public void LoadNextScene()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            
            currentSceneIndex += 1;

            if (currentSceneIndex >= scenes.Count)
                currentSceneIndex = 0;
            
            if (scenes[currentSceneIndex].SceneName.Equals(currentSceneName))
                currentSceneIndex += 1;

            StartCoroutine(LoadNextSceneCoroutine(scenes[currentSceneIndex].SceneName));
        }

        private IEnumerator LoadNextSceneCoroutine(string sceneName)
        {
            yield return transitionManager.PlayTransition(sceneName, true);
            
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneName);
            yield return new WaitUntil(() => loadingOperation.isDone);
            
            yield return transitionManager.PlayTransition(sceneName, false);
        }
    }
}
