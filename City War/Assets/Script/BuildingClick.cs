using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingClick : MonoBehaviour
{
    bool isClicked = false;

    BuildingManager bm;
    ColorPallette color;
    GameManager gm;

    Outline outline;
    Color outlineColor;

    RTS_Camera cam;

    //Players
    Gov_Player govPlayer;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        toggleOutline(false);

        cam = FindObjectOfType<RTS_Camera>();

        govPlayer = FindObjectOfType<Gov_Player>();
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
        if(IsPointerOverUIObject() == false) //check if over UI
        {
            if(gm.isGovTurn() && (govPlayer.getState() == Gov_Player.playerStates.Targeting))
            {
                //Just highlight, add to targetting

            }
            else
            {
                //Normal clicking
                if (isClicked) { unclick(); isClicked = false; }
                else { click(); isClicked = true; }
            }            
        }
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

    public bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
