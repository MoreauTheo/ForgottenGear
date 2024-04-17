using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GearScriptLink : MonoBehaviour
{

    public List<GameObject> Linked;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void Turn(float speed);
    public abstract void Linking(bool AR);
}