
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
        Mouse.current.WarpCursorPosition(new Vector2(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(camera.position, camera.forward * 100, Color.red);
        if (Input.GetMouseButtonDown(0))
        {
            PickUp();
        }
        if (Holding)
        {
            if (gearHolded != null)
            {
                Vector3 InFrontOfCameraPos = camera.position + (camera.forward * distanceHold);
                gearHolded.transform.position = new Vector3(Mathf.Round(InFrontOfCameraPos.x), Mathf.Round(InFrontOfCameraPos.y), Mathf.Round(InFrontOfCameraPos.z));
                distanceHold += Input.GetAxis("Mouse ScrollWheel") * 3;
            }
        }
        else
        {
            if (gearHolded != null)
                gearHolded.GetComponent<GearScriptLink>().Linking(true);

            gearHolded = null;

        }
    }
    public RaycastHit Raycasting()
    {
        Vector3 screenMousePosFar = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, Mathf.Infinity, LayerMask.GetMask("Gear"));
        return hit;
    }


    public void PickUp()
    {

        if (!Holding)
        {
            Debug.Log("tir");
            RaycastHit hit = Raycasting();
            Debug.Log(hit.collider.gameObject);

            if (hit.collider != null)
            {
                Debug.Log("touche");
                gearHolded = hit.collider.gameObject;
                gearHolded.GetComponent<GearScriptLink>().Linking(false);
                distanceHold = Vector3.Distance(gearHolded.transform.position, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane)));
            }
        }
        Holding = !Holding;
    }

}
