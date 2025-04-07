using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneIntro : MonoBehaviour
{
    public MouseControls mouseControlScript;

    public GameObject objectToActivate;
    public GameObject objectToDeactivate;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(mouseControlScript.noInputDuration);
        objectToActivate.SetActive(true);
        objectToDeactivate.SetActive(false);
    }
}
