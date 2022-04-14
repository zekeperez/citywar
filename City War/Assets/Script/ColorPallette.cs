using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPallette : MonoBehaviour
{
    public static ColorPallette instance;

    [Header("Government")]
    public Color govDark;
    public Color govLight;
    public Color trapped;

    [Header("Terrorist")]
    public Color terDark;
    public Color terLight;
    public Color captured;
    public Color stronghold;

    [Header("Neutral")]
    public Color neutral;
    public Color bombed;
    public Color targetColor;
    
    private void Awake()
    {
        instance = this;
    }

    public Color getColor(string colorName)
    {
        switch (colorName.ToLower())
        {
            case "govdark":
                return govDark;

            case "govlight":
                return govLight;

            case "terdark":
                return terDark;

            case "terlight":
                return terLight;

            case "targetColor":
                return targetColor;

            case "neutral":
            default:
                return neutral;
        }
    }

    public Color getColorTer(string shade)
    {
        switch (shade.ToLower())
        {
            case "dark":
                return terDark;

            case "light":
            default:
                return terLight;
        }
    }

    public Color getColorGov(string shade)
    {
        switch (shade.ToLower())
        {
            case "dark":
                return govDark;

            case "light":
            default:
                return govLight;
        }
    }
}
