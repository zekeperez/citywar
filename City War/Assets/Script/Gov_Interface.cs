using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Gov_Interface : MonoBehaviour
{
    [Header("Headers")]
    public Text dayCounter;
    public GameObject headerObj;
    public Text headerText;

    [Header("Panels")]
    public GameObject actionPanel;
    public GameObject shopPanel;
    public Text stateButton; //bottom right button

    [Header("Action Panel")]
    public Text[] actionCounter;
    public Text[] targetCounter;

    [Header("Information Panel")]
    public Text moneyText;
    public Text populationText;

    [Header("Pause")]
    public GameObject pauseParent;
    public GameObject pauseMenu;
    public GameObject quitScreen;

    #region bottom right
    public void togglePanel(bool action)
    {
        switch (action)
        {
            case true:
                actionPanel.SetActive(true);
                shopPanel.SetActive(false);
                break;

            default:
                actionPanel.SetActive(false);
                shopPanel.SetActive(true);
                break;
        }
    }
    public void setPanelParent(bool val)
    {
        actionPanel.transform.parent.gameObject.SetActive(val);
    }
    public void setActionCounter(int index, int amount) { actionCounter[index].text = amount.ToString(); }
    public void setTargetCounter(int index, int amount) { targetCounter[index].text = amount.ToString(); }
    public void setStateButtonText(string newString) { stateButton.text = newString; }
    #endregion

    #region header
    public void triggerHeaderPerm(string text)
    {
        headerObj.SetActive(true);
        headerText.text = text;
    }
    public void setHeader(bool val) { headerObj.SetActive(val); }
    public void triggerHeader(string text)
    {
        headerObj.SetActive(true);
        headerText.text = text;

        hideHeader();
    } 
    IEnumerator hideHeader()
    {
        yield return new WaitForSeconds(3);
        headerObj.SetActive(false);
    }
    #endregion

    public void togglePause(int index)
    {
        //0 - remove, 1 - pause, 2 - confirm quit

        switch (index)
        {
            case 0:
            default: //remove
                pauseParent.SetActive(false);
                break;

            case 1: //pause
                pauseParent.SetActive(true);
                pauseMenu.SetActive(true);
                quitScreen.SetActive(false);
                break;

            case 2: //confirm quit
                pauseParent.SetActive(true);
                pauseMenu.SetActive(false);
                quitScreen.SetActive(true);
                break;
        }
    }

    public void setMoneyText(int amount) { moneyText.text = "Money: $" + amount.ToString(); }
    public void setPopulationText(int amount) { populationText.text = "Population: " + amount.ToString(); }
}
