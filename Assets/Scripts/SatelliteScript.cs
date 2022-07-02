using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteScript : MonoBehaviour
{
    //Variables for the rotational speed of the satellites and the rotational speed of the planet.
    [SerializeField] float rotateSpeed, rotateAroundSpeed;
    //Game object for the planet that the satellite will orbit.
    [SerializeField] GameObject planet;
    //Game manager script for get management.
    [SerializeField] GameManager gameManager;
    void Update()
    {
        //It makes the planet revolve around itself.
        transform.Rotate(0, (rotateAroundSpeed / gameManager.simulationTime) * Time.deltaTime, 0);
        //It makes the planet revolve around the planet.
        transform.RotateAround(planet.transform.position, Vector3.up, (rotateSpeed / gameManager.simulationTime) * Time.deltaTime);
    }
}
