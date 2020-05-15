using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBrain : CharBrain
{
    static Animator NPCanim;
    static GameObject box;

    private bool isRunning = false;
    private bool isIdle = true;
    private bool isCarrying;
    private bool has_box = false;

    private GameObject[] WP;
    int currentWP = 0;
    float accuracyWP = 5.0f;
    public NPCBrain(NavMeshAgent nav) : base(nav)
    {
        nav.GetComponent<Renderer>().material.color = Color.white;
        NPCanim = nav.GetComponent<Animator>();
        WP = nav.GetComponent<NPCBase>().waypoints;
        box = nav.GetComponent<NPCBase>().boxCarry;
    }

    public override void Tick()
    {
        Vector3 direction = nav.nextPosition - nav.transform.position;
        if (!nav.hasPath || nav.pathStatus != NavMeshPathStatus.PathComplete)
        {
            if (nav.gameObject.name.Contains("BoxShipper"))
            {
                if (WP.Length > 0)
                {
                    isIdle = false;
                    isRunning = true;
                    if (Vector3.Distance(WP[currentWP].transform.position, nav.transform.position) < accuracyWP)
                    {
                        currentWP++;
                        if (currentWP >= WP.Length)
                        {
                            currentWP = 0;
                        }
                    }
                    direction = WP[currentWP].transform.position - nav.transform.position;
                    nav.transform.rotation = Quaternion.Slerp(nav.transform.rotation, Quaternion.LookRotation(direction), 0.2f * Time.deltaTime);
                    nav.transform.Translate(0, 0, Time.deltaTime * 0.5f);
                }
            }
            else if (nav.gameObject.name.Contains("PaperWorks"))
            {
                if (WP.Length > 0)
                {
                    isIdle = false;
                    isRunning = true;
                    if (Vector3.Distance(WP[currentWP].transform.position, nav.transform.position) < accuracyWP)
                    {
                        currentWP++;
                        if (currentWP >= WP.Length)
                        {
                            currentWP = 0;
                        }
                    }
                    direction = WP[currentWP].transform.position - nav.transform.position;
                    nav.transform.rotation = Quaternion.Slerp(nav.transform.rotation, Quaternion.LookRotation(direction), 0.2f * Time.deltaTime);
                    nav.transform.Translate(0, 0, Time.deltaTime * 0.5f);
                }
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

    private Vector3 GetRandomPosition()
    {
        var randomOffset = new Vector3(
            UnityEngine.Random.Range(-5f, 5f), 
            0f, 
            UnityEngine.Random.Range(-5f, 5f));
        return nav.transform.position + randomOffset;
    }
}
