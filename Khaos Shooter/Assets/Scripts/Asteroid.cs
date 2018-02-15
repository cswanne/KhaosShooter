using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public GameObject[] asteroidTypes;
    
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private bool spawn;
    private BoxCollider bc;

    public void startStop()
    {
        spawn = !spawn;
        if (spawn) {
            StartCoroutine(SpawnWaves());
        };

    }

    void destroyObjects(string tagName)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tagName);
        foreach (GameObject obj in objects)
            GameObject.Destroy(obj);
    }

    private void Start()
    {
        BoxCollider[] bcs = transform.GetComponentsInChildren<BoxCollider>();
        bc = bcs[1];
    }

    void Update()
    {
        if (spawn == false) {
            destroyObjects("Enemy");
            destroyObjects("Boulder");
            //destroyObjects("MiscObjects");
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        GameObject hazard;

        while (spawn == true) {
            for (int i = 0; i < hazardCount; i++) {
                if (!spawn) continue;
                Vector3 spawnPosition = new Vector3(150/*bc.transform.parent.position.x*/, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                hazard = asteroidTypes[Mathf.RoundToInt(Random.Range(0, 3))];
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}

