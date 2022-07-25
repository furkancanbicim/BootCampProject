using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    CanvasManager canvasManager;
    //Variable for simulation time. It can be change from unity inspector.
    [Range(1f, 10f)]
    public float simulationTime;
    //Variable to be able to summon the sun from other scripts.
    public GameObject sun;
   
    //Variable for sun's rotate speed of itself.
    [SerializeField] float sunRotateSpeed;
    private void Update()
    {
        ////It makes the sun revolve around itself.
        //sun.transform.Rotate(0, (sunRotateSpeed / simulationTime) * Time.deltaTime, 0);
        //if(Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if(Physics.Raycast(ray, out hit))
        //    {
        //        canvasManager.SetPlanet(hit.transform.gameObject);
        //    }
        //}
    }
}
