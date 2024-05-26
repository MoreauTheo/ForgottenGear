using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveGear : GearScriptLink
{

    public bool RGB;
    public float hue;
    void Awake()
    {

        Linking(true);
        GameObject.Find("GearManager").GetComponent<GearManager>().AllGers.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (RGB)
        {
            if (hue >= 1)
            { hue = 0; }
            hue += Time.deltaTime / 2;
            GetComponent<Renderer>().materials[1].color = Color.HSVToRGB(hue, 1, 1);
        }
    }
    public override void Turn(float speed)
    {
        transform.RotateAround(transform.position, transform.forward, Time.deltaTime * speed);
    }

    public override void Linking(bool AR)
    {

        Linked.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.forward * 0.5f, 0.6f);
        foreach (Collider collider in hitColliders)
        {
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