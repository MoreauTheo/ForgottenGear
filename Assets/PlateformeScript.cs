using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeScript : MonoBehaviour
{
    public GameObject manager;
    public List<GameObject> triggerList;
    public bool open;
    public List <GameObject> Motor;
    public float solHauteur;
    void Start()
    {
        foreach(Transform go in transform)
        {
            if(go.gameObject.tag == "GearMotor")
            {
                Motor.Add(go.gameObject);
            }
        }
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
            transform.position = new Vector3(transform.position.x, solHauteur, transform.position.z);
            foreach(GameObject go in Motor)
            {
                go.GetComponent<GearScriptLink>().Linking(false);

                go.GetComponent<GearScriptLink>().Linking(true);
            }
        }
        
    }
}
