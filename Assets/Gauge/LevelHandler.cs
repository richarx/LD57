using SceneLoading;
using System.Collections.Generic;
using UnityEngine;
using VictoryText;

public class LevelHandler : MonoBehaviour
{
    public bool testMode;
    public bool forceSuccess = false;
    public float delayBeforeNextScene;

    public float minInterval;
    public float maxInterval;

    public float delayBeforeText;
    public float textDuration;

    [TextArea] public string success;
    [TextArea] public string failTooMuch;
    [TextArea] public string failNotEnough;

    public List<GameObject> gameObjectsToActivateOnSuccess;
    public List<GameObject> gameObjectsToDeactivateOnSuccess;

    public List<GameObject> gameObjectsToActivateOnFailureTooMuch;
    public List<GameObject> gameObjectsToDeactivateOnFailureTooMuch;

    public List<GameObject> gameObjectsToActivateOnFailureNotEnough;
    public List<GameObject> gameObjectsToDeactivateOnFailureNotEnough;
    
    void Start()
    {
        MouseControls.OnComplete.AddListener((sliderValue) => TriggerEndLevel(sliderValue));
    }

    private void TriggerEndLevel(float sliderValue)
    {
        if (sliderValue < minInterval)
            TriggerFailureNotEnough();
        else if (sliderValue <= maxInterval)
            TriggerSuccess();
        else
            TriggerFailureTooMuch();

#if UNITY_EDITOR
        if (testMode)
            return;
#endif

        if (forceSuccess && (sliderValue < minInterval || sliderValue > maxInterval))
        {
            SceneLoaderManager.instance.ReloadCurrentScene(delayBeforeNextScene);
        }
        else
        {
            SceneLoaderManager.instance.LoadNextScene(delayBeforeNextScene);
        }
    }

    private void TriggerSuccess()
    {
        foreach (GameObject go in gameObjectsToActivateOnSuccess)
        {
            go.SetActive(true);
        }

        foreach (GameObject go in gameObjectsToDeactivateOnSuccess)
        {
            go.SetActive(false);
        }

        EndTextManager.instance.SpawnText(success, delayBeforeText, textDuration, EndTextManager.ScoreState.Success);
    }

    private void TriggerFailureTooMuch()
    {
        foreach (GameObject go in gameObjectsToActivateOnFailureTooMuch)
        {
            go.SetActive(true);
        }

        foreach (GameObject go in gameObjectsToDeactivateOnFailureTooMuch)
        {
            go.SetActive(false);
        }

        EndTextManager.instance.SpawnText(failTooMuch, delayBeforeText, textDuration, EndTextManager.ScoreState.TooMuch);
    }

    private void TriggerFailureNotEnough()
    {
        foreach (GameObject go in gameObjectsToActivateOnFailureNotEnough)
        {
            go.SetActive(true);
        }

        foreach (GameObject go in gameObjectsToDeactivateOnFailureNotEnough)
        {
            go.SetActive(false);
        }

        EndTextManager.instance.SpawnText(failNotEnough, delayBeforeText, textDuration, EndTextManager.ScoreState.NotEnough);
    }
}
