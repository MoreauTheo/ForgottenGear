using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinalHandler : MonoBehaviour
{
    public GameObject manager;
    public List<GameObject> triggerList;
    public bool open;
    public PlayerMovement playerMovement;
    public bool triggeredAtLeastOnce = false;
    public GameObject canvas;
    public CinemachineVirtualCamera elevatorCamera;
    public GameObject character;
    public Animator RouageD;
    void Start()
    {
        
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

        if (open && !triggeredAtLeastOnce)
        {
            triggeredAtLeastOnce = true;
            OpenDoorTuto();
        }



    }

    private void OpenDoorTuto()
    {
        playerMovement.OnDisable();
        canvas.SetActive(false);
        character.SetActive(true);
        elevatorCamera.Priority = 100;
        StartCoroutine(waitTransition());
    }

    private IEnumerator waitTransition()
    {
        yield return new WaitForSeconds(1.2F);

        elevatorCamera.Priority = 100;
        yield return new WaitForSeconds(1);
        RouageD.SetTrigger("Start");
        yield return new WaitForSeconds(5);
        elevatorCamera.Priority = 1;
        yield return new WaitForSeconds(1);

        playerMovement.OnEnable();
        canvas.SetActive(true);
        if (playerMovement.fps)
        {
            character.SetActive(false);

        }


    }
}
