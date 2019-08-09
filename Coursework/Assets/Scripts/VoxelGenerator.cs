using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), 
    typeof(MeshRenderer), 
    typeof(MeshCollider))]

public class VoxelGenerator : MonoBehaviour {
    //variables for the mesh
    Mesh mesh;
    MeshCollider meshCollider;
    List<Vector3> vertexList;
    List<int> triIndexList;
    List<Vector2> UVList;
    int numQuads = 0;

    // Use this for initialization
    void Start() {

        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();
        vertexList = new List<Vector3>();
        triIndexList = new List<int>();
        UVList = new List<Vector2>();

        CreateVoxel(0, 0, 0, new Vector2(0, 0));
      
        mesh.vertices = vertexList.ToArray();   // Convert index list to array and store in mesh
        mesh.triangles = triIndexList.ToArray();        // Convert index list to array and store in mesh
        mesh.uv = UVList.ToArray();        // Convert UV list to array and store in mesh
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = null;        // Create a collision mesh
        meshCollider.sharedMesh = mesh;

    }

    void CreateVoxel(int x, int y, int z, Vector2 uvCoords)
    {
        CreateNegativeZFace(x, y, z, uvCoords);
    }

    void CreateNegativeZFace(int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x, y, z));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }

    void AddTriangleIndices()
    {
        triIndexList.Add(numQuads * 4);
        triIndexList.Add((numQuads * 4) + 1);
        triIndexList.Add((numQuads * 4) + 3);
        triIndexList.Add((numQuads * 4) +1 );
        triIndexList.Add((numQuads * 4) +2 );
        triIndexList.Add((numQuads * 4) +3 );
        numQuads++;
    }

    void AddUVCoords(Vector2 uvCoords)
    {
        UVList.Add(new Vector2(uvCoords.x, uvCoords.y + 0.5f));
        UVList.Add(new Vector2(uvCoords.x + 0.5f, uvCoords.y + 0.5f));
        UVList.Add(new Vector2(uvCoords.x + 0.5f, uvCoords.y));
    }
    // Update is called once per frame
    void Update () {
		
	}
}
