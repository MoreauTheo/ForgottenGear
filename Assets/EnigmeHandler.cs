using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnigmeHandler : MonoBehaviour
{
    public GameObject manager;
    public List<GameObject> triggerList;
    public bool open;
    public PlayerMovement playerMovement;
    public bool triggeredAtLeastOnce = false;
    public GameObject canvas;
    public PorteScript porteScript;
    public PorteScript porteScript2;
    public CinemachineVirtualCamera doorCamera;
    public CinemachineVirtualCamera elevatorCamera;
    public Animator RouageG;
    public GameObject character;
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
        doorCamera.Priority = 100;
        porteScript.locked = true;
        porteScript2.locked = true;
        character.SetActive(true);
        StartCoroutine(waitTransition());
    }

    private IEnumerator waitTransition()
    {
        yield return new WaitForSeconds(1.2F);
        porteScript.openDoor();
        porteScript2.openDoor();

        yield return new WaitForSeconds(1.5F);
        doorCamera.Priority = 1;
        elevatorCamera.Priority = 100;
        yield return new WaitForSeconds(1);
        RouageG.SetTrigger("Start");
        yield return new WaitForSeconds(5);
        elevatorCamera.Priority = 1;
        yield return new WaitForSeconds(1);

        playerMovement.OnEnable();
        canvas.SetActive(false);
        if (playerMovement.fps)
        {
            character.SetActive(false);

        }
        porteScript.locked = false;
        porteScript2.locked = false;


    }
}