using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public enum buildingStates { Normal, Captured, Stronghold, Bombed }
    public buildingStates state;

    public enum buildingTypes { House, Commercial, Industrial, Special }
    public buildingTypes type;

    public buildingStates getState() { return state; }
    public void setState(buildingStates newState) { state = newState; }
    public buildingTypes getType() { return type; }
}
