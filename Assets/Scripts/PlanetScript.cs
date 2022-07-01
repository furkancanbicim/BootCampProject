using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    [SerializeField] float rotateSpeed, rotateAroundSpeed;
    [SerializeField] GameManager gameManager;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, (rotateAroundSpeed / gameManager.simulationTime) * Time.deltaTime, 0);
        transform.RotateAround(gameManager.sun.transform.position, Vector3.up, (rotateSpeed / gameManager.simulationTime) * Time.deltaTime);
    }
}
