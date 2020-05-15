using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlledBrain : CharBrain
{
    static Animator NPCanim;
    static GameObject box;

    private bool isRunning = false;
    private bool isCarryRunning = false;
    private bool isIdle = true;

    public ControlledBrain(NavMeshAgent nav) : base(nav)
    {
        nav.GetComponent<Renderer>().material.color = Color.green;
        NPCanim = nav.GetComponent<Animator>();
        box = nav.GetComponent<NPCBase>().boxCarry;
    }

    public override void Tick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Input.GetKey(KeyCode.LeftControl) &&  Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                nav.destination = hit.point;
            }
        }
        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            isIdle = true;
            isRunning = false;
        }
        else 
        {
            isIdle = false;
            isRunning = true;
        }
            NPCanim.SetBool("isRunning", isRunning);
            NPCanim.SetBool("isIdle", isIdle);
    }

}
