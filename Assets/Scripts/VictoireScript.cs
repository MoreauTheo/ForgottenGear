using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoireScript : MonoBehaviour
{

    public GameObject manager;
    public List<GameObject> triggerList;
    public GameObject trigger;
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
            if (!manager.GetComponent<GearManager>().allTriggerGear.Contains(trigger))
            {
                open = false;
            }
        }

        trigger.SetActive(open);
        
       
    }
}