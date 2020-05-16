using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBoxShipper : MonoBehaviour
{
    static Animator NPCAnim;
    public Transform player;
    public GameObject boxCarry;
    public GameObject boxPrep;
    public GameObject[] waypoints;

    private string state = "shipping";
    private int currentWay = 0;
    private float RotSpeed = 3.5f;
    private float speed = 3.5f;
    private float accWP = 1.2f;
    private float pauseduration = 0;

    void BoxLose()
    {
        boxPrep.SetActive(false);
        boxCarry.SetActive(false);
        NPCAnim.SetBool("IsCarryRunning", false);
        NPCAnim.SetBool("isPrepping", false);
        NPCAnim.SetBool("isIdle", false);
        NPCAnim.SetBool("isRunning", true);
    }

    void BoxGive()
    {
        boxPrep.SetActive(false);
        boxCarry.SetActive(true);
        NPCAnim.SetBool("isPrepping", false);
        NPCAnim.SetBool("isRunning", false);
        NPCAnim.SetBool("isIdle", false);
        NPCAnim.SetBool("IsCarryRunning", true);
    }

    void BoxPrep()
    {
        boxPrep.SetActive(true);
        NPCAnim.SetBool("isRunning", false);
        NPCAnim.SetBool("isIdle", false);
        NPCAnim.SetBool("isPrepping", true);
        NPCAnim.SetBool("IsCarryRunning", true);
    }

    void Start()
    {
        NPCAnim = GetComponent<Animator>();

    }

    private void Awake()
    {
        waypoints[0] = GameObject.Find("Packaging Table");
        waypoints[1] = GameObject.Find("Pallet");
    }

    void Update()
    {
        if (pauseduration > 0)
            pauseduration -= Time.deltaTime;
        else
            move();
    }

    private void move()
    {
        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;

        if (state == "shipping" && waypoints.Length > 1)
        {
            if (currentWay == 0)
            { 
                NPCAnim.SetBool("isIdle", false);
                NPCAnim.SetBool("isRunning", true);
            }
            if (Vector3.Distance(waypoints[currentWay].transform.position, transform.position) < accWP)
            {
                if (currentWay == 0)
                {
                    BoxPrep();
                }
                if (currentWay == 1)
                {
                    BoxLose();
                }
                if (currentWay == 0)
                    pauseduration = 7;
                else
                    pauseduration = 9;
                currentWay++;
                if (currentWay >= waypoints.Length)
                {
                    currentWay = 0;
                }
            }
            direction = waypoints[currentWay].transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), RotSpeed * Time.deltaTime);
            transform.Translate(0, 0, Time.deltaTime * speed);
        }
    }
}
