using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeToDistance : MonoBehaviour
{
    public AudioSource soundToModify;

    public Transform itemToCompute;
    public Transform startingPosition;
    public Transform endingPosition;

    void Update()
    {
        soundToModify.volume = Tools.NormalizeValue(Vector3.Distance(itemToCompute.position, endingPosition.position), Vector3.Distance(startingPosition.position, endingPosition.position), 0);
    }
}
