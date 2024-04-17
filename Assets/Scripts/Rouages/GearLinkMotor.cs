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
        transform.RotateAround(transform.position, transform.right, Time.deltaTime * speed);
    }
    public override void Linking(bool AR)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.8f);
        foreach (Collider collider in hitColliders)
        {
            Debug.Log("ca touche");

            if (collider.gameObject.layer == 6)
            {
                if (AR == true)
                {
                    collider.GetComponent<GearScriptLink>().Linked.Add(this.gameObject);
                    Linked.Add(collider.gameObject);
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