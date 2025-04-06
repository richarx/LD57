using System.Collections.Generic;
using UnityEngine;

public class GameObjectSwapper : MonoBehaviour
{
    [SerializeField] List<GameObject> objects;
    [SerializeField] float frameDuration = 0.2f;

    void Update()
    {
        if (objects.Count == 0)
            return;

        int index = (int)(Time.time / frameDuration) % objects.Count;

        for (int i = 0; i < objects.Count; i++)
            objects[i].SetActive(i == index);
    }
}
