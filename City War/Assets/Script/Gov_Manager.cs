using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gov_Manager : MonoBehaviour
{
    public static Gov_Manager instance;
    public int money = 150;
    public int incHouse = 50;
    public int incCommercial = 100;
    public int incIndustrial = 300;

    Gov_Interface ui;
    Gov_Player player;

    private void Awake()
    {
        instance = this;
        ui = GetComponent<Gov_Interface>();
        player = GetComponent<Gov_Player>();
    }

    private void Start()
    {
        //Starting UI
        ui.togglePause(0);
        ui.setMoneyText(money);
    }

    public void startShop()
    {
        //Interface
        ui.togglePause(0);
        ui.setPanelParent(true);
        ui.togglePanel(false);
        ui.setHeader(false);
        player.toggleStateButton(false);
        player.updateInventoryCounter();
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
        player.updateInventoryCounter();
    }

    public void applySalary()
    {
        money += getIncome();
        ui.setMoneyText(money);
    }

    public void deductMoney(int val) { money -= val; }
    public void addMoney(int val) { money += val; }

    int getIncome()
    {
        int income = (incHouse * BuildingManager.instance.getTotalBuildingByType(Building.buildingTypes.House)) +
            (incCommercial * BuildingManager.instance.getTotalBuildingByType(Building.buildingTypes.Commercial)) +
            (incIndustrial * BuildingManager.instance.getTotalBuildingByType(Building.buildingTypes.Industrial));

        return income;
    }
}
