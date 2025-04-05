using UnityEngine;

namespace Tools_and_Scripts
{
    public static class Initializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

        public static void Execute()
        {
            Debug.Log("Load Dont Destroy On Load stuff");
            Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("PERSISTOBJECTS")));
        }
    }
}
