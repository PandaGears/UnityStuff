using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData
{
    public static ObjectContainer objectContainer = new ObjectContainer();

    public delegate void SerializeAction();
    public static event SerializeAction OnBeforeSave;
    public static event SerializeAction OnLoaded;

    public static void Load(string path) {
       objectContainer = LoadObjects(path);

        foreach (ObjectData data in objectContainer.objects)
        {
            GameManager.CreateObject(data, GameManager.ObjectPath + data.name, data.pos, Quaternion.identity);
        }
        OnLoaded();
        ClearObjectList();
    }

    public static void Save(string path, ObjectContainer objects)
    {
        OnBeforeSave();
        SaveObjects(path, objects);
        ClearObjectList();
    }

    public static void AddObjectData(ObjectData data)
    {
        objectContainer.objects.Add(data);
    }

    private static void ClearObjectList()
    {
        objectContainer.objects.Clear();
    }

    private static ObjectContainer LoadObjects(string path)
    {
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<ObjectContainer>(json);
    }

    private static void SaveObjects(string path, ObjectContainer objects)
    {
        string json = JsonUtility.ToJson(objects);

        StreamWriter sw = File.CreateText(path);
        sw.Close();

        File.WriteAllText(path, json);
    }
}
