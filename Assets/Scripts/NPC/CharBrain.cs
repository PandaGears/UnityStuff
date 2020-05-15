using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class CharBrain
{
    protected NavMeshAgent nav;

    public CharBrain(NavMeshAgent nav)
    {
        this.nav = nav;
        nav.SetDestination(nav.transform.position);
    }

    public abstract void Tick();
}
