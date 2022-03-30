using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gov_PauseHandler : MonoBehaviour
{
    public bool pause;

    Gov_Interface ui;

    private void Awake()
    {
        ui = GetComponent<Gov_Interface>();
    }

    private void Start()
    {
        
    }

    public bool isPaused()
    {
        return pause;
    }

    public void togglePause()
    {
        pause = !pause;
        Debug.Log("Paused: " + pause);

        GameManager.instance.setPause(pause);

        //Interface
        if (pause)
        {
            ui.togglePause(1);
        }
        else
        {
            ui.togglePause(0);
        }
    }

    public void quitGame(bool confirm)
    {
        if (!confirm) //first round
        {
            ui.togglePause(2);
        }
        else //confirm quit
        {
            Debug.Log("Player has quit the game.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePause();
        }
    }
}
