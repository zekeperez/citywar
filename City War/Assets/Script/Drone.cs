using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public Transform target;
    public float orbitSpeed;

    public void setTarget(Transform target) { this.target = target; }

    private void Awake()
    {
        if(orbitSpeed <= 0) { orbitSpeed = 20; }
        transform.position = new Vector3(
            target.position.x + 1,
            target.position.y + 2.5f,
            target.position.z
            ) ;
    }

    void Update()
    {
        // Spin the object around the target at 20 degrees/second.
        transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }
}
