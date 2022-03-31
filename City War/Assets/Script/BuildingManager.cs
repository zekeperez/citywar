using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance;

    BuildingClick activeBuilding;

    //orbit
    bool orbit = false;
    float orbitSpeed = 3f;

    RTS_Camera cam;
    Camera mainCam;

    private void Awake()
    {
        instance = this;

        cam = FindObjectOfType<RTS_Camera>();
        mainCam = Camera.main;
    }


    public void overrideBuilding(BuildingClick newBuilding)
    {
        if(activeBuilding != null) activeBuilding.toggleOutline(false);
        activeBuilding = newBuilding;
        setCameraTarget(activeBuilding.transform);
    }

    public void setCameraTarget(Transform buildingTransform)
    {
        if(buildingTransform == null)
        {
            cam.targetFollow = null;
            mainCam.fieldOfView = 60;
        }
        else
        {
            cam.transform.LookAt(buildingTransform.position);
            cam.targetFollow = buildingTransform;

            Vector3 dir = buildingTransform.position - cam.transform.position;
            Ray ray = new Ray(buildingTransform.position, dir);
            float distance = Vector3.Distance(buildingTransform.position, cam.transform.position);
            Vector3 offset = ray.GetPoint(1f);

            cam.targetOffset = offset;

            mainCam.fieldOfView = 30;

            Invoke("allowOrbit", 3f);
        }     
    }

    void allowOrbit()
    {
        cam.targetFollow = null;
        orbit = true;
    }

    private void Update()
    {
        if(activeBuilding != null)
        {
            cam.transform.LookAt(activeBuilding.transform.position);

            if (orbit)
            {
                cam.transform.Translate(Vector3.right * Time.deltaTime * orbitSpeed);              
            }

            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
            {
                removeBuilding();
            }
        }
    }

    public void removeBuilding() //called from buildingclick.cs
    {
        cam.gameObject.transform.rotation = 
            Quaternion.Euler(new Vector3(45, cam.gameObject.transform.rotation.y, cam.gameObject.transform.rotation.z));

        setCameraTarget(null);
        activeBuilding = null;
        orbit = false;
    }

    #region building counter
    public int getTotalBuildingByType(Building.buildingTypes buildingType)
    {
        Building[] buildings = GameObject.FindObjectsOfType<Building>();
        List<Building> totalBuildings = new List<Building>();

        for(int i = 0; i < buildings.Length; i++)
        {
            if(buildings[i].getType() == buildingType && buildings[i].getState() == Building.buildingStates.Normal)
            {
                totalBuildings.Add(buildings[i]);
            }
        }

        return totalBuildings.Count;
    }

    public int getTotalBuildingByState(Building.buildingStates buildingState)
    {
        Building[] buildings = GameObject.FindObjectsOfType<Building>();
        List<Building> totalBuildings = new List<Building>();

        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i].getState() == buildingState)
            {
                totalBuildings.Add(buildings[i]);
            }
        }

        return totalBuildings.Count;
    }

    public int getTotalBuildingByTypeAndState(Building.buildingTypes buildingType, Building.buildingStates buildingState)
    {
        Building[] buildings = GameObject.FindObjectsOfType<Building>();
        List<Building> totalBuildings = new List<Building>();

        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i].getType() == buildingType && buildings[i].getState() == buildingState)
            {
                totalBuildings.Add(buildings[i]);
            }
        }

        return totalBuildings.Count;
    }


    //public int getTotalPopulation()
    //{
    //    Building[] buildings = FindObjectsOfType<Building>();

    //    int totalPopulation = 0;
        
    //    for(int i = 0; i < buildings.Length; i++)
    //    {
    //        totalPopulation += buildings[i].getPopulation();
    //    }

    //    return totalPopulation;
    //}
    #endregion
}
