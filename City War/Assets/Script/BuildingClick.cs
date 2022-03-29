using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingClick : MonoBehaviour
{
    bool isClicked = false;

    BuildingManager bm;
    ColorPallette color;
    GameManager gm;

    Outline outline;
    Color outlineColor;

    private void Awake()
    {
        
    }

    private void Start()
    {
        //set instances
        bm = BuildingManager.instance;
        color = ColorPallette.instance;
        gm = GameManager.instance;

        //outline.enabled = false;
        toggleOutline(true);
    }

    private void OnMouseDown()
    {
        if (isClicked) { unclick(); isClicked = false; }
        else { click(); isClicked = true;  }
    }

    void click()
    {
        bm.overrideBuilding(this);

        //Outline
        toggleOutline(true);

        if (gm.isGovTurn())
        {
        }
        else
        {
        }
    }

    void unclick()
    {
        bm.removeBuilding();

        //outline
        //outline.enabled = false;
        //outline.OutlineColor = outlineColor;
        toggleOutline(false);
    }


    public void toggleOutline(bool val)
    {

    }

    public void setOutlineColor(Color newColor)
    {
        outlineColor = newColor;
    }
}
