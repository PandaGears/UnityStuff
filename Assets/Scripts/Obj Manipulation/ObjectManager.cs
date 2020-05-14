using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectManager : MonoBehaviour {

	public float rotationSpeed = 10f;
    public float speed = 1.5f;
    public static GameObject controlledUnit = null;
    DrawAxis axis;
	Material lineMaterial;
    RaycastHit hit;

	void Start () {
		axis = GetComponent<DrawAxis>();
	}


    void selectObject()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            BoxCollider bcol = hit.collider as BoxCollider;
            if (Physics.Raycast(ray, out hit))
            {
                if (bcol != null)
                {
                    if (hit.transform.gameObject.tag == "Object")
                    {
                        controlledUnit = hit.transform.gameObject;
                        Debug.Log(controlledUnit.name);
                    }
                }
                else
                {
                    controlledUnit = null;
                }
            }
        }
    }

	void Update () {
        selectObject();
		if (controlledUnit != null)
		{
			axis.canDraw = true;
			if (Input.GetKey(KeyCode.LeftControl))
				RotateObject(controlledUnit);
			if (Input.GetKey(KeyCode.LeftAlt))
				TranslateObject(controlledUnit);
			if (Input.GetKeyDown(KeyCode.Delete))
				Destroy(controlledUnit);
		}
		else
		{
			axis.canDraw = false;
		}
	}

	void RotateObject(GameObject ctrlunit)
	{
    if (Input.GetKey(KeyCode.LeftControl) && Input.GetAxis("Mouse ScrollWheel") > 0) {
        ctrlunit.transform.Rotate(Vector3.up * 0.5f, Space.Self);
    }
    if (Input.GetKey(KeyCode.LeftControl) && Input.GetAxis("Mouse ScrollWheel") < 0) {
        ctrlunit.transform.Rotate(Vector3.down * 0.5f, Space.Self);
}
	}

	void TranslateObject(GameObject ctrlunit)
	{
        Vector3 pos = ctrlunit.transform.position;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            pos.z += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            pos.z -= speed * Time.deltaTime;
        }


        ctrlunit.transform.position = pos;
    }

}
