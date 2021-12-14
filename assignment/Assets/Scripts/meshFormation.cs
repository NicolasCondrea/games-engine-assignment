using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MeshFormation : MonoBehaviour
{
    public int xSquaresAmount = 100;
    public int zSquaresAmount = 100;

    public int mapScale = 500;
    public float scale;
    public int octaves;
    public float lacunarity;

    Mesh mesh;
    Vector3[] vertices;
    int[] trianglePoints;

    public AnimationCurve heightCurve;
    public Gradient gradient;

    public int seed;

    private Color[] colors;

    private float maxHeightOfMap;
    private float minHeightOfMap;

    private float previousNoiseHeight;
    public GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreatePoints();
        TriangleGeneration();
        AddingColour();

        // Applying changes to mesh
        UpdateMesh();

    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private Vector2[] GetSeed()
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
        Vector2[] octaveOffsets = GetSeed();

        if (scale <= 0)
            scale = 0.0001f;

        vertices = new Vector3[(xSquaresAmount + 1) * (zSquaresAmount + 1)];

        int index = 0;

        for (int z = 0; z <= zSquaresAmount; z++)
        {
            for (int x = 0; x <= xSquaresAmount; x++)
            {

                // Using noise to randomly assign height of vertices
                float noiseHeight = NoiseHeightCreation(z, x, octaveOffsets);

                if (noiseHeight > maxHeightOfMap) maxHeightOfMap = noiseHeight;
                if (noiseHeight < minHeightOfMap) minHeightOfMap = noiseHeight;

                vertices[index] = new Vector3(x, noiseHeight, z);

                index++;

            }
        }

    }

    public void TriangleGeneration()
    {
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


    private float NoiseHeightCreation(int z, int x, Vector2[] octaveOffsets)
    {
        float amplitude = 12;
        float noiseHeight = 0;
        float frequency = 1;
        float persistence = 0.5f;

        // loop over octaves
        for (int y = 0; y < octaves; y++)
        {
            float Z = z / scale * frequency + octaveOffsets[y].y;
            float X = x / scale * frequency + octaveOffsets[y].x;

            // Create perlinValues and the * 2 - 1 is to create a flat floor
            float perlinValue = (Mathf.PerlinNoise(Z, X)) * 2 - 1;
            noiseHeight += heightCurve.Evaluate(perlinValue) * amplitude;
            frequency *= lacunarity;
            amplitude *= persistence;
        }

        return noiseHeight;
    }

    private void AddingColour()
    {
        colors = new Color[vertices.Length];

        int i = 0;

        for (int z = 0; z < vertices.Length; z++)
        {
            float height = Mathf.InverseLerp(minHeightOfMap, maxHeightOfMap, vertices[i].y);
            colors[i] = gradient.Evaluate(height);
            i++;
        }
    }

    public void ObjectSpawns()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertexLocations = transform.TransformPoint(mesh.vertices[i]);

            var noiseHeight = vertexLocations.y;

            if (System.Math.Abs(previousNoiseHeight - vertexLocations.y) < 25)
            {
                if (noiseHeight > 100)
                {
                    if (UnityEngine.Random.Range(1, 5) == 1)
                    {
                        GameObject objectToSpawn = objects[UnityEngine.Random.Range(0, objects.Length)];
                        var terrainSpawnHeight = noiseHeight * 2;

                        Instantiate(objectToSpawn, new Vector3(mesh.vertices[i].x * mapScale, terrainSpawnHeight, mesh.vertices[i].z * mapScale), Quaternion.identity);
                    }
                }
            }

            previousNoiseHeight = noiseHeight;
        }
    }

    private void UpdateMesh()
    {
        // Clearing any posible vertices and triangle data
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = trianglePoints;
        mesh.colors = colors;

        // Display lighting correctly
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        GetComponent<MeshCollider>().sharedMesh = mesh;

        gameObject.transform.localScale = new Vector3(mapScale, mapScale, mapScale);

        ObjectSpawns();
    }

}
