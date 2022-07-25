using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    [Header("Planet Status Panels")]
    [SerializeField]
    GameObject mercuryPanel;
    [SerializeField]
    GameObject venusPanel;
    [SerializeField]
    GameObject earthPanel;
    [SerializeField]
    GameObject marsPanel;
    private void OnEnable()
    {
        EventManager.planetSelected += SetPlanet;
        EventManager.planetUnSelected += CloseAllPanels;
    }
    private void OnDisable()
    {
        EventManager.planetSelected -= SetPlanet;
        EventManager.planetUnSelected -= CloseAllPanels;
    }

    public void SetPlanet(Transform planet, Vector3 pos)
    {
        switch (planet.name)
        {
            case "Mercury":
                CloseAllPanels();
                mercuryPanel.SetActive(true);
                break;
            case "Venus":
                CloseAllPanels();
                venusPanel.SetActive(true);
                break;
            case "Earth":
                CloseAllPanels();
                earthPanel.SetActive(true);
                break;
            case "Mars":
                CloseAllPanels();
                marsPanel.SetActive(true);
                break;
        }
        Debug.Log(planet.name);
    }

    void CloseAllPanels()
    {
        mercuryPanel.SetActive(false);
        venusPanel.SetActive(false);
        earthPanel.SetActive(false);
        marsPanel.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
