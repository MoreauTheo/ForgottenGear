
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PickGear : MonoBehaviour
{
    public Transform camera;
    public float distanceHold;
    public bool Holding;
    public GameObject gearHolded;
    public GameObject PrefabPassif;
    public PlayerMovement characterMovement;
    public GearManager gearManager;
    public int numberGear;
    public TextMeshProUGUI displayNb;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        displayNb.text = numberGear.ToString();
        Debug.DrawRay(camera.position, camera.forward * 100, Color.red);
        if (Input.GetMouseButtonDown(0))
        {
            PickUp();
        }
       

        if (Holding)
        {
            if (gearHolded != null)
            {
                gearHolded.transform.position = new Vector3(-1, -1, -1);
                RaycastHit hitM = RaycastingMur();
                gearHolded.transform.LookAt(gearHolded.transform.position - new Vector3(hitM.normal.x,hitM.normal.y, hitM.normal.z));
                if(hitM.normal.y != 1)
                {
                    gearHolded.transform.position = new Vector3(Mathf.Round(hitM.point.x), Mathf.Ceil(hitM.point.y - 0.5f), Mathf.Round(hitM.point.z));
                }
                else
                {
                    gearHolded.transform.position = new Vector3(Mathf.Round(hitM.point.x), Mathf.Ceil(hitM.point.y), Mathf.Round(hitM.point.z));
                    Debug.Log(hitM.point.y);
                }

            }
        }
        else if(numberGear >0) 
        {
            gearHolded = Instantiate(PrefabPassif, transform.position, Quaternion.identity);
            gearHolded.GetComponent<GearScriptLink>().Linking(false);
            gearHolded.GetComponent<Collider>().enabled = false;
            distanceHold = Vector3.Distance(gearHolded.transform.position, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane)));
            Holding = true;
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
        RaycastHit hit = RaycastingGear();
        //if (hit.collider.tag != "barre")
        //{
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag != "TriggerGear" && hit.collider.gameObject.tag != "GearMotor")
                {
                    hit.collider.GetComponent<GearScriptLink>().Linking(false);
                    gearManager.AllGers.Remove(hit.collider.gameObject);
                    numberGear++;
                    Destroy(hit.collider.gameObject);

                }
            }
            else
            {
                RaycastHit hit2 = RaycastingMur();

                if (hit2.collider != null)
                {

                    if (gearHolded != null)
                        gearHolded.GetComponent<GearScriptLink>().Linking(true);

                    gearHolded.GetComponent<Collider>().enabled = true;
                    gearHolded.transform.parent = hit2.collider.transform;
                    gearHolded = null;
                    Holding = false;
                    numberGear--;
                    if (numberGear < 0)
                    {
                        numberGear = 0;
                    }



                }
            }
        //}
    }

}
