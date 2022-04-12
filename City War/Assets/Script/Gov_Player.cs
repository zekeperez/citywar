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

    private void Awake()
    {
        actionInUse = new bool[6];
        for(int i = 0; i < actionInUse.Length; i++) { actionInUse[i] = false; }

        shopInUse = new bool[6];
        for(int i = 0; i < shopInUse.Length; i++) { shopInUse[i] = false; }

        inventory = new int[6];
        for(int i = 0; i < inventory.Length; i++) { inventory[i] = 0; }

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

    public void clickActionButton(int id)
    {
        if(actionActive(id) == false)
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
                Debug.Log("CONFIRMED ACTION.");
                break;
        }
    }
    public void actionConfirmReturnButton() { toggleActionConfirmation(true); }

    public void toggleActionConfirmation(bool val) { actionConfirmation.SetActive(val); actionExplanation.SetActive(!val); } //toggles the action confirmation screen
    //void toggleActionExplanation(bool val) { actionExplanation.SetActive(val); } //toggles the action description screen
    public void toggleShopConfirmation(bool val) { shopConfirmation.SetActive(val); shopExplanation.SetActive(!val); }

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

    public void buyItem(int id)
    {
        if(shopPrices[id] <= Gov_Manager.instance.money)
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

    bool actionActive(int index) { return actionInUse[index]; } //check if there are already targets in use
    bool shopActive(int index) { return shopInUse[index]; }

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

    public void incrementInventory(int index) { inventory[index]++; updateInventoryCounters(); }
    public void decreaseInventory(int index) { inventory[index]--; updateInventoryCounters(); }

    void updateInventoryCounters()
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            inventoryTexts[i].text = inventory[i].ToString() ;
        }
    }
}
