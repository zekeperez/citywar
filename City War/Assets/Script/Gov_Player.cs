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
    public Text actionHeader;
    public Text actionDescription;
    string lineBreakGap = "\n\n\n";

    public GameObject shopConfirmation;
    public GameObject shopExplanation;
    #endregion

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
        if(actionActive(id) == false)
        {
            if (id == selectedActionButton)
            {
                //Reset everything
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

    void toggleActionConfirmation(bool val) { actionConfirmation.SetActive(val); }
    void toggleShopConfirmation(bool val) { shopConfirmation.SetActive(val); }

    void setActionConfirmation(int index)
    {
        actionSprite.sprite = buttonLogos[index];

        switch (index)
        {
            case 0: //bomb
            default:
                actionHeader.text = "Bomb";

                actionDescription.text = "The bomb is a lethal explosive device capable of fully destroying buildings.\n" +
                                                            "When used, anyone living inside will cease to exist, including the\n" +
                                                            "functionalities of the building." + lineBreakGap +

                                                            "+ Destroying the final stronghold can net you the win. \n" +
                                                            "= Destroying captured buildings won't have any repercussions, however \n" + 
                                                            "the citizens might think otherwise.\n" + 
                                                            "- Destroying innocent buildings will have repercussions.";
                break;

            case 1: //drone
                actionHeader.text = "Drone";

                actionDescription.text = "Provides a surveillance drone to the target building.\n" +
                                                            "Reveals the current status of the building." + lineBreakGap +

                                                            "+ Reveals the real status of the building on that current turn.\n" + 
                                                            "+ Citizens will gradually become unhappy due to its invasive nature.";
                break;

            case 2: //shield
                actionHeader.text = "Defense";

                actionDescription.text = "Send troops to defend a building. Prevents any form of capture.\n" +
                                                            "However, their presence will definitely displease the tenants." + lineBreakGap +

                                                            "+ Prevents the Terrorist from capturing the building once.\n" +
                                                            "- Reduce overall happiness.";
                break;

            case 3: //barge
                actionHeader.text = "Barge In";

                actionDescription.text = "Send troops to barge into a building with lethal force authorized.\n" + 
                                                            "Well - trained troops can decipher situations better than untrained troops." + lineBreakGap + 


                                                            "+ Captured buildings will return to a neutral state.\n" + 
                                                            "- Barging in to innocent buildings will have repercussions.\n" + 
                                                            "-Strongholds can fend off barge in attacks.\n" + 
                                                             "- Untrained officers may cause casualties.";
                break;

            case 4: //police
                actionHeader.text = "Patrol Check";

                actionDescription.text = "Send troops to check in on houses. Depending on population happiness,\n" +
                                                            "civilians have the right to decline. Definitely cheaper and less intrusive\n" +
                                                            "than the drone." + lineBreakGap +

                                                            "= Only reveals if the building is currently occupied or not.\n" +
                                                            "= Chances of reveal depends on population happiness.";
                break;

            case 5: //repair
                actionHeader.text = "Building Reparations";

                actionDescription.text = "Hire a repair team and rebuild the destroyed building. The building will\n" +
                                                            "immediately be available for use." + lineBreakGap +

                                                            "+ Repair a building.\n" +
                                                            "+ Increases population happiness.\n" +
                                                            "-It will instantly be available for use for the next player.";
                break;
        }
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
