using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject[] explosionsPrefab;
    public Building targetBuilding;

    public void setTargetBuilding(Building target) { targetBuilding = target ; }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Building")
        {
            for(int i = 0; i < explosionsPrefab.Length; i++)
            {
                Instantiate(explosionsPrefab[i], transform.position, Quaternion.identity);
            }

            targetBuilding.bombBuilding();

            Destroy(this.gameObject);
        }
    }
}
