using UnityEngine;
using System.Collections;

public class meshTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Vector3[] vertices = new Vector3[5 * 2 + 2];
        for (int i = 0; i < 5 + 1; i++)
        {
            vertices[2 * i + 0] = new Vector3(i * 10 + 30,
                1,
                10);
            vertices[2 * i + 1] = new Vector3(i * 10 - 30,
                1,
                -10);
        }

        int[] triangles = new int[5 * 2 * 3];
        for (int i = 0; i < triangles.Length / 6; i++)
        {
            triangles[i * 6 + 0] = i * 2;
            triangles[i * 6 + 1] = i * 2 + 1;
            triangles[i * 6 + 2] = i * 2 + 2;

            triangles[i * 6 + 3] = i * 2 + 1;
            triangles[i * 6 + 4] = i * 2 + 2;
            triangles[i * 6 + 5] = i * 2 + 3;
        }

        MeshFilter meshF = GetComponent(typeof(MeshFilter)) as MeshFilter;
        Mesh mesh = meshF.mesh;
        MeshRenderer meshRenderer = GetComponent(typeof(MeshRenderer)) as MeshRenderer;

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        //mesh.RecalculateBounds();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    MeshFilter meshF = GetComponent(typeof(MeshFilter)) as MeshFilter;
    //    Mesh mesh = meshF.mesh;
    //    mesh.RecalculateBounds();
    //}
}
