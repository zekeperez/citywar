using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMat : MonoBehaviour
{
    List<Color> origColors = new List<Color>();
    List<Material> matList = new List<Material>();

    Renderer ren;

    private void Awake()
    {
        ren = GetComponent<Renderer>();

    }

    private void Start()
    {
        setupMats();
    }

    public void setMat(string state)
    {
        switch (state.ToLower())
        {
            case "bombed":
            case "destroyed":

                for(int i = 0; i < matList.Count; i++)
                {
                    matList[i].color = Color.black;
                }

                break;

            case "trapped":
                for (int i = 0; i < matList.Count; i++)
                {
                    matList[i].color = Color.blue;
                }
                break;

            case "captured":
                for (int i = 0; i < matList.Count; i++)
                {
                    matList[i].color = Color.red;
                }
                break;

            case "stronghold":
                for (int i = 0; i < matList.Count; i++)
                {
                    matList[i].color = Color.yellow;
                }
                break;

            case "original":
            default:

                for(int i = 0; i < matList.Count; i++)
                {
                    matList[i].color = origColors[i];
                }

                break;
        }
    }

    #region setup
    void setupMats()
    {
        int matLength = ren.materials.Length;

        for(int i = 0; i < matLength; i++)
        {
            matList.Add(ren.materials[i]);
        }

        for(int s = 0; s < matLength; s++)
        {
            origColors.Add(ren.materials[s].color);
        }
    }

    #endregion
}
