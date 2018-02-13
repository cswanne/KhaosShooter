using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteriods : MonoBehaviour {

    public GameObject[] asteroidTypes;
    public bool spawn = false;
    private Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    // Use this for initialization
    void Start()
    {
        spawn = true;
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        GameObject hazard;

        while (spawn == true) {
            for (int i = 0; i < hazardCount; i++) {
                if (!spawn) continue;
                Vector3 spawnPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                hazard = asteroidTypes[Mathf.RoundToInt(Random.Range(0, 3))];
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}
