using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneLoading
{
    public class SceneLoaderManager : MonoBehaviour
    {
        [SerializeField] private float fadeDuration;
        [SerializeField] private Image blackScreen;
        [SerializeField] private List<SceneField> scenes;
        
        public static SceneLoaderManager instance;

        private int currentSceneIndex;
        
        private void Awake()
        {
            instance = this;
        }

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
            yield return Tools.Fade(blackScreen, fadeDuration, true);
            
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneName);
            yield return new WaitUntil(() => loadingOperation.isDone);
            
            yield return Tools.Fade(blackScreen, fadeDuration, false);
        }
    }
}
