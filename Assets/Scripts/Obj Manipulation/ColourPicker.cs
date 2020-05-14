using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPicker : MonoBehaviour
{

    public Material[] BodyColorMat;
    Material CurrMat;
    Renderer renders;
    
    // Use this for initialization
    void Start()
    {

        renders = this.GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    //render red color
    public void RedColor()
    {
        renders.material = BodyColorMat[0];
        CurrMat = renders.material;
        Debug.Log("RED");
    }

    //render greencolor
    public void GreenColor()
    {
        renders.material = BodyColorMat[1];
        CurrMat = renders.material;
        Debug.Log("GREEN");
    }
    //render blue color
    public void BlueColor()
    {
        renders.material = BodyColorMat[2];
        CurrMat = renders.material;
        Debug.Log("BLUE");
    }
}