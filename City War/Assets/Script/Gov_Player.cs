using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gov_Player : MonoBehaviour
{
    #region actions
    [Header("Action Buttons")]
    public Button[] actionButtons;
    bool[] actionInUse;
    int selectedActionButton = 99;

    public Button[] shopButtons;
    bool[] shopInUse;
    int selectedShopButton = 99;

    public Button stateButton;
    #endregion

    #region action interfaces
    [Header("Action Descriptions")]
    public Sprite[] buttonLogos;

    public GameObject actionConfirmation;
    public GameObject actionExplanation;
    public Image actionSprite;
    public Text actionConfirmationHeader;
    public Image actionConfirmationSprite;
    public Text actionHeader;
    public Text actionDescription;
    public Text[] inventoryCounter;
    public Text[] targetCounter;

    Gov_ActionStrings actionStrings;

    public Sprite[] buttonLogos_Green;

    public GameObject shopConfirmation;
    public GameObject shopExplanation;
    public Image shopSprite;
    public Text shopConfirmationHeader;
    public Image shopConfirmationSprite;
    public Text shopHeader;
    public Text shopDescription;

    //bool confirmationScreen = true;
    #endregion

    #region shop labels
    [Header("Shop Labels")]
    public int[] shopPrices;
    public Text[] shopPriceTexts;
    public Text[] inventoryTexts;
    #endregion

    int[] inventory;

    Gov_Manager manager;
    Gov_Interface ui;
    bool singlePlayer;

    #region states
    public enum playerStates { Waiting, Shopping, Targeting, Ready, Gaming, FinishedTurn }
    [Header("States")]
    public playerStates state;

    public void setState(playerStates newState) { state = newState; }
    public playerStates getState() { return state; }

    #endregion

    #region targetting
    List<Building> targetBuildings_0 = new List<Building>();
    List<Building> targetBuildings_1 = new List<Building>();
    List<Building> targetBuildings_2 = new List<Building>();
    List<Building> targetBuildings_3 = new List<Building>();
    List<Building> targetBuildings_4 = new List<Building>();
    List<Building> targetBuildings_5 = new List<Building>();

    #endregion

    private void Awake()
    {
        #region arrays
        actionInUse = new bool[6];
        for(int i = 0; i < actionInUse.Length; i++) { actionInUse[i] = false; }

        shopInUse = new bool[6];
        for(int i = 0; i < shopInUse.Length; i++) { shopInUse[i] = false; }

        inventory = new int[6];
        for(int i = 0; i < inventory.Length; i++) { inventory[i] = 0; }
        #endregion

        manager = GetComponent<Gov_Manager>();
        ui = GetComponent<Gov_Interface>();

        //Descriptions
        actionStrings = new Gov_ActionStrings();

        //update price UI
        for(int i = 0; i < shopPrices.Length; i++) { shopPriceTexts[i].text = "$" +  shopPrices[i].ToString(); }
    }

    private void Start()
    {
        singlePlayer = GameManager.instance.singlePlayer;

        //Turn off interface
        toggleActionConfirmation(false);
        actionExplanation.SetActive(false);
        toggleShopConfirmation(false);
        shopExplanation.SetActive(false);

        //Setup relevant interfaces
        updateInventoryCounters();

        //toggleActionExplanation(false);
    }

    #region action stuff
    public void clickActionButton(int id)
    {
        if (actionActive(id) == false)
        {
            if (id == selectedActionButton) //reset everything
            {
                for (int i = 0; i < actionButtons.Length; i++)
                {
                    actionButtons[i].GetComponent<ToggleButton>().setGraphic(false);
                }

                toggleActionConfirmation(false);
                actionExplanation.SetActive(false);
                selectedActionButton = 99;
            }
            else //select button
            {
                selectedActionButton = id;

                //Panels
                toggleActionConfirmation(true);
                setActionConfirmation(id);
                setActionExplanation(id); //set description

                //Buttons
                for (int i = 0; i < actionButtons.Length; i++)
                {
                    actionButtons[i].GetComponent<ToggleButton>().setGraphic(false);
                }

                //"Highlights" the button
                actionButtons[id].GetComponent<ToggleButton>().setGraphic(true);
            }
        }
        else
        {
            Debug.Log("GOV_Player: Cancel action here");
        }
    }
    public void clickActionConfirmationButton(int id)
    {
        switch (id)
        {
            case 0: //Cancel
            default:
                clickActionButton(selectedActionButton);
                break;

            case 1: //Information
                toggleActionConfirmation(false);
                //toggleActionExplanation(true);
                setActionExplanation(selectedActionButton);
                break;

            case 2: //Confirmation
                targetMode(true);
                break;
        }
    }
    public void actionConfirmReturnButton() { toggleActionConfirmation(true); }
    public void toggleActionConfirmation(bool val) { actionConfirmation.SetActive(val); actionExplanation.SetActive(!val); } //toggles the action confirmation screen
    void setActionExplanation(int index) // pulls up the screen to describe the action
    {
        actionSprite.sprite = buttonLogos[index];

        actionHeader.text = actionStrings.getActionString(index)[0];
        actionDescription.text = actionStrings.getActionString(index)[1];
    }
    void setActionConfirmation(int index)
    {
        actionConfirmationHeader.text = actionStrings.getActionString(index)[0];
        actionConfirmationSprite.sprite = buttonLogos[index];
    }

    public int getSelectedAction() { return selectedActionButton; }
    #endregion

    #region targetting
    public void targetMode(bool val)
    {
        if (val) //start multi targetting mode
        {
            Debug.Log("GOV_PLAYER: TARGET MODE ON"); 

            //State
            setState(playerStates.Targeting);

            //Interface
            toggleActionConfirmation(false);
            actionExplanation.SetActive(false);
            ui.triggerHeaderPerm("Select your targets. Targets selected: " + totalTargets(selectedActionButton));
            if(!targetCounter[selectedActionButton].gameObject.activeInHierarchy) targetCounter[selectedActionButton].gameObject.SetActive(true);
            targetCounter[selectedActionButton].text = totalTargets(selectedActionButton).ToString();
        }
        else //stop 
        {
            Debug.Log("GOV_PLAYER: TARGET MODE OFF");

            //State
            setState(playerStates.Gaming);

            //Interface
            
        }
    }
    public void addTarget(Building newTarget, int index)
    {
        if(!targetExists(newTarget, index))
        {
            Debug.Log("Adding target to : " + index);
            switch (index)
            {
                case 0:
                default:
                    targetBuildings_0.Add(newTarget);
                    Debug.Log("Total targets: " + index);
                    break;

                case 1:
                    targetBuildings_1.Add(newTarget);
                    Debug.Log("Total targets: " + index);
                    break;

                case 2:
                    targetBuildings_2.Add(newTarget);
                    Debug.Log("Total targets: " + index);
                    break;

                case 3:
                    targetBuildings_3.Add(newTarget);
                    Debug.Log("Total targets: " + index);
                    break;

                case 4:
                    targetBuildings_4.Add(newTarget);
                    Debug.Log("Total targets: " + index);
                    break;

                case 5:
                    targetBuildings_5.Add(newTarget);
                    Debug.Log("Total targets: " + index);
                    break;
            }
        }
        //if (!targetExists(newTarget)) 
        //{ 
        //    switch
        //    targetBuildings.Add(newTarget); 
        //}
    }
    public void removeTarget(Building removeTarget, int index)
    {
        switch (index)
        {
            case 0:
            default:
                for(int i = 0; i < targetBuildings_0.Count; i++)
                {
                    if(targetBuildings_0[i] == removeTarget) { targetBuildings_0.RemoveAt(i); }
                }
                break;

            case 1:
                for (int i = 0; i < targetBuildings_1.Count; i++)
                {
                    if (targetBuildings_1[i] == removeTarget) { targetBuildings_1.RemoveAt(i); }
                }
                break;

            case 2:
                for (int i = 0; i < targetBuildings_2.Count; i++)
                {
                    if (targetBuildings_2[i] == removeTarget) { targetBuildings_2.RemoveAt(i); }
                }
                break;

            case 3:
                for (int i = 0; i < targetBuildings_3.Count; i++)
                {
                    if (targetBuildings_3[i] == removeTarget) { targetBuildings_3.RemoveAt(i); }
                }
                break;

            case 4:
                for (int i = 0; i < targetBuildings_4.Count; i++)
                {
                    if (targetBuildings_4[i] == removeTarget) { targetBuildings_4.RemoveAt(i); }
                }
                break;

            case 5:
                for (int i = 0; i < targetBuildings_5.Count; i++)
                {
                    if (targetBuildings_5[i] == removeTarget) { targetBuildings_5.RemoveAt(i); }
                }
                break;


        }
    }
    public int totalTargets(int index) 
    {
        switch (index)
        {
            case 0:
            default:
                return targetBuildings_0.Count;

            case 1:
                return targetBuildings_1.Count;

            case 2:
                return targetBuildings_2.Count;

            case 3:
                return targetBuildings_3.Count;

            case 4:
                return targetBuildings_4.Count;

            case 5:
                return targetBuildings_5.Count;
        }
    }
    public void updateTargetCounter()
    {
        targetCounter[0].text = targetBuildings_0.Count.ToString() ;
        targetCounter[1].text = targetBuildings_1.Count.ToString();
        targetCounter[2].text = targetBuildings_2.Count.ToString();
        targetCounter[3].text = targetBuildings_3.Count.ToString();
        targetCounter[4].text = targetBuildings_4.Count.ToString();
        targetCounter[5].text = targetBuildings_5.Count.ToString();
    }

    #endregion
    public void clearTargets(int index)
    {
        switch (index)
        {
            case 0:
            default:
                targetBuildings_0.Clear();
                break;

            case 1:
                targetBuildings_1.Clear();
                break;

            case 2:
                targetBuildings_2.Clear();
                break;

            case 3:
                targetBuildings_3.Clear();
                break;

            case 4:
                targetBuildings_4.Clear();
                break;

            case 5:
                targetBuildings_5.Clear();
                break;
        }
    }
    public void clearAllTargets()
    {
        targetBuildings_0.Clear();
        targetBuildings_1.Clear();
        targetBuildings_2.Clear();
        targetBuildings_3.Clear();
        targetBuildings_4.Clear();
        targetBuildings_5.Clear();
    }
    bool targetExists(Building buildingToCheck, int index)
    {
        switch (index)
        {
            case 0:
            default:

                for (int i = 0; i < targetBuildings_0.Count; i++)
                {
                    if (buildingToCheck == targetBuildings_0[i]) return true;
                }

                return false;

            case 1:

                for (int i = 0; i < targetBuildings_1.Count; i++)
                {
                    if (buildingToCheck == targetBuildings_1[i]) return true;
                }

                return false;

            case 2:

                for (int i = 0; i < targetBuildings_2.Count; i++)
                {
                    if (buildingToCheck == targetBuildings_2[i]) return true;
                }

                return false;

            case 3:

                for (int i = 0; i < targetBuildings_3.Count; i++)
                {
                    if (buildingToCheck == targetBuildings_3[i]) return true;
                }

                return false;

            case 4:

                for (int i = 0; i < targetBuildings_4.Count; i++)
                {
                    if (buildingToCheck == targetBuildings_4[i]) return true;
                }

                return false;

            case 5:

                for (int i = 0; i < targetBuildings_5.Count; i++)
                {
                    if (buildingToCheck == targetBuildings_5[i]) return true;
                }

                return false;
        }
    }
    //void toggleActionExplanation(bool val) { actionExplanation.SetActive(val); } //toggles the action description screen
    public void endTurn()
    {
        setState(playerStates.FinishedTurn);

        //Interface
        ui.setPanelParent(false);
        toggleActionConfirmation(false);
        actionExplanation.SetActive(false);
        toggleShopConfirmation(false);
        shopExplanation.SetActive(false);

        if (singlePlayer) //if singleplayer, end turn immediately
        {
            GameManager.instance.endTurn();
        }
        else //else wait for other player
        {
            Debug.LogError("Multiplayer is not implemented yet.");
            //if(Ter_Player.getState() == Ter_Player.playerStates.FinishedTurn)
        }
    }
    public void setReady()
    {
        setState(playerStates.Ready);

        //Interface
        ui.setPanelParent(false);
        toggleActionConfirmation(false);
        actionExplanation.SetActive(false);
        toggleShopConfirmation(false);
        shopExplanation.SetActive(false);

        //do a check if other player is finishedturn here
        GameManager.instance.endTurn();
    }

    #region shop
    public void toggleShopConfirmation(bool val) { shopConfirmation.SetActive(val); shopExplanation.SetActive(!val); }
    void setShopExplanation(int index) // pulls up the screen to describe the action
    {
        shopSprite.sprite = buttonLogos_Green[index];

        shopHeader.text = actionStrings.getActionString(index)[0];
        shopDescription.text = actionStrings.getActionString(index)[1];
    }
    void setShopConfirmation(int index)
    {
        shopConfirmationHeader.text = actionStrings.getActionString(index)[0];
        shopConfirmationSprite.sprite = buttonLogos_Green[index];
    }
    public void clickShopButton(int id)
    {
        if (shopActive(id) == false)
        {
            if (id == selectedShopButton) //reset everything
            {
                for (int i = 0; i < shopButtons.Length; i++)
                {
                    shopButtons[i].GetComponent<ToggleButton>().setGraphic(false);
                }

                toggleShopConfirmation(false);
                shopExplanation.SetActive(false);

                selectedShopButton = 99;
            }
            else //select button
            {
                selectedShopButton = id;

                //Panels
                toggleShopConfirmation(true);
                setShopConfirmation(id);
                setShopExplanation(id); //set description

                //Buttons
                for (int i = 0; i < shopButtons.Length; i++)
                {
                    shopButtons[i].GetComponent<ToggleButton>().setGraphic(false);
                }

                //"Highlights" the button
                shopButtons[id].GetComponent<ToggleButton>().setGraphic(true);
            }
        }
        else
        {
            Debug.Log("GOV_Player: Cancel action here");
        }
    }
    public void clickShopConfirmButton(int index)
    {
        switch (index)
        {
            case 0: //Cancel
            default:
                clickShopButton(selectedShopButton);
                break;

            case 1: //Information
                toggleShopConfirmation(false);
                //toggleActionExplanation(true);
                setShopExplanation(selectedShopButton);
                break;

            case 2: //Confirmation
                buyItem(selectedShopButton);
                break;
        }
    }
    #endregion

    #region purchasing
    public void buyItem(int id)
    {
        if (shopPrices[id] <= Gov_Manager.instance.money)
        {
            incrementInventory(id);
            updateInventoryCounters();

            Gov_Manager.instance.deductMoney(shopPrices[id]);
            ui.setMoneyText(Gov_Manager.instance.money);
        }
        else
        {
            ui.triggerHeader("You do not have enough money to afford this!");
        }
    }
    public void updateInventoryCounter()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventoryCounter[i].text = inventory[i].ToString();
        }
    }

    public int getInventoryAmount(int index) { return inventory[index]; }

    public void incrementInventory(int index) { inventory[index]++; updateInventoryCounters(); }
    public void decreaseInventory(int index) { inventory[index]--; updateInventoryCounters(); }

    #endregion

    #region interface
    void updateInventoryCounters()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventoryTexts[i].text = inventory[i].ToString();
        }
    }
    public void toggleStateButton(bool isTurn)
    {
        stateButton.onClick.RemoveAllListeners();

        if (isTurn)
        {
            stateButton.onClick.AddListener(endTurn);
            ui.setStateButtonText("End Turn");
        }
        else
        {
            stateButton.onClick.AddListener(setReady);
            ui.setStateButtonText("Ready");
        }
    }

    #endregion

    #region return functions
    bool actionActive(int index) { return actionInUse[index]; } //check if there are already targets in use
    bool shopActive(int index) { return shopInUse[index]; }

    #endregion


    


}
