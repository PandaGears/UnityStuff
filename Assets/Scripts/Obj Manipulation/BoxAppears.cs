using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAppears : MonoBehaviour
{
    public GameObject[] boxes;

    private int boxarray = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("HumanBoxShipper"))
        {
            if (boxes.Length >= boxarray)
            {
                GameObject box = boxes[boxarray];
                box.SetActive(true);
                boxarray++;
            }
        }
    }
}
