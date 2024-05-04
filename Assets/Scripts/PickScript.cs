
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickGear : MonoBehaviour
{
    public Transform camera;
    public float distanceHold;
    public bool Holding;
    public GameObject gearHolded;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(camera.position, camera.forward * 100, Color.red);
        if (Input.GetMouseButtonDown(0))
        {
            PickUp();
        }
        if (Holding)
        {
            if (gearHolded != null)
            {
                /*
                Vector3 InFrontOfCameraPos = camera.position + (camera.forward * distanceHold);
                gearHolded.transform.position = new Vector3(Mathf.Round(InFrontOfCameraPos.x), Mathf.Round(InFrontOfCameraPos.y), Mathf.Round(InFrontOfCameraPos.z));
                distanceHold += Input.GetAxis("Mouse ScrollWheel") * 3;*/
                RaycastHit hitM = RaycastingMur();
                Debug.Log(hitM);
                gearHolded.transform.position = new Vector3(Mathf.Round(hitM.point.x),Mathf.Ceil(hitM.point.y), Mathf.Round(hitM.point.z));

                gearHolded.transform.LookAt(gearHolded.transform.position - new Vector3(hitM.normal.x,hitM.normal.y, hitM.normal.z));
                


            }
        }
        else
        {
            if (gearHolded != null)
                gearHolded.GetComponent<GearScriptLink>().Linking(true);

            gearHolded = null;

        }
    }
    public RaycastHit RaycastingGear()
    {
        Vector3 screenMousePosFar = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, Mathf.Infinity, LayerMask.GetMask("Gear"));
        return hit;
    }


    public RaycastHit RaycastingMur()
    {
        Vector3 screenMousePosFar = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hitM;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hitM, 10 , LayerMask.GetMask("Mur"));
        return hitM;
    }
    public void PickUp()
    {

        if (!Holding)
        {
           
            RaycastHit hit = RaycastingGear();

            if (hit.collider != null)
            {
                gearHolded = hit.collider.gameObject;
                gearHolded.GetComponent<GearScriptLink>().Linking(false);
                distanceHold = Vector3.Distance(gearHolded.transform.position, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane)));
            }
        }
        Holding = !Holding;
    }

}
