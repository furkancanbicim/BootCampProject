using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    //Variables for the rotational speed of the planets and the rotational speed of the sun.
    [SerializeField] float rotateSpeed, rotateAroundSpeed;
    //Game manager script for get management.
    [SerializeField] GameManager gameManager;
    float travelledDistance, countTour;
    void Update()
    {
        //It makes the planet revolve around itself.
        transform.Rotate(0, (rotateAroundSpeed / gameManager.simulationTime) * Time.deltaTime, 0);
        //It makes the planet revolve around the sun.
        transform.RotateAround(gameManager.sun.transform.position, Vector3.up, (rotateSpeed / gameManager.simulationTime) * Time.deltaTime);

        //Calculates the travelled distance by planet with speed multiplier.
        travelledDistance += (rotateSpeed / gameManager.simulationTime) * Time.deltaTime;
        if (travelledDistance > 360)
        {
            //If any planet completed its tour, resets the distance travelled.
            travelledDistance = 0f;
            countTour++;
            Debug.Log(gameObject.name + " has completed its tour. Tours count: "+countTour);
        }
    }
}
