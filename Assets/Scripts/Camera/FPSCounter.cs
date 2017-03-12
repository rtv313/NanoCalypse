using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    float deltaTime = 0.0f;

    private bool debug = false;
	private int totalVertex, totalTriangles, auxVertex, auxTriangles;

    void Update()
    {
		if (debug) {
			foreach(MeshFilter mf in FindObjectsOfType(typeof(MeshFilter)))
			{
				auxVertex += mf.mesh.vertexCount;
				auxTriangles += mf.mesh.triangles.Length;
			}
			totalVertex = auxVertex;
			totalTriangles = auxTriangles;
			auxVertex = 0;
			auxTriangles = 0;
		}
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        if (Input.GetKeyDown(KeyCode.F10))
        {
			foreach(MeshFilter mf in FindObjectsOfType(typeof(MeshFilter)))
			{
				auxVertex += mf.mesh.vertexCount;
				auxTriangles += mf.mesh.triangles.Length;
			}
			totalVertex = auxVertex;
			totalTriangles = auxTriangles;
			auxVertex = 0;
			auxTriangles = 0;
            debug = !debug;
        }
    }

    void OnGUI()
    {
        if (debug)
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
			Rect rect2= new Rect(10, 10, w, h * 2 / 100);
			string text2 = string.Format("{0:0.} Vertex ({1:0.} Triangles)", totalVertex, totalTriangles);
			GUI.Label(rect2, text2, style);
        }

    }
}
