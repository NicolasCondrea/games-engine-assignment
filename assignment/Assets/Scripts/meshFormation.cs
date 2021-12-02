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

        for(int z= 0; z <= zSquaresAmount; z++)
        {
            for(int x= 0; x <= xSquaresAmount; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                vertices[index] = new Vector3(x, y, z);

                index ++;

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
