using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MenuManager : MonoBehaviour {

    public GameObject button;

    public List<GameObject> toggleMenus = new List<GameObject>();

    private bool ismenu = false;
    public Canvas menu;
    
    void Start() {
        foreach (GameObject menu in toggleMenus)
        {
            if (menu.transform.tag == "BuildMenu")
            {
                menu.SetActive(false);
            }
        }
    }

    void Update() {
        if (ismenu == false && Input.GetKeyUp(KeyCode.Escape)) {
            ismenu = true;
            menu.enabled = true;
        }
        else if (menu.enabled == true && Input.GetKeyUp(KeyCode.Escape))
        {
            ismenu = false;
            menu.enabled = false;
        }
    }

    public void CreateObject(string name)
    {
        GameObject obj = null;
        foreach (GameObject item in GameManager.gm.placableObjects)
        {
            if (item.transform.name == name)
            {
                obj = item;
                break;
            }
        }
        if (obj != null)
        {
            var instance = Instantiate(obj, new Vector3(0,0,0), Quaternion.identity);
            var temp = new Vector3(1, 1, 1);
            
            instance.transform.localScale = temp;
        }
    }

    public void ShowBuildMenu()
    {
        foreach (GameObject menu in toggleMenus)
        {
            if (menu.transform.tag == "BuildMenu")
                menu.SetActive(!menu.activeSelf);
        }
    }


    public void AnimatePrinter()
    {
        GetComponent<Animator>().SetBool(name, true);
    }

    public void AnimatePhotocopier()
    {
        GetComponent<Animator>().SetBool(name, true);
    }

    public void AnimateCart()
    {
        GetComponent<Animator>().SetBool(name, true);
    }

}
