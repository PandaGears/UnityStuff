
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SceneObject {
	
	public string name;
	public string prefabName;
	public string id;
	public string idParent;

	public bool active;
	public Vector3 position;
	public Vector3 localScale;
	public Quaternion rotation;

	public List<ObjectComponent> objectComponents = new List<ObjectComponent>();
}



