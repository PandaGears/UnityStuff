using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectIdentifier : MonoBehaviour {
	
	public string name;
	public string prefabName;

	public string id;
	public string idParent;
	public bool dontSave = false;

	public void SetID() {
		
		id = System.Guid.NewGuid().ToString();
		CheckForRelatives();
	}
	
	private void CheckForRelatives() {
		
		if(transform.parent == null) {
			idParent = null;
		}
		else {
			ObjectIdentifier[] childrenIds = GetComponentsInChildren<ObjectIdentifier>();
			foreach(ObjectIdentifier idScript in childrenIds) {
				if(idScript.transform.gameObject != gameObject) {
					idScript.idParent = id;
					idScript.SetID();
				}
			}
		}
	}
}

