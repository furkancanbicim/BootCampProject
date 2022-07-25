using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawnScript : MonoBehaviour
{
    [SerializeField] GameObject[] meteorSpawnPoints;
    [SerializeField] GameObject meteorObj;
    [SerializeField]
    GameObject[] planets;
    GameObject spawnedMeteor;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            
           spawnedMeteor = Instantiate(meteorObj, meteorSpawnPoints[Random.RandomRange(0, meteorSpawnPoints.Length - 1)].transform);
        }
    }
}
