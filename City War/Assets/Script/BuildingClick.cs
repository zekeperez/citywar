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

    RTS_Camera cam;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        toggleOutline(false);

        cam = FindObjectOfType<RTS_Camera>();
    }

    private void Start()
    {
        //set instances
        bm = BuildingManager.instance;
        color = ColorPallette.instance;
        gm = GameManager.instance;

        //outline.enabled = false;
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
        outline.enabled = val;
    }

    public void setOutlineColor(Color newColor)
    {
        outlineColor = newColor;
    }
}
