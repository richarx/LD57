using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPopDelay : MonoBehaviour
{
    public float delay;
    public GameObject balloonPieces;
    public GameObject balloon;

    private Animator animator;

    IEnumerator Start()
    {
        animator = GetComponent<Animator>();

        yield return new WaitForSeconds(delay);
        animator.Play("pop");
        balloon.SetActive(false);
        balloonPieces.SetActive(true);
    }
}
