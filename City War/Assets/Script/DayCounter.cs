using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCounter : MonoBehaviour
{
    public static DayCounter instance;
    public Text dayCounterText;
    int day = 0;
    string startingString = "Day: ";

    private void Awake()
    {
        instance = this;
    }

    public void resetDay()
    {
        day = 0;
        updateDay();
    }

    public void incrementDay()
    {
        day++;
        updateDay();
    }

    void updateDay()
    {
        dayCounterText.text = startingString + day;
    }
}
