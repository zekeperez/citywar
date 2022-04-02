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
    int selectedShopButton = 99;
    public Button stateButton;
    #endregion

    Gov_Manager manager;
    Gov_Interface ui;
    bool singlePlayer;

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

    public GameObject shopConfirmation;
    public GameObject shopExplanation;
    #endregion

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

        manager = GetComponent<Gov_Manager>();
        ui = GetComponent<Gov_Interface>();

        actionStrings = new Gov_ActionStrings();
    }

    private void Start()
    {
        singlePlayer = GameManager.instance.singlePlayer;
        toggleActionConfirmation(false);
        toggleActionExplanation(false);
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
                toggleActionExplanation(true);
                setActionExplanation(id);
                break;

            case 2: //Confirmation
                Debug.Log("CONFIRMED ACTION.");
                break;
        }
    }

    void toggleActionConfirmation(bool val) { actionConfirmation.SetActive(val); } //toggles the action confirmation screen
    void toggleActionExplanation(bool val) { actionExplanation.SetActive(val); } //toggles the action description screen
    void toggleShopConfirmation(bool val) { shopConfirmation.SetActive(val); }

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

    public void clickShopButton(int id)
    {

    }

    bool actionActive(int index) { return actionInUse[index]; } //check if there are already targets in use

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
        ui.setPanelParent(false);

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
        ui.setPanelParent(false);

        //do a check if other player is finishedturn here
        GameManager.instance.endTurn();
    }
}
