using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gov_Manager : MonoBehaviour
{
    public int money = 150;
    public int incHouse = 50;
    public int incCommercial = 100;
    public int incIndustrial = 300;

    Gov_Interface ui;
    Gov_Player player;

    private void Awake()
    {
        ui = GetComponent<Gov_Interface>();
        player = GetComponent<Gov_Player>();
    }

    private void Start()
    {
        //Starting UI
        ui.togglePause(0);
    }

    public void startShop()
    {
        //Interface
        ui.togglePause(0);
        ui.setPanelParent(true);
        ui.togglePanel(false);
        ui.setHeader(false);
        player.toggleStateButton(false);
    }

    public void startTurn()
    {
        //Finance
        applySalary();

        //Interface
        ui.togglePause(0);
        ui.setPanelParent(true);
        ui.togglePanel(true);
        ui.setHeader(false);
        player.toggleStateButton(true);
    }

    public void applySalary()
    {
        money += getIncome();
    }

    int getIncome()
    {
        int income = (incHouse * BuildingManager.instance.getTotalBuildingByType(Building.buildingTypes.House)) +
            (incCommercial * BuildingManager.instance.getTotalBuildingByType(Building.buildingTypes.Commercial)) +
            (incIndustrial * BuildingManager.instance.getTotalBuildingByType(Building.buildingTypes.Industrial));

        return income;
    }
}
