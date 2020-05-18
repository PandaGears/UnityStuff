using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperAppears : MonoBehaviour
{
    public GameObject[] Pages;

    private int pagearray = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("HumanPaperWorks"))
        {
            if (Pages.Length >= pagearray)
            {
                GameObject page = Pages[pagearray];
                page.SetActive(true);
                pagearray++;
            }
        }
    }
}
