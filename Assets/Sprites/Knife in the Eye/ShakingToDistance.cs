using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingToDistance : MonoBehaviour
{
    public RandomShaking shakingScript;

    public Transform itemToCompute;
    public Transform startingPosition;
    public Transform endingPosition;

    public float shakeMax;

    void Update()
    {
        shakingScript.shakePower = Tools.NormalizeValueInRange(Vector3.Distance(itemToCompute.position, endingPosition.position), Vector3.Distance(startingPosition.position, endingPosition.position), 0, 0, shakeMax);
    }

    //plus tu te rapproches de endingPosition - plus shaking augmente
    //plus tu t'éloignes, plus il est faible

    //shaking = blabla / distance ?

    //Distance entre starting & ending min -> 0
    //Distance entre starting & ending max -> 0.05
}
