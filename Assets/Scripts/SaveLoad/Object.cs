using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public ObjectData data = new ObjectData();
    public string names = "obj";

    public void StoreData() {

        data.name = names;
        data.pos = transform.position;
    }
    public void ApplyData() {
        SaveData.AddObjectData(data);
    }

    public void LoadData() {

        names = data.name;
        transform.position = data.pos;
    }

    void OnEnable()
    {
        SaveData.OnLoaded += LoadData;
        SaveData.OnBeforeSave += StoreData;
        SaveData.OnBeforeSave += ApplyData;
    }

    void OnDisable() {
        SaveData.OnLoaded -= LoadData;
        SaveData.OnBeforeSave -= StoreData;
        SaveData.OnBeforeSave += ApplyData;
    }
}
[Serializable]
public class ObjectData {

    public string name;
    public Vector3 pos;
    
}