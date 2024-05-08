using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGear : GearScriptLink
{
    public bool turning;
    public bool lastTurning;
    public GameObject porte;
    public bool call;
    void Awake()
    {
        Linking(true);

    }

    // Update is called once per frame
    void Update()
    {
        
           



    }

    public override void Turn(float speed)
    {

        transform.RotateAround(transform.position, transform.forward, Time.deltaTime * speed);
    }

    public override void Linking(bool AR)
    {
        Linked.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.6f);
        foreach (Collider collider in hitColliders)
        {
            Debug.Log(collider.gameObject.name);
            if (collider.gameObject.layer == 6 && collider.gameObject != gameObject)
            {
                if (AR == true)
                {
                    if (!Linked.Contains(collider.gameObject))
                    {
                        Linked.Add(collider.gameObject);

                    }
                    if (!collider.GetComponent<GearScriptLink>().Linked.Contains(gameObject))
                    {
                        collider.GetComponent<GearScriptLink>().Linked.Add(gameObject);
                    }

                }
                else
                {
                    if (Linked.Contains(collider.gameObject))
                    {
                        Linked.Remove(collider.gameObject);

                    }
                    if (collider.GetComponent<GearScriptLink>().Linked.Contains(gameObject))
                    {
                        collider.GetComponent<GearScriptLink>().Linked.Remove(gameObject);

                    }

                }
            }
        }
    }


}