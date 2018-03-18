using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    public GameObject[] asteroidTypes;

    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private bool spawn;
    private float spawnX = 0f;
    private float spawnYBottom = 0;
    private float spawnYTop = 0;


    public void startStop()
    {
        spawn = !spawn;
        if (spawn) {
            StartCoroutine(SpawnWaves());
        } 
    }

    private void Start()
    {
        Transform obj = transform.GetChild(1);
        if (obj.name == "End") {
            spawnX = obj.transform.position.x + 50;
            BoxCollider2D col = obj.GetComponent<BoxCollider2D>();
            spawnYBottom = -((col.size.y - obj.position.y) / 2.2f);
            spawnYTop = (col.size.y - obj.position.y) - 4;
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        GameObject hazard;
        while (spawn == true) {
            for (int i = 0; i < hazardCount; i++) {
                if (!spawn) continue;
                Vector3 spawnPosition = new Vector3(spawnX, Random.Range(spawnYTop, spawnYBottom), transform.position.z);
                Quaternion spawnRotation = Quaternion.identity;
                hazard = asteroidTypes[Mathf.RoundToInt(Random.Range(0, 3))];
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}


