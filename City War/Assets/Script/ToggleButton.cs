using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public Sprite[] buttonImages;
    bool selected;
    Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    public void toggleButton()
    {
        selected = !selected;
    }

    public void setGraphic(bool selected)
    {
        if (selected)
        {
            btn.image.sprite = buttonImages[1];
        }
        else
        {
            btn.image.sprite = buttonImages[0];
        }
    }
}
