using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PorteScript : MonoBehaviour
{

    public GameObject manager;
    public List<GameObject> triggerList;
    public bool open;
    public Animator animator;
    public GameObject hitBox;
    //public bool porte2;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("SpeedMulti", 0);
    }

    // Update is called once per frame
    void Update()
    {
        open = true;
       foreach (GameObject trigger in triggerList)
       {
            if(!manager.GetComponent<GearManager>().allTriggerGear.Contains(trigger)) 
            {
                open = false;
            }
       }


        hitBox.SetActive(!open);
       // if(!porte2)
        //{
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0)
            {
                animator.Play("Opening", -1, 0f);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                animator.Play("Opening", -1, 1f);
            }
        /*}
        else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0)
            {
                animator.Play("Opening2", -1, 0f);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                animator.Play("Opening2", -1, 1f);
            }
        }*/
       

        if (open)
        {
            animator.SetFloat("SpeedMulti", 1);
        }
        else
        {
           animator.SetFloat("SpeedMulti", -1);
        }


    }
}
