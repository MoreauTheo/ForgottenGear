
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class GearManager : MonoBehaviour
{
    public List<GameObject> AllGers;
    public List<GameObject> Used;
    public List<GameObject> Motors;
    public List<GameObject> chainStuckUsed;
    public List<GameObject> allTriggerGear;
    public bool MRGB;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        allTriggerGear.Clear();
        Used = new List<GameObject>();
        foreach (GameObject gear in AllGers)
        {
            if (gear.tag == "GearMotor")
            {
                if (!StartLink(gear, gear.GetComponent<GearLinkMotor>().Speed))
                {
                    gear.GetComponent<GearScriptLink>().Turn(gear.GetComponent<GearLinkMotor>().Speed);
                    Used.Add(gear);
                    foreach (GameObject connected in gear.GetComponent<GearLinkMotor>().Linked)
                    {
                        if(connected != null)
                        {
                            Propagate(connected, gear.GetComponent<GearLinkMotor>().Speed);

                        }

                    }
                }
            }
        }

        if(Input.GetKey(KeyCode.R)&& Input.GetKey(KeyCode.G)&& Input.GetKey(KeyCode.B))
        {
            if (!MRGB)
            {
                MRGB = true;
                GoRGB();
            }
        }
    }

    public void GoRGB()
    {
        foreach(GameObject ger in AllGers)
        {
            if(ger.tag == "Untagged")
            {
                ger.GetComponent<PassiveGear>().RGB = true;
                ger.GetComponent<PassiveGear>().hue = Random.Range(0f, 1f);
            }
        }
    }

    public void Propagate(GameObject childToTurn, float SpeedOfParent)
    {
        if (!Used.Contains(childToTurn))
        {
            childToTurn.GetComponent<GearScriptLink>().Turn(-SpeedOfParent);
            Used.Add(childToTurn);
            foreach (GameObject LinkedCogs in childToTurn.gameObject.GetComponent<GearScriptLink>().Linked)
            {
                if(LinkedCogs.tag != "GearMotor")
                {
                Propagate(LinkedCogs, -SpeedOfParent); 
                if (LinkedCogs.tag == "TriggerGear")
                {
                    allTriggerGear.Add(LinkedCogs);

                }
                }

            }
        }
    }

    public bool StartLink(GameObject StartingCog, float InitialSpeed)
    {
        chainStuckUsed.Clear();
        return (isChildStuck(StartingCog, InitialSpeed));
    }

    public bool isChildStuck(GameObject child, float InitialSpeed)
    {
        if (child)
        {
            if (!chainStuckUsed.Contains(child))
            {
                chainStuckUsed.Add(child);

                if (child.tag != "GearMotor")
                {
                    foreach (GameObject enfant in child.GetComponent<GearScriptLink>().Linked)
                    {
                        if (isChildStuck(enfant, -InitialSpeed))
                            return true;
                    }
                    return false;
                }
                else if (child.GetComponent<GearLinkMotor>().Speed == InitialSpeed)
                {
                    foreach (GameObject enfant in child.GetComponent<GearScriptLink>().Linked)
                    {
                        if (isChildStuck(enfant, -InitialSpeed))
                            return true;

                    }
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }
}
