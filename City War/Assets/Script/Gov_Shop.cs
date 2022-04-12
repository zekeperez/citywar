using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gov_Shop : MonoBehaviour
{
    #region shops
    [Header("Action Buttons")]
    public Button[] actionButtons;
    bool[] actionInUse;
    int selectedActionButton = 99;
    public Button[] shopButtons;
    int selectedShopButton = 99;
    public Button stateButton;
    #endregion

    #region shop interface
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

    Gov_Manager manager;
    Gov_Interface ui;

    private void Awake()
    {
        actionInUse = new bool[6];
        for (int i = 0; i < actionInUse.Length; i++) { actionInUse[i] = false; }

        manager = GetComponent<Gov_Manager>();
        ui = GetComponent<Gov_Interface>();

        actionStrings = new Gov_ActionStrings();
    }
}
