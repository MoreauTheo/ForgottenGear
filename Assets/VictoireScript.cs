using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoireScript : MonoBehaviour
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
            if (!manager.GetComponent<GearManager>().allTriggerGear.Contains(trigger))
            {
                open = false;
            }
        }
        
        if (open)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().enabled = false ;
        }
    }
}