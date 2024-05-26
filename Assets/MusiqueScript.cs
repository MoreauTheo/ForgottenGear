using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusiqueScript : MonoBehaviour
{
    public AudioManager audioManager;
    void Start()
    {
        audioManager.Play("Theme");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
