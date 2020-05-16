using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChange : MonoBehaviour
{

    public void RestoreSpeed()
    {
        Time.timeScale = 1.0f;
    }

    public void SpeedUp()
    {
        Time.timeScale = 2.0f;
    }

    public void SlowDown()
    {
        Time.timeScale = 0.5f;
    }

    public void StopDown()
    {
        Time.timeScale = 0f;
    }
} 

