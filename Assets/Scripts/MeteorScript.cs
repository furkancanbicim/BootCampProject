using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    
    Transform targetPlanet;

    void GetTargetPlanet(Transform _targetPlanet)
    {
        targetPlanet = _targetPlanet;
    }
}
