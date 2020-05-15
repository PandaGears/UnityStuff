using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlSwap : MonoBehaviour
{
    private NPCBase[] chars;

    int charIndex;

    private void Awake()
    {
        chars = FindObjectsOfType<NPCBase>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectNextChar();
        }
    }

    private void SelectNextChar()
    {
        chars[charIndex].UseNPCBrain();
        charIndex++;
        if (charIndex >= chars.Length)
        {
            charIndex = 0;
        }
        chars[charIndex].UseControlledBrain();
    }
}
