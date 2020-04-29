using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawAxis : MonoBehaviour {

	[HideInInspector] public bool canDraw;

	static Material lineMaterial;
	void OnRenderObject()
	{
		if (canDraw)
		{
			CreateLineMaterial();
			lineMaterial.SetPass(0);
			GL.PushMatrix();

			GL.MultMatrix(transform.localToWorldMatrix);
			GL.Begin(GL.LINES);
			GL.Color(Color.red);
			GL.Vertex3(0, 0, 0);
            GL.Vertex3(2.0f, 0.0f, 0.0f);
			GL.Color(Color.green);
			GL.Vertex3(0, 0, 0);
			GL.Vertex3(0.0f, 2.0f, 0.0f);
			GL.Color(Color.blue);
			GL.Vertex3(0, 0, 0);
			GL.Vertex3(0.0f, 0.0f, 2.0f);
			GL.End();
			GL.PopMatrix();
		}
	}

	void CreateLineMaterial()
	{
		if (!lineMaterial)
		{
			Shader shader = Shader.Find("Hidden/Internal-Colored");
			lineMaterial = new Material(shader);
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
	
			lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
		
			lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
	
			lineMaterial.SetInt("_ZWrite", 0);
		}
	}
}
