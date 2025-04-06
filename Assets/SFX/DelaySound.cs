using System.Collections;
using UnityEngine;

public class DelaySound : MonoBehaviour
{
    [SerializeField] private float delay;
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
    
        GetComponent<AudioSource>().Play();
    }
}
