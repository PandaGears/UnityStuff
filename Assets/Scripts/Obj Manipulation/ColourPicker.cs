using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPicker : MonoBehaviour
{

    public Material[] BodyColorMat;
    Material CurrMat;
    Renderer renderer;
    
    // Use this for initialization
    void Start()
    {

        renderer = this.GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    //render red color
    public void RedColor()
    {
        renderer.material = BodyColorMat[0];
        CurrMat = renderer.material;
        Debug.Log("RED");
    }

    //render greencolor
    public void GreenColor()
    {
        renderer.material = BodyColorMat[1];
        CurrMat = renderer.material;
        Debug.Log("GREEN");
    }
    //render blue color
    public void BlueColor()
    {
        renderer.material = BodyColorMat[2];
        CurrMat = renderer.material;
        Debug.Log("BLUE");
    }
}