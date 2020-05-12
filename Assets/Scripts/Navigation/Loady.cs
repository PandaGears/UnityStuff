using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loady : MonoBehaviour
{
    public GameObject panel;
    bool state;

    public void SwitchShowHide()
    {
        state = !state;
        panel.gameObject.SetActive(state);
    }
}
