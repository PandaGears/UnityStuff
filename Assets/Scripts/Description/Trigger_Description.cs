using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger_Description : MonoBehaviour
{

    public bool GuiOn;
    public string Text = "";
    private string NewText;
    public Rect BoxSize = new Rect(0, 0, 200, 100);
    public GUISkin customSkin;

    public GameObject Inputfield;
    private string newname;


    public void namechange()
    {
        newname = Inputfield.GetComponent<Text>().text;
        if (Input.GetKey(KeyCode.Return))
        {
            gameObject.name = newname;
            Inputfield.SetActive(false);
        }
    }

    void OnTriggerEnter()
    {
        GuiOn = true;
    }

    void OnTriggerExit()
    {
        GuiOn = false;
    }

    void OnGUI()
    {

        if (customSkin != null)
        {
            GUI.skin = customSkin;
        }

        if (GuiOn == true)
        {
            if(Input.GetKey(KeyCode.R))
            {
                Inputfield.SetActive(true);
            }
            if(Input.GetKey(KeyCode.Return))
            { 
                GUI.BeginGroup(new Rect((Screen.width - BoxSize.width) / 2, (Screen.height - BoxSize.height) / 2, BoxSize.width, BoxSize.height));

                NewText = gameObject.name + " : " + Text;
                GUI.Label(BoxSize, NewText);
            
                GUI.EndGroup();
            }
        }


    }

}