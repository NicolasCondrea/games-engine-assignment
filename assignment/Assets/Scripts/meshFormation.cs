using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshFormation : MonoBehaviour
{
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

        // Adding the vertices points in an array
        vertices = new Vector3[]
        {
            new Vector3 (0, 0, 0),
            new Vector3 (0, 0, 1),
            new Vector3 (1, 0, 0),
            new Vector3 (1, 0, 1),
        };

        // Adding triangles
        trianglePoints = new int[]
        {
            // points must be in clockwise order or else backface culling will occur
            0, 1, 2,
            1, 3, 2
        };
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
