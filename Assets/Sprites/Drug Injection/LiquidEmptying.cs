using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tools_and_Scripts;
using UnityEngine;

public class LiquidEmptying : MonoBehaviour
{
    public GameObject dropPrefab;

    public Transform dropSpawnPosition;

    public float xRandomMin;
    public float xRandomMax;

    public float yRandomMin;
    public float yRandomMax;

    public float spawnDuration;
    public float spawnInterval;

    void Start()
    {
        StartCoroutine(SpawnDroplets());
    }

    private IEnumerator SpawnDroplets()
    {
        float timer = 0;
        while (timer < spawnDuration)
        {
            GameObject dropTemp = Instantiate(dropPrefab, dropSpawnPosition.position, Quaternion.identity);
            dropTemp.transform.eulerAngles = new Vector3(0, 0, Random.Range(-60, 0));
            Vector2 newForce = new Vector2(Random.Range(xRandomMin, xRandomMax), Random.Range(yRandomMin, yRandomMax));

            if (dropTemp.TryGetComponent<ImpulseForce2D>(out ImpulseForce2D component))
                component.Trigger(newForce);

            yield return new WaitForSeconds(spawnInterval);
            timer += spawnInterval;
        }
    }
}
