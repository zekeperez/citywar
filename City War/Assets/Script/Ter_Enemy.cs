using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ter_Enemy : MonoBehaviour
{
    [Header("Economy")]
    public int money = 250;
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

    //Targets
    AutomatedTargetTerrorist targeting;
    List<Building> bombTargets = new List<Building>();
    List<Building> trapTargets = new List<Building>();

    private void Awake()
    {
        targeting = GetComponent<AutomatedTargetTerrorist>();
        if(targeting == null) { Debug.LogError("Terrorist AI has no targeting system."); }
    }

    public void startTurn(bool isTurn)
    {
        Debug.Log("TER_AI: Starting Turn (" + isTurn + ")");
        applySalary();
        if (isTurn) { automateTurn(); }
        else { automateShopping(); }

        endTurn();
        //Invoke("endTurn", Random.Range(3, 5));
    }

    public void chooseStronghold() //dev note, strongholds dont affect indu-comm relationships
    {
        Building randomBuilding = targeting.getSingleRandomBuilding();
        randomBuilding.setState(Building.buildingStates.Stronghold);
    }

    void automateTurn()
    {
        int rand = Random.Range(0, 2);
        Debug.Log("TER_AI: Playing - " + rand);

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

            case 5:
                Debug.Log("TER_AI: Abstaining this round.");
                break;
        }

    }

    void automateShopping()
    {
        int randAction = Random.Range(0, 2);
        Debug.Log("TER_AI: Shopping - " + randAction);

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

            case 4:
                Debug.Log("TER_AI: Abstaining this round.");
                break;
        }
    }


    void endTurn()
    {
        Debug.Log("TER_AI: Completed Action. Waiting for Player to finish.");

        //GameManager.instance.endTurn();
    }

    public List<Building> getBombTargets() { return bombTargets; }

    public List<Building> getTrapTargets() { return trapTargets; }

    public void resetTargetList()
    {
        for(int i = 0; i < bombTargets.Count; i++) { bombTargets.RemoveAt(0); }
        for(int i = 0; i < trapTargets.Count; i++) { trapTargets.RemoveAt(0); }
    }


    #region AI actions

    #region action
    void useAllBombs(Building.buildingTypes type)
    {
        while(bombStorage > 0)
        {
            useSingleBomb(type);
        }
    }

    void useAllBombs()
    {
        while (bombStorage > 0)
        {
            useSingleBomb();
        }
    }

    void useSingleBomb(Building.buildingTypes type)
    {
        if(bombStorage > 0)
        {
            Building targetBuilding = targeting.getSingleTargetByType(type);
            bombTargets.Add(targetBuilding);
            //targetBuilding.bombBuilding();

            bombStorage--;
        }     
    }

    void useSingleBomb()
    {
        if (bombStorage > 0)
        {
            Building targetBuilding = targeting.getSingleRandomBuilding();
            bombTargets.Add(targetBuilding);
            //targetBuilding.bombBuilding();

            bombStorage--;
        }
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
