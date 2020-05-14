using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
[System.Serializable]
public class GameManager : MonoBehaviour {

	public static GameManager gm;

    public GameObject Env;

    public GameObject ObjectPrefab;
    public const string ObjectPath = "Prefabs/";

    private static string dataPath = string.Empty;
    
    public List<GameObject> placableObjects = new List<GameObject>();

    void Awake () {
		if (gm == null)
			gm = this;

        dataPath = System.IO.Path.Combine(Application.persistentDataPath, "Obj.json");
    }

    void Start()
    {
           
    }

    public static Object CreateObject(ObjectData data, string path, Vector3 position, Quaternion rotation)
    {
        Object obj = CreateObject(path, position, rotation);
        obj.data = data;
        return obj;
    }

    public static Object CreateObject(string path, Vector3 position, Quaternion rotation) {
        GameObject prefab = Resources.Load<GameObject>(path);

        GameObject go = Instantiate(prefab, position, rotation) as GameObject;

        Object obj = go.GetComponent<Object>() ?? go.AddComponent<Object>();
        return obj;
    }

    public void Save() {
        SaveData.Save(dataPath, SaveData.objectContainer);
    }

    public void Load()
    {
        SaveData.Load(dataPath);
    }
}
