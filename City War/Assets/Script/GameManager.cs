using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool govTurn = false;
    public bool paused;

    Gov_Manager govManager;
    Gov_Player govPlayer;

    private void Awake()
    {
        instance = this;

        govPlayer = FindObjectOfType<Gov_Player>();
        govManager = FindObjectOfType<Gov_Manager>();
    }

    private void Start()
    {
        govTurn = false;
        terStart();
    }

    public void terStart()
    {
        Debug.Log("Terrorist chooses a random stronghold.");
        flipTurn();
        startTurn();
    }

    public void startTurn()
    {
        if (govTurn)
        {
            Debug.Log("Government turn");
            //Government playing
            govPlayer.setState(Gov_Player.playerStates.Gaming);
            govManager.startTurn();

            //Terrorist shopping
        }
        else
        {
            Debug.Log("Terrorist AI not implemented yet, skipping turn.");
            govPlayer.setState(Gov_Player.playerStates.Shopping);
            govManager.startShop();
            
        }
    }

    public void endTurn()
    {
        StartCoroutine(endTurnTrigger());

        govPlayer.setState(Gov_Player.playerStates.Waiting);
        //terPlayer.setState(Ter_Player.playerStates.Waiting);
    }

    IEnumerator endTurnTrigger()
    {
        Debug.Log("Animations will play here.");
        yield return new WaitForSeconds(10);
        flipTurn();
        startTurn();
        yield return null;
    }

    public void setPause(bool val)
    {
        paused = val;

        if (val)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    #region turns
    public void flipTurn()
    {
        govTurn = !govTurn;
    }

    public bool isGovTurn()
    {
        return govTurn;
    }
    #endregion
}
