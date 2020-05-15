using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBase : MonoBehaviour
{
    private CharBrain brain;
    public GameObject boxCarry;
    static Animator NPCanim;

    public bool isCarry;
    public bool has_box;

    public GameObject[] waypoints;
    void Start()
    {
        NPCanim = GetComponent<Animator>();
        UseNPCBrain();
    }
    void BoxLose()
    {
        boxCarry.SetActive(false);
    }

    void BoxGive()
    {
        boxCarry.SetActive(true);
    }

    void Update()
    {
        brain.Tick();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Packaging Table"))
        { 
            BoxGive();
        }
        else if (other.gameObject.name.Contains("Pallet"))
        {
            BoxLose();
        }
        else if (other.gameObject.name.Contains("Printer") || other.gameObject.name.Contains("Photocopier"))
        {
                NPCanim.SetBool("isPushing", true);
        }
    }
    public void UseControlledBrain()
    {
        brain = new ControlledBrain(GetComponent<NavMeshAgent>());
    }

    public void UseNPCBrain()
    {
        brain = new NPCBrain(GetComponent<NavMeshAgent>());
    }
}
