using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gov_Player : MonoBehaviour
{
    [Header("Action Buttons")]
    public Button[] actionButtons;
    bool[] actionInUse;
    int selectedActionButton = 99;
    public Button[] shopButtons;
    int selectedShopButton = 99;
    public Button stateButton;

    Gov_Manager manager;
    Gov_Interface ui;

    bool singlePlayer;

    #region states

    public enum playerStates { Waiting, Shopping, Targeting, Ready, Gaming, FinishedTurn }
    public playerStates state;

    public void setState(playerStates newState) { state = newState; }
    public playerStates getState() { return state; }

    #endregion

    private void Awake()
    {
        actionInUse = new bool[5];
        for(int i = 0; i < actionInUse.Length; i++) { actionInUse[i] = false; }

        manager = GetComponent<Gov_Manager>();
        ui = GetComponent<Gov_Interface>();
    }

    private void Start()
    {
        singlePlayer = GameManager.instance.singlePlayer;
    }

    public void clickActionButton(int id)
    {
        if(id == selectedActionButton)
        {
            //Reset everything
            for (int i = 0; i < actionButtons.Length; i++)
            {
                actionButtons[i].GetComponent<ToggleButton>().setGraphic(false);
            }

            selectedActionButton = 99;
        }
        else
        {
            selectedActionButton = id;

            for (int i = 0; i < actionButtons.Length; i++)
            {
                actionButtons[i].GetComponent<ToggleButton>().setGraphic(false);
            }

            //"Highlights" the button
            actionButtons[id].GetComponent<ToggleButton>().setGraphic(true);
        }
    }

    public void clickShopButton(int id)
    {

    }

    bool actionActive(int index) { return actionInUse[index]; }

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
