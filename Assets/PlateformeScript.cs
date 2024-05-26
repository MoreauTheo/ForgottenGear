using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeScript : MonoBehaviour
{
    public AudioManager audioManager;
    public GameObject manager;
    public List<GameObject> triggerList;
    public bool open;
    public List <GameObject> Motor;
    public float solHauteur;
    private bool once = false;
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
            if(!once)
            {
                audioManager.Play("PlateformeDown");
                once = true;
            }
            transform.position = new Vector3(transform.position.x, solHauteur, transform.position.z);
            GetComponent<Animator>().SetTrigger("Down");
            foreach (GameObject go in Motor)
            {

                go.GetComponent<GearScriptLink>().Linking(false);

                go.GetComponent<GearScriptLink>().Linking(true);
            }
        }
        
    }
}
