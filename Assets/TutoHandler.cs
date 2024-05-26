using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class TutoHandler : MonoBehaviour
{

    public GameObject manager;
    public List<GameObject> triggerList;
    public bool open;
    public PlayerMovement playerMovement;
    public bool triggeredAtLeastOnce = false;
    public GameObject canvas;
    public PorteScript porteScript;
    public CinemachineVirtualCamera doorCamera;
    public GameObject character;
    public AudioManager audioManager;
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

        if(open && !triggeredAtLeastOnce)
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
        doorCamera.Priority = 100;
        porteScript.locked = true;
        StartCoroutine(waitTransition());
    }

    private IEnumerator waitTransition()
    {
        yield return new WaitForSeconds(1.75F);
        porteScript.openDoor();
        audioManager.Play("VictorySound");

        yield return new WaitForSeconds(1.5F);
        doorCamera.Priority = 1;
        yield return new WaitForSeconds(1);
        playerMovement.OnEnable();
        canvas.SetActive(true);
        if(playerMovement.fps)
        {
            character.SetActive(false);

        }
        porteScript.locked = false;


    }
}
