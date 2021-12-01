using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshFormation : MonoBehaviour
{
    public int xSquaresAmount = 20;
    public int zSquaresAmount = 20;

    Mesh mesh;
    Vector3[] vertices;
    int[] trianglePoints;


    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreatePoints();

        // Applying changes to mesh
        UpdateMesh();

    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void CreatePoints()
    {
        vertices = new Vector3[(xSquaresAmount + 1) * (zSquaresAmount + 1)];

        int index = 0;

        for(int i= 0; i <= zSquaresAmount; i++)
        {
            for(int j= 0; j <= xSquaresAmount; j++)
            {
                vertices[index] = new Vector3(i, 0, j);

                index ++;

            }
        }


        trianglePoints = new int[xSquaresAmount * zSquaresAmount * 6];

        int tris = 0;
        int verts = 0;

        for (int i = 0; i < zSquaresAmount; i++)
        {
            for (int j = 0; j < xSquaresAmount; j++)
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

    private void UpdateMesh()
    {
        // Clearing any posible vertices and triangle data
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = trianglePoints;


        // Display lighting correctly
        mesh.RecalculateNormals();

    }

    
}
