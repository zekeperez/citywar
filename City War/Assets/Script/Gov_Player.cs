using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gov_Player : MonoBehaviour
{
    public Button stateButton;

    Gov_Manager manager;
    Gov_Interface ui;

    public enum playerStates { Waiting, Shopping, Ready, Gaming, FinishedTurn }
    public playerStates state;

    public void setState(playerStates newState) { state = newState; }
    public playerStates getState() { return state; }

    private void Awake()
    {
        manager = GetComponent<Gov_Manager>();
        ui = GetComponent<Gov_Interface>();
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


    public void endTurn()
    {
        setState(playerStates.FinishedTurn);
        ui.setPanelParent(false);

        //do a check if other player is ready here
        GameManager.instance.endTurn();
    }
    

    public void setReady()
    {
        setState(playerStates.Ready);
        ui.setPanelParent(false);

        //do a check if other player is finishedturn here
        GameManager.instance.endTurn();
    }
}
