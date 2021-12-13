using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]

public class Water : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] trianglePoints;

    public int xSquaresAmount = 100;
    public int zSquaresAmount = 100;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        createPoints();
        updateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createPoints()
    {
        vertices = new Vector3[(xSquaresAmount + 1) * (zSquaresAmount + 1)];

        int index = 0;

        for (int z = 0; z <= zSquaresAmount; z++)
        {
            for (int x = 0; x <= xSquaresAmount; x++)
            {
                vertices[index] = new Vector3(x, 0, z);

                index++;
            }
        }

        trianglePoints = new int[xSquaresAmount * zSquaresAmount * 6];

        int tris = 0;
        int verts = 0;

        for (int z = 0; z < zSquaresAmount; z++)
        {
            for (int x = 0; x < xSquaresAmount; x++)
            {
                trianglePoints[tris + 0] = verts + 0;
                trianglePoints[tris + 1] = verts + xSquaresAmount + 1;
                trianglePoints[tris + 2] = verts + 1;
                trianglePoints[tris + 3] = verts + 1;
                trianglePoints[tris + 4] = verts + xSquaresAmount + 1;
                trianglePoints[tris + 5] = verts + xSquaresAmount + 2;

                tris += 6;
                verts++;
            }

            verts++;
        }

    }

    private void updateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = trianglePoints;

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
    }

}
