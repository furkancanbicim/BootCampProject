using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelectScript : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 10f)]
    float cameraMercuryDistance, cameraVenusDistance, cameraEarthDistance, cameraMarsDistance;
    bool canSelectPlanet;
    Vector3 distance;

    private void Start()
    {
        canSelectPlanet = true;
    }

    private void OnEnable()
    {
        EventManager.planetSelected += ChangeCanSelect;
        EventManager.planetUnSelected += ChangeCanSelect;
    }
    private void OnDisable()
    {
        EventManager.planetSelected -= ChangeCanSelect;
        EventManager.planetUnSelected -= ChangeCanSelect;
    }
    void ChangeCanSelect()
    {
        canSelectPlanet = !canSelectPlanet;
    }
    void ChangeCanSelect(Transform transform, Vector3 Pos)
    {
        canSelectPlanet = !canSelectPlanet;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0)&&canSelectPlanet)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit))
            {
                switch(hit.transform.name)
                {
                    case "Mercury":
                        distance = new Vector3(cameraMercuryDistance,0.5f,0);
                        break;
                    case "Venus":
                        distance = new Vector3(cameraVenusDistance, 0.5f, 0);
                        break;
                    case "Earth":
                        distance = new Vector3(cameraEarthDistance, 0.5f, 0);
                        break;
                    case "Mars":
                        distance = new Vector3(cameraMarsDistance, 0.5f, 0);
                        break;
                }
                if (hit.transform.name != "Sun" && hit.transform.name != "Moon")
                {

                    EventManager.planetSelected?.Invoke(hit.transform, distance);
                }
                     
            }
        }
    }
}
