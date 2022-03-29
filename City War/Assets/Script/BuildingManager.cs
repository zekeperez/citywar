using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance;
    BuildingClick activeBuilding;

    RTS_Camera cam;
    Camera mainCam;

    private void Awake()
    {
        instance = this;

        cam = FindObjectOfType<RTS_Camera>();
        mainCam = Camera.main;
    }

    public void overrideBuilding(BuildingClick newBuilding)
    {
        if(activeBuilding != null) activeBuilding.toggleOutline(false);
        activeBuilding = newBuilding;
        setCameraTarget(activeBuilding.transform);
    }

    public void setCameraTarget(Transform buildingTransform)
    {
        if(buildingTransform == null)
        {
            cam.targetFollow = null;
            mainCam.fieldOfView = 60;
        }
        else
        {
            cam.transform.LookAt(buildingTransform.position);
            cam.targetFollow = buildingTransform;

            Vector3 dir = buildingTransform.position - cam.transform.position;
            Ray ray = new Ray(buildingTransform.position, dir);
            float distance = Vector3.Distance(buildingTransform.position, cam.transform.position);
            Vector3 offset = ray.GetPoint(distance / 3);

            cam.targetOffset = offset;

            mainCam.fieldOfView = 30;
        }     
    }

    private void Update()
    {
        if(cam.targetFollow != null)
        {
            cam.transform.LookAt(cam.targetFollow);
        }
    }

    public void removeBuilding()
    {
        cam.gameObject.transform.rotation = 
            Quaternion.Euler(new Vector3(45, cam.gameObject.transform.rotation.y, cam.gameObject.transform.rotation.z));

        setCameraTarget(null);
        activeBuilding = null;
    }
}
