using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;//for Type class
using System.Reflection;

using System.Linq;


public class SaveLoadMenu : MonoBehaviour {

	public bool usePersistentDataPath = true;
	public string savePath;
	public Dictionary<string,GameObject> prefabDictionary;

	// Use this for initialization
	void Start () {
		if(usePersistentDataPath == true) {
			savePath = Application.persistentDataPath + "/Saved Games/";
		}

		prefabDictionary = new Dictionary<string, GameObject>();
		GameObject[] prefabs = Resources.LoadAll<GameObject>("");
		foreach(GameObject loadedPrefab in prefabs) {
			if(loadedPrefab.GetComponent<ObjectIdentifier>()) {
				prefabDictionary.Add (loadedPrefab.name,loadedPrefab);
				Debug.Log("Added GameObject to prefabDictionary: " + loadedPrefab.name);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.F5)) {
			SaveGame();
		}
		
		if(Input.GetKeyDown(KeyCode.F9)) {
			LoadGame();
		}
	}
	
	
	
	IEnumerator wait(float time) {
		yield return new WaitForSeconds(time);
	}

	public void SaveGame() {
		SaveGame("QuickSave");
	}

	public void SaveGame(string saveGameName) {

		if(string.IsNullOrEmpty(saveGameName)) {
			Debug.Log ("SaveGameName is null or empty!");
			return;
		}

		SaveLoad.saveGamePath = savePath;

		SaveGame newSaveGame = new SaveGame();
		newSaveGame.savegameName = saveGameName;

		List<GameObject> goList = new List<GameObject>();

		ObjectIdentifier[] objectsToSerialize = FindObjectsOfType(typeof(ObjectIdentifier)) as ObjectIdentifier[];

		foreach (ObjectIdentifier objectIdentifier in objectsToSerialize) {

			if(objectIdentifier.dontSave == true) {
				Debug.Log("GameObject " + objectIdentifier.gameObject.name + " is set to dontSave = true, continuing loop.");
				continue;
			}

			if(string.IsNullOrEmpty(objectIdentifier.id) == true) {
				objectIdentifier.SetID();
			}

			goList.Add(objectIdentifier.gameObject);
		}

		foreach(GameObject go in goList) {
			go.SendMessage("OnSerialize",SendMessageOptions.DontRequireReceiver);
		}

		foreach(GameObject go2 in goList) {

			newSaveGame.sceneObjects.Add(PackGameObject(go2));
		}


		SaveLoad.Save(newSaveGame);
	}

	public void LoadGame() {
		LoadGame("QuickSave");
	}


	public void LoadGame(string saveGameName) {

		ClearScene();


		SaveGame loadedGame = SaveLoad.Load(saveGameName);
		if(loadedGame == null) {
			Debug.Log("saveGameName " + saveGameName + "couldn't be found!");
			return;
		}

		List<GameObject> goList = new List<GameObject>();


		foreach(SceneObject loadedObject in loadedGame.sceneObjects) {
			GameObject go_reconstructed = UnpackGameObject(loadedObject);
			if(go_reconstructed != null) {

				goList.Add(go_reconstructed);
			}
		}


		foreach(GameObject go in goList) {
			string parentId = go.GetComponent<ObjectIdentifier>().idParent;
			if(string.IsNullOrEmpty(parentId) == false) {
				foreach(GameObject go_parent in goList) {
					if(go_parent.GetComponent<ObjectIdentifier>().id == parentId) {
						go.transform.parent = go_parent.transform;
					}
				}
			}
		}

		foreach(GameObject go2 in goList) {
			go2.SendMessage("OnDeserialize",SendMessageOptions.DontRequireReceiver);
		}

	}

	public void ClearScene() {


		object[] obj = GameObject.FindObjectsOfType(typeof (GameObject));
		foreach (object o in obj) {
			GameObject go = (GameObject) o;


			if(go.CompareTag("DontDestroy") || go.CompareTag("BuildMenu") || go.CompareTag("Player") || go.CompareTag("MainCamera") || go.CompareTag("ParentButton")) {
				Debug.Log("Keeping GameObject in the scene: " + go.name);
				continue;
			}
			Destroy(go);
		}
	}
	
	public SceneObject PackGameObject(GameObject go) {
		
		ObjectIdentifier objectIdentifier = go.GetComponent<ObjectIdentifier>();


		SceneObject sceneObject = new SceneObject();
		sceneObject.name = go.name;
		sceneObject.prefabName = objectIdentifier.prefabName;
		sceneObject.id = objectIdentifier.id;
		if(go.transform.parent != null && go.transform.parent.GetComponent<ObjectIdentifier>() == true) {
			sceneObject.idParent = go.transform.parent.GetComponent<ObjectIdentifier>().id;
		}
		else {
			sceneObject.idParent = null;
		}


		List<string> componentTypesToAdd = new List<string>() {
			"UnityEngine.MonoBehaviour"
		};


		List<object> components_filtered = new List<object>();


		object[] components_raw = go.GetComponents<Component>() as object[];
		foreach(object component_raw in components_raw) {
			if(componentTypesToAdd.Contains(component_raw.GetType().BaseType.FullName)) {
				components_filtered.Add(component_raw);
			}
		}
		
		foreach(object component_filtered in components_filtered) {
			sceneObject.objectComponents.Add(PackComponent(component_filtered));
		}

		sceneObject.position = go.transform.position;
		sceneObject.localScale = go.transform.localScale;
		sceneObject.rotation = go.transform.rotation;
		sceneObject.active = go.activeSelf;

		return sceneObject;
	}
	
	public ObjectComponent PackComponent(object component) {


		ObjectComponent newObjectComponent = new ObjectComponent();
		newObjectComponent.fields = new Dictionary<string, object>();
		
		const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
		var componentType = component.GetType();
		FieldInfo[] fields = componentType.GetFields(flags);
		
		newObjectComponent.componentName = componentType.ToString();
		
		foreach(FieldInfo field in fields) {
			
			if (field != null) {
				if(field.FieldType.IsSerializable == false) {

					continue;
				}
				if(TypeSystem.IsEnumerableType(field.FieldType) == true || TypeSystem.IsCollectionType(field.FieldType) == true) {
					Type elementType = TypeSystem.GetElementType(field.FieldType);

					if(elementType.IsSerializable == false) {
						continue;
					}
				}
				
				object[] attributes = field.GetCustomAttributes(typeof(DontSaveField), true);
				bool stop = false;
				foreach(Attribute attribute in attributes) {
					if(attribute.GetType() == typeof(DontSaveField)) {

						stop = true;
						break;
					}
				}
				if(stop == true) {
					continue;
				}
				
				newObjectComponent.fields.Add(field.Name, field.GetValue(component));

			}
		}
		return newObjectComponent;
	}

	public GameObject UnpackGameObject(SceneObject sceneObject) {

		if(prefabDictionary.ContainsKey(sceneObject.prefabName) == false) {
			Debug.Log("Can't find key " + sceneObject.prefabName + " in SaveLoadMenu.prefabDictionary!");
			return null;
		}

		GameObject go = Instantiate(prefabDictionary[sceneObject.prefabName], sceneObject.position, sceneObject.rotation) as GameObject;
		
		go.name = sceneObject.name;
		go.transform.localScale = sceneObject.localScale;
		go.SetActive (sceneObject.active);
		
		if(go.GetComponent<ObjectIdentifier>() == false) {
			ObjectIdentifier oi = go.AddComponent<ObjectIdentifier>();
		}
		
		ObjectIdentifier idScript = go.GetComponent<ObjectIdentifier>();
		idScript.id = sceneObject.id;
		idScript.idParent = sceneObject.idParent;
		
		UnpackComponents(ref go, sceneObject);


		ObjectIdentifier[] childrenIds = go.GetComponentsInChildren<ObjectIdentifier>();
		foreach(ObjectIdentifier childrenIDScript in childrenIds) {
			if(childrenIDScript.transform != go.transform) {
				if(string.IsNullOrEmpty(childrenIDScript.id) == true) {
					Destroy (childrenIDScript.gameObject);
				}
			}
		}

		return go;
	}

	public void UnpackComponents(ref GameObject go, SceneObject sceneObject) {

		foreach(ObjectComponent obc in sceneObject.objectComponents) {
			
			if(go.GetComponent(obc.componentName) == false) {
				Type componentType = Type.GetType(obc.componentName);
				go.AddComponent(componentType);
			}
			
			object obj = go.GetComponent(obc.componentName) as object;
			
			var tp = obj.GetType();
			foreach(KeyValuePair<string,object> p in obc.fields) {
				
				var fld = tp.GetField(p.Key,
				                      BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
				                      BindingFlags.SetField);
				if (fld != null) {
					
					object value = p.Value;
					fld.SetValue(obj, value);
				}
			}
		}
	}

}

