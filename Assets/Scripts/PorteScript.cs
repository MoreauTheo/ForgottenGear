using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PorteScript : MonoBehaviour
{

    public GameObject manager;
    public List<GameObject> triggerList;
    public bool open;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        open = true;
       foreach (GameObject trigger in triggerList)
       {
            if(!manager.GetComponent<GearManager>().allTriggerGear.Contains(trigger)) 
            {
                open = false;
            }
       }
    
        if(open)
        { 
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            GetComponent<Renderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
    }
}
