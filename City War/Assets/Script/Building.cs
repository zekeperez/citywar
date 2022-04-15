using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public enum buildingStates { Normal, Captured, Stronghold, Bombed, Trapped, BombTrapped, Defended }
    public buildingStates state;

    public enum buildingTypes { House, Commercial, Industrial, Special }
    public buildingTypes type;

    public buildingStates getState() { return state; }
    public void setState(buildingStates newState) { state = newState; }
    public buildingTypes getType() { return type; }

    public int population;
    public int getPopulation() { return population; }

    BuildingMat mat;
    Outline outline;

    private void Awake()
    {
        mat = GetComponent<BuildingMat>();
        outline = GetComponent<Outline>();

        //set population
        switch (type)
        {
            case buildingTypes.House:
                population = Random.Range(1, 10);
                break;

            case buildingTypes.Commercial:
                population = Random.Range(50, 500);
                break;

            case buildingTypes.Industrial:
                population = Random.Range(100, 750);
                break;

            case buildingTypes.Special:
                population = Random.Range(10, 1000);
                break;
        }
    }

    public void bombBuilding()
    {  
        setState(buildingStates.Bombed);
        mat.setMat("bombed");
        outline.enabled = false;
    }

    public void resetBuilding()
    {
        setState(buildingStates.Normal);
        mat.setMat("normal");
    }
}
