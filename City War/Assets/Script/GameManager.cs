using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool govTurn = false;
    public bool paused;

    public bool singlePlayer = false;
    public bool spIsGov = true;

    Gov_Manager govManager;
    Gov_Player govPlayer;
    Gov_Interface govInterface;

    Ter_Enemy terEnemy;
    //Ter_Player terPlayer;

    private void Awake()
    {
        instance = this;

        govInterface = FindObjectOfType<Gov_Interface>();
        govPlayer = FindObjectOfType<Gov_Player>();
        govManager = FindObjectOfType<Gov_Manager>();

        terEnemy = FindObjectOfType<Ter_Enemy>();
    }

    private void Start()
    {
        //turn
        govTurn = false;
        terStart();

        //day counter
        DayCounter.instance.resetDay();
    }

    public void terStart()
    {
        Debug.Log("Terrorist chooses a random stronghold.");

        if (singlePlayer)
        {
            if (spIsGov)
            {
                terEnemy.chooseStronghold();
                govPlayer.setState(Gov_Player.playerStates.Shopping);
                govManager.startShop();
            }
            else
            {
                Debug.LogError("Terrorist Player is not implemented yet.");
            }
        }
        else
        {
            Debug.LogError("Multiplayer is not implemented yet.");
        }

        //flipTurn();
        //startTurn();
    }

    public void startTurn()
    {
        DayCounter.instance.incrementDay();

        if (singlePlayer)
        {
            if (govTurn)
            {
                Debug.Log("Government turn");

                if (spIsGov) //check if player is the gov or terrorist
                { //Player is gov
                  //Government playing
                    govPlayer.setState(Gov_Player.playerStates.Gaming);
                    govManager.startTurn();

                    //Terrorist shopping
                    terEnemy.startTurn(false);
                }
                else
                { //Player is terrorist
                    Debug.LogError("Government AI is not implemented yet.");
                    //Government Shopping


                    //Terrorist Playing
                    Debug.LogError("Terrorist Player is not implemented yet.");
                }


            }
            else
            {
                Debug.Log("Terrorist Turn");

                if (spIsGov) //Check if player is government
                {
                    //Government Shopping
                    govPlayer.setState(Gov_Player.playerStates.Shopping);
                    govManager.startShop();


                    //Terrorist Turn
                    terEnemy.startTurn(true);
                }
                else
                {
                    Debug.LogError("Terrorist Player is not implemented yet.");

                }
            }
        }
        else
        {
            Debug.LogError("Multiplayer handling is not setup yet.");
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
        govInterface.triggerHeaderPerm("The day is ending...");
        Debug.Log("Animations will play here.");

        if (singlePlayer)
        {
            if (spIsGov)
            {
                #region Government
                //List<Building> govBombTargets
                #endregion

                #region Terrorist
                List<Building> terBombTargets = terEnemy.getBombTargets();
                for(int i = 0; i < terBombTargets.Count; i++) { terBombTargets[i].bombBuilding(); }
                #endregion
            }
            else
            {
                Debug.LogError("Terrorist Player is not implemented yet.");
            }
        }
        else
        {
            Debug.LogError("Multiplayer is not implemented yet.");
        }

        yield return new WaitForSeconds(5);
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
