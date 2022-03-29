using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance;
    BuildingClick activeBuilding;

    private void Awake()
    {
        instance = this;
    }

    public void overrideBuilding(BuildingClick newBuilding)
    {
        activeBuilding = newBuilding;
    }

    public void removeBuilding()
    {
        activeBuilding = null;
    }
}
