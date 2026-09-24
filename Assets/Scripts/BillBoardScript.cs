using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    void LateUpdate()
    {
        Vector3 newRotation =  Camera.main.transform.eulerAngles;
        /*
        newRotation.x = 0;
        newRotation.z = 0;
        */
        transform.eulerAngles = newRotation;
    }
}
