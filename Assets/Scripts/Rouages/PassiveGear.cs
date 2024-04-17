using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveGear : GearScriptLink
{

    // Start is called before the first frame update
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
        Linked.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.8f);
        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.layer == 6)
            {
                if (AR == true)
                {
                    Linked.Add(collider.gameObject);
                    collider.GetComponent<GearScriptLink>().Linked.Add(this.gameObject);
                }
                else if (collider.GetComponent<GearScriptLink>().Linked.Contains(this.gameObject))
                {
                    Linked.Remove(collider.gameObject);

                    collider.GetComponent<GearScriptLink>().Linked.Remove(this.gameObject);

                }
            }
        }
    }
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.8f);
    }

}