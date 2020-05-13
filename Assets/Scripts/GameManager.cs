using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
[System.Serializable]
public class GameManager : MonoBehaviour {

	public static GameManager gm;

    public GameObject Env;

        
    public List<GameObject> placableObjects = new List<GameObject>();

    void Awake () {
		if (gm == null)
			gm = this;

        /* use linq to neaten this
         */
            var objectArray = Resources.LoadAll("", typeof(GameObject));
           foreach (Object item in objectArray)
           {
               placableObjects.Add((GameObject)item);
           }
  
    }

}
