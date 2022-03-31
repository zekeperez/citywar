using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ter_Enemy : MonoBehaviour
{
    [Header("Economy")]
    public int money;
    public int capturedHouseInc = 150;
    public int capturedCommercialInc = 250;
    public int capturedIndustrialInc = 500;
    public int strongholdInc = 750;

    [Header("Pricing")]
    public int bombPrice = 300;
    public int bombTrapPrice = 550;

    [Header("Inventory")]
    public int bombStorage;
    public int bombTrapStorage;

    AutomatedTargetTerrorist targeting;

    private void Awake()
    {
        targeting = GetComponent<AutomatedTargetTerrorist>();
        if(targeting == null) { Debug.LogError("Terrorist AI has no targeting system."); }
    }

    public void startTurn()
    {
        applySalary();
        automateTurn();
    }

    void automateTurn()
    {
        int rand = Random.Range(0, 5);

        switch (rand)
        {
            case 0: //Mass bombing
                int rand2 = Random.Range(0, 4);
                switch (rand2)
                {
                    case 0:
                        useAllBombs(Building.buildingTypes.House);
                        break;

                    case 1:
                        useAllBombs(Building.buildingTypes.Commercial);
                        break;

                    case 2:
                        useAllBombs(Building.buildingTypes.Industrial);
                        break;

                    case 3:
                    default:
                        useAllBombs();
                        break;
                }              
                break;

            case 1: //Single bombing
                int rand3 = Random.Range(0, 5);
                switch (rand3)
                {
                    case 0:
                        useSingleBomb(Building.buildingTypes.House);
                        break;

                    case 1:
                        useSingleBomb(Building.buildingTypes.Commercial);
                        break;

                    case 2:
                        useSingleBomb(Building.buildingTypes.Industrial);
                        break;

                    case 3:
                    default:
                        useSingleBomb();
                        break;

                    case 4:
                        useSingleBomb(Building.buildingTypes.Special);
                        break;
                }
                break;

            case 2: //Mass Trapping
                int rand4 = Random.Range(0, 5);
                switch (rand4)
                {
                    case 0:
                        useAllTraps(Building.buildingTypes.House);
                        break;

                    case 1:
                        useAllTraps(Building.buildingTypes.Commercial);
                        break;

                    case 2:
                        useAllTraps(Building.buildingTypes.Industrial);
                        break;

                    case 3:
                    default:
                        useAllTraps();
                        break;
                }
                break;

            case 3: //Single Trapping
                int rand5 = Random.Range(0, 5);
                switch (rand5)
                {
                    case 0:
                        useSingleTrap(Building.buildingTypes.House);
                        break;

                    case 1:
                        useSingleTrap(Building.buildingTypes.Commercial);
                        break;

                    case 2:
                        useSingleTrap(Building.buildingTypes.Industrial);
                        break;

                    case 3:
                    default:
                        useSingleTrap();
                        break;

                    case 4:
                        useSingleTrap(Building.buildingTypes.Special);
                        break;
                }
                break;

            case 4: //stronghold
                convertStronghold();
                break;
        }
    }

 

    void automateShopping()
    {
        int randAction = Random.Range(0, 4);

        switch (randAction)
        {
            case 0:
            default:
                buyBomb();
                break;

            case 1:
                massBuyBombs();
                break;

            case 2:
                buyTrapBomb();
                break;

            case 3:
                massBuyTrapBombs();
                break;
        }

        // end turn is called here
    }

    #region AI actions

    #region action
    void useAllBombs(Building.buildingTypes type)
    {

    }

    void useAllBombs()
    {

    }

    void useSingleBomb(Building.buildingTypes type)
    {

    }

    void useSingleBomb()
    {

    }

    void useAllTraps()
    {

    }

    void useAllTraps(Building.buildingTypes type)
    {

    }

    void useSingleTrap(Building.buildingTypes type)
    {

    }

    void useSingleTrap()
    {

    }

    void captureBuilding(Building.buildingTypes type)
    {

    }

    void captureBuilding()
    {

    }

    void convertStronghold()
    {

    }
    #endregion

    #region buying
    public void massBuyBombs()
    {
        while(money >= bombPrice)
        {
            buyBomb();
        }
    }

    public void massBuyTrapBombs()
    {
        while(money >= bombTrapPrice)
        {
            buyTrapBomb();
        }
    }

    public void buyBomb()
    {
        if(money >= bombPrice)
        {
            money -= bombPrice;
            bombStorage++;
        }
        else
        {
            Debug.LogError("TER_AI: Tried to buy Bomb with insufficient funds!");
        }
    }

    public void buyTrapBomb()
    {
        if(money >= bombTrapPrice)
        {
            money -= bombTrapPrice;
            bombTrapStorage++;
        }
        else
        {
            Debug.LogError("TER_AI: Tried to buy Trap Bomb with insufficient funds!");
        }
    }

    #endregion

    #endregion

    #region salary

    void applySalary()
    {
        money += calculateIncome();
    }

    int calculateIncome()
    {
        int totalIncome =
            capturedHouseInc * 
            BuildingManager.instance.getTotalBuildingByTypeAndState
            (Building.buildingTypes.House, Building.buildingStates.Captured) +

            capturedCommercialInc * 
            BuildingManager.instance.getTotalBuildingByTypeAndState
            (Building.buildingTypes.Commercial, Building.buildingStates.Captured) +

            capturedIndustrialInc * 
            BuildingManager.instance.getTotalBuildingByTypeAndState
            (Building.buildingTypes.Industrial, Building.buildingStates.Captured) +

            strongholdInc * 
            BuildingManager.instance.getTotalBuildingByState(Building.buildingStates.Stronghold);

        return totalIncome;
    }

    #endregion
}