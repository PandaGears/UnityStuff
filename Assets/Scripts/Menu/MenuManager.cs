using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public GameObject button;

    public List<GameObject> toggleMenus = new List<GameObject>();

    private bool ismenu = false;
    public Canvas menu;

    string[] officeObjects = { "Photocopier", "Printer", "Paper", "Document", "code" };
    string[] shelvingObjects = { "Shelves", "Rack", "Cage", "Cantilever" };
    string[] tables = { "Table", "Desk" };
    string[] misc = { "Box", "Pallet", "Crate", "Cabinet", "Kardex", "Cart" };
    bool subMenu = false;

    void Start() {
        foreach (GameObject menu in toggleMenus)
        {
            menu.SetActive(false);
        }
        SetupBuildMenu();
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

    public void CloseAllMenus()
    {
        foreach (var menu in toggleMenus)
        {
            if (menu.transform.tag == "BuildMenu" || menu.transform.tag == "AnimateMenu")
            {
                foreach (Transform child in menu.transform)
                {
                    if (child.tag != "ParentButton")
                        child.gameObject.SetActive(false);
                }
            }
        }
    }

    void SetupBuildMenu()
    {
        foreach (var menu in toggleMenus)
        {
            if (menu.transform.tag == "BuildMenu")
            {
                foreach (GameObject obj in GameManager.gm.placableObjects)
                {
                    var btn = Instantiate(button, menu.transform);
                    btn.transform.name = obj.name;
                    btn.transform.GetChild(0).gameObject.GetComponent<Text>().text = obj.transform.name;
                    var temp = btn.transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale;
                    temp.x = 4;
                    btn.transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = temp;
                    Button b = btn.GetComponent<Button>();
                    b.onClick.AddListener(() => CreateObject(obj.transform.name));
                    btn.SetActive(false);
                }
            }
        }
    }

    public void MiscButton()
    {
        foreach (var menu in toggleMenus)
        {
            if (menu.transform.tag == "BuildMenu")
            {
                var menuButton = menu.transform.Find("Misc");
                Debug.Log(menuButton);
                foreach (Transform btn in menu.transform)
                {
                    foreach (string name in misc)
                    {
                        if (btn.name.Contains(name) && !btn.name.Contains("Rack"))
                        {
                            if (subMenu && btn.gameObject.active)
                            {
                                btn.gameObject.SetActive(false);
                            }
                            else
                            {
                                btn.SetSiblingIndex(menuButton.GetSiblingIndex() + 1);
                                btn.gameObject.SetActive(true);
                            }
                        }
                    }
                }
            }
        }
        subMenu = true;
    }

    public void TableButon()
    {
        foreach (var menu in toggleMenus)
        {
            if (menu.transform.tag == "BuildMenu")
            {
                var menuButton = menu.transform.Find("Other");
                Debug.Log(menuButton);
                foreach (Transform btn in menu.transform)
                {
                    foreach (string name in tables)
                    {
                        if (btn.name.Contains(name))
                        {
                            if (subMenu && btn.gameObject.active)
                            {
                                    btn.gameObject.SetActive(false);
                        }
                        else
                        {
                            btn.SetSiblingIndex(menuButton.GetSiblingIndex() + 1);
                            btn.gameObject.SetActive(true);
                        }
                        }
                    }
                }
            }
        }
        subMenu = true;
    }

    public void ShelvingButton()
    {
        foreach (var menu in toggleMenus)
        {
            if (menu.transform.tag == "BuildMenu")
            {
                var menuButton = menu.transform.Find("Shelving");
                foreach (Transform btn in menu.transform)
                {
                    foreach (string name in shelvingObjects)
                    {
                        if (btn.name.Contains(name))
                        {
                            if (subMenu && btn.gameObject.active)
                                btn.gameObject.SetActive(false);
                            else
                            {
                                btn.SetSiblingIndex(menuButton.GetSiblingIndex() + 1);
                                btn.gameObject.SetActive(true);
                            }
                        }
                    }
                }
            }
        }
        subMenu = true;
    }

    public void OfficeButton()
    {
        foreach (var menu in toggleMenus)
        {
            if (menu.transform.tag == "BuildMenu")
            {
                var menuButton = menu.transform.Find("Office Equipment");
                foreach (Transform btn in menu.transform)
                {
                    foreach (string name in officeObjects)
                    {
                        if (btn.name.Contains(name))
                        {
                            if (subMenu && btn.gameObject.active)
                                btn.gameObject.SetActive(false);
                            else
                            {
                                btn.SetSiblingIndex(menuButton.GetSiblingIndex() + 1);
                                btn.gameObject.SetActive(true);
                            }
                        }
                    }
                }
            }
        }
        subMenu = true;
    }

    public void MachineryButton()
    {
        foreach (var menu in toggleMenus)
        {
            if (menu.transform.tag == "BuildMenu")
            {
                var menuButton = menu.transform.Find("Machinery");
                foreach (Transform btn in menu.transform)
                {
                    if (btn.name.Contains("Forklift") || btn.name.Contains("loader"))
                    {
                        if (subMenu && btn.gameObject.active)
                            btn.gameObject.SetActive(false);
                        else
                        {
                            btn.SetSiblingIndex(menuButton.GetSiblingIndex() + 1);
                            btn.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
        subMenu = true;
    }

    void CreateObject(string name)
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
            var instance = Instantiate(obj, new Vector3(1,0,1), Quaternion.identity);
            var temp = new Vector3(1, 1, 1);
            instance.transform.localScale = temp;
        }
    }

    public void ShowBuildMenu()
    {
        foreach (var menu in toggleMenus)
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
