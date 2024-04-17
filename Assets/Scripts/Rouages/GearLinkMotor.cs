using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearLinkMotor : GearScriptLink
{
    public float Speed;
    void Start()
    {
        Linking(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Turn(float speed)
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * speed);
    }
    public override void Linking(bool AR)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.6f);
        foreach (Collider collider in hitColliders)
        {

            if (collider.gameObject.layer == 6)
            {
                if (AR == true && !GetComponent<GearScriptLink>().Linked.Contains(gameObject) && collider.gameObject != gameObject)
                {
                    Linked.Add(collider.gameObject);
                    if (!collider.GetComponent<GearScriptLink>().Linked.Contains(gameObject))
                        collider.GetComponent<GearScriptLink>().Linked.Add(gameObject);
                }
                else if (collider.GetComponent<GearScriptLink>().Linked.Contains(this.gameObject))
                {

                    collider.GetComponent<GearScriptLink>().Linked.Remove(this.gameObject);
                    Linked.Clear();


                }
            }
        }
    }
}