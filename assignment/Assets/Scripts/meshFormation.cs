using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshFormation : MonoBehaviour
{
    public int xSquaresAmount = 20;
    public int zSquaresAmount = 20;

    public int mapScale = 500;
    public AnimationCurve heightCurve;

    public float scale;
    public int octaves;
    public float lacunarity;

    Mesh mesh;
    Vector3[] vertices;
    int[] trianglePoints;

    public int seed;
    private System.Random prng;
    private Vector2[] octaveOffsets;


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

    private Vector2[] getSeed()
    {

        seed = UnityEngine.Random.Range(0, 1000);
        // changes area of map
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        for (int o = 0; o < octaves; o++)
        {
            float offsetX = prng.Next(-100000, 100000);
            float offsetY = prng.Next(-100000, 100000);
            octaveOffsets[o] = new Vector2(offsetX, offsetY);
        }
        return octaveOffsets;
    }

    private void CreatePoints()
    {
        // Here we are creating a seed 
        Vector2[] octaveOffsets = getSeed();

        if (scale <= 0)
            scale = 0.0001f;

        vertices = new Vector3[(xSquaresAmount + 1) * (zSquaresAmount + 1)];

        int index = 0;

        for(int z= 0; z <= zSquaresAmount; z++)
        {
            for(int x= 0; x <= xSquaresAmount; x++)
            {

                // Using noise to randomly assign height of vertices
                float noiseHeight = noiseHeightCreation(z, x, octaveOffsets);
                vertices[index] = new Vector3(x, noiseHeight, z);

                index ++;

            }
        }


        private float noiseHeightCreation(int z, int x, Vector2[] octaveOffsets)
        {
            return 0;
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
