using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPaperWorks : MonoBehaviour
{
    static Animator NPCAnim;
    public Transform player;
    public GameObject Docs;
    public GameObject[] waypoints;

    private string state = "Printing";
    private int currentWay = 0;
    private float RotSpeed = 3.5f;
    private float speed = 3.5f;
    private float accWP;
    private float pauseduration = 0;

    void DocLose()
    {
        Docs.SetActive(false);
    }

    void DocGive()
    {
        Docs.SetActive(true);
    }


    void Start()
    {
        NPCAnim = GetComponent<Animator>();

    }

    private void Awake()
    {
        waypoints[0] = GameObject.Find("Photocopier");
        waypoints[1] = GameObject.Find("Printer");
        waypoints[2] = GameObject.Find("Paper tray");
    }

    void Update()
    {
        if (pauseduration > 0)
            pauseduration -= Time.deltaTime;
        else
            move();
    }

    void move()
    {
        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;

        if (state == "Printing" && waypoints.Length > 1)
        {

            NPCAnim.SetBool("isIdle", false);
            NPCAnim.SetBool("isPushing", false);
            NPCAnim.SetBool("isTalking", false);
            NPCAnim.SetBool("isRunning", true);
            if (currentWay == 0)
                accWP = 0.4f;
            else if (currentWay == 1)
                accWP = 0.7f;
            else
                accWP = 1.0f;
            if (Vector3.Distance(waypoints[currentWay].transform.position, transform.position) < accWP)
            {
                direction = waypoints[currentWay].transform.position;
                if (currentWay == 0)
                    accWP = 0.4f;
                else if (currentWay == 1)
                    accWP = 0.4f;
                else
                    accWP = 1.0f;
                currentWay++;
                pauseduration = 5;
                if (pauseduration - Time.deltaTime != 0)
                {
                    if (currentWay == 1)
                    {
                        NPCAnim.SetBool("isRunning", false);
                        NPCAnim.SetBool("isIdle", false);
                        NPCAnim.SetBool("isPushing", true);
                        NPCAnim.SetBool("isTalking", false);
                    }
                    else if (currentWay == 2)
                    {
                        NPCAnim.SetBool("isRunning", false);
                        NPCAnim.SetBool("isIdle", false);
                        NPCAnim.SetBool("isTalking", false);
                        NPCAnim.SetBool("isPushing", true);
                    }
                    else
                    {
                        NPCAnim.SetBool("isRunning", false);
                        NPCAnim.SetBool("isIdle", false);
                        NPCAnim.SetBool("isPushing", false);
                        NPCAnim.SetBool("isTalking", true);
                    }
                }
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
