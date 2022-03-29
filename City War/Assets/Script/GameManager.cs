using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool govTurn;

    private void Awake()
    {
        instance = this;
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
