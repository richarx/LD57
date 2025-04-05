using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public float minInterval;
    public float maxInterval;

    public List<GameObject> gameObjectsToActivateOnSuccess;
    public List<GameObject> gameObjectsToDeactivateOnSuccess;

    public List<GameObject> gameObjectsToActivateOnFailureTooMuch;
    public List<GameObject> gameObjectsToDeactivateOnFailureTooMuch;

    public List<GameObject> gameObjectsToActivateOnFailureNotEnough;
    public List<GameObject> gameObjectsToDeactivateOnFailureNotEnough;
    
    void Start()
    {
        PowerController.OnEndSliding.AddListener((sliderValue) => TriggerEndLevel(sliderValue));
    }

    private void TriggerEndLevel(float sliderValue)
    {
        if (sliderValue < minInterval)
            TriggerFailureNotEnough();
        else if (sliderValue <= maxInterval)
            TriggerSuccess();
        else
            TriggerFailureTooMuch();
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
    }

    //check la valeur du slider
    //si valeur est dans le bon intervalle : success
    //sinon : failure

    //écoute le startSliding pour lancer les animations etc
    //écoute le stopSliding pour arrêter les animations etc
}
