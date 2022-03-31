using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedTargetTerrorist : MonoBehaviour
{
    //This script is for AI enemies only.
    public static AutomatedTargetTerrorist instance;


    public Building getSingleTargetByType(Building.buildingTypes type)
    {
        Building[] potentialTargets = FindObjectsOfType<Building>();
        List<Building> suitableTargets = new List<Building>();

        for(int i = 0; i < potentialTargets.Length; i++)
        {
            if(potentialTargets[i].getState() != Building.buildingStates.Bombed && potentialTargets[i].getState() != Building.buildingStates.Captured
                && potentialTargets[i].getState() != Building.buildingStates.Stronghold && potentialTargets[i].getState() != Building.buildingStates.BombTrapped)
            {
                if (potentialTargets[i].getType() == type)
                {
                    suitableTargets.Add(potentialTargets[i]);
                }
            }           
        }

        Building chosenTarget = suitableTargets[Random.Range(0, suitableTargets.Count)];

        return chosenTarget;
    }

    public Building getSingleRandomBuilding()
    {
        Building[] potentialTargets = FindObjectsOfType<Building>();
        List<Building> suitableTargets = new List<Building>();

        for (int i = 0; i < potentialTargets.Length; i++)
        {
            if (potentialTargets[i].getState() != Building.buildingStates.Bombed && potentialTargets[i].getState() != Building.buildingStates.Captured
                && potentialTargets[i].getState() != Building.buildingStates.Stronghold && potentialTargets[i].getState() != Building.buildingStates.BombTrapped)
            {
                    suitableTargets.Add(potentialTargets[i]);          
            }
        }

        Building chosenTarget = suitableTargets[Random.Range(0, suitableTargets.Count)];

        return chosenTarget;
    }

    //public Building[] getMultipleRandomBuildings(int amt)
    //{
    //    Building[] potentialTargets = FindObjectsOfType<Building>();
    //    List<Building> suitableTargets = new List<Building>();

    //    for (int i = 0; i < potentialTargets.Length; i++)
    //    {
    //        if (potentialTargets[i].getState() != Building.buildingStates.Bombed && potentialTargets[i].getState() != Building.buildingStates.Captured
    //            && potentialTargets[i].getState() != Building.buildingStates.Stronghold && potentialTargets[i].getState() != Building.buildingStates.BombTrapped)
    //        {
    //            suitableTargets.Add(potentialTargets[i]);
    //        }
    //    }

    //    List<Building> chosenTargets = new List<Building>();

    //    for(int i = 0; i < suitableTargets.Count; i++)
    //    {
    //        int rand = Random.Range(0, 2);
            
    //        if()
    //    }

    //    Building[] targets = new Building[chosenTargets.Count];
        
    //    //Convert to array
    //    for(int i = 0; i < targets.Length; i++)
    //    {
    //        targets[i] = chosenTargets[i];
    //    }

    //    return targets;
    //}
}
