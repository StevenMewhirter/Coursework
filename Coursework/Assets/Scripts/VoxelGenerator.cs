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
    Dictionary<string, Vector2> texNameCoordDictionary;

    public List<string> texNames;
    public List<Vector2> texCoords;
    public float texSize;

    public void Initialise()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();
        vertexList = new List<Vector3>();
        triIndexList = new List<int>();
        UVList = new List<Vector2>();
  
        CreateTextureNameCoordDictionary();
        CreateVoxel(0, 0, 0, new Vector2(0, 0));
    }

    public void UpdateMesh()
    {
        mesh.vertices = vertexList.ToArray();   // Convert index list to array and store in mesh
        mesh.triangles = triIndexList.ToArray();        // Convert index list to array and store in mesh
        mesh.uv = UVList.ToArray();        // Convert UV list to array and store in mesh
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = null;        // Create a collision mesh
        meshCollider.sharedMesh = mesh;
    }

    void Start()
    {
        
    }

    void CreateVoxel(int x, int y, int z, Vector2 uvCoords)
    {
        CreateNegativeXFace(x, y, z, uvCoords);
        CreatePositiveXFace(x, y, z, uvCoords);
        CreateNegativeYFace(x, y, z, uvCoords);
        CreatePositiveYFace(x, y, z, uvCoords);
        CreateNegativeZFace(x, y, z, uvCoords);
        CreatePositiveZFace(x, y, z, uvCoords);
    }
    void CreateVoxel(int x, int y, int z, string texture)
    {
        Vector2 uvCoords = texNameCoordDictionary[texture];
        CreateNegativeXFace(x, y, z, uvCoords);
        CreatePositiveXFace(x, y, z, uvCoords);
        CreateNegativeYFace(x, y, z, uvCoords);
        CreatePositiveYFace(x, y, z, uvCoords);
        CreateNegativeZFace(x, y, z, uvCoords);
        CreatePositiveZFace(x, y, z, uvCoords);
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
    //void CreateNegativeZFace(int x, int y, int z, string texture)
    //{
        
    //    vertexList.Add(new Vector3(x, y + 1, z));
    //    vertexList.Add(new Vector3(x + 1, y + 1, z));
    //    vertexList.Add(new Vector3(x + 1, y, z));
    //    vertexList.Add(new Vector3(x, y, z));
    //    AddTriangleIndices();
        
    //    //  AddUVCoords(uvCoords);
    //}

    void CreatePositiveZFace(int x, int y, int z, Vector2 uvCoords)
    {

        vertexList.Add(new Vector3(x + 1, y, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y, z + 1));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }    void CreatePositiveZFace(int x, int y, int z, string texture)
    {
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y, z + 1));
        AddTriangleIndices();
        Vector2 uvCoords = texNameCoordDictionary[texture];
        //  AddUVCoords(uvCoords);
    }    void CreateNegativeXFace(int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x, y, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x, y, z));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }
    void CreateNegativeXFace(int x, int y, int z, string texture)
    {
        
        vertexList.Add(new Vector3(x, y, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x, y, z));
        AddTriangleIndices();
        Vector2 uvCoords = texNameCoordDictionary[texture];
        // AddUVCoords(uvCoords);
    }    void CreatePositiveXFace(int x, int y, int z, Vector2 uvCoords)
        {
        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }
    void CreatePositiveXFace(int x, int y, int z, string texture)
    {
       
        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        AddTriangleIndices();
        Vector2 uvCoords = texNameCoordDictionary[texture];
        // AddUVCoords(uvCoords);
    }
    void CreateNegativeYFace(int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x, y, z));
        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        vertexList.Add(new Vector3(x, y, z + 1));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }
    void CreateNegativeYFace(int x, int y, int z, string texture)
    {
        
        vertexList.Add(new Vector3(x, y, z));
        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        vertexList.Add(new Vector3(x, y, z + 1));
        AddTriangleIndices();
        // AddUVCoords(uvCoords);
        Vector2 uvCoords = texNameCoordDictionary[texture];
    }
    void CreatePositiveYFace(int x, int y, int z, Vector2 uvCoords)
    {
        
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        AddTriangleIndices();
        AddUVCoords(uvCoords);

    }
    void CreatePositiveYFace(int x, int y, int z, string texture)
    {
       
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        AddTriangleIndices();
        texNameCoordDictionary.Add(texNames[i]);
        // AddUVCoords(uvCoords);
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
        UVList.Add(new Vector2(uvCoords.x, uvCoords.y));
    }
    void CreateTextureNameCoordDictionary()
    {
        texNameCoordDictionary = new Dictionary<string, Vector2>(); // Create a dictionary instance before using
        if (texNames.Count == texCoords.Count) // Check the number of names and coordinates match
        {
            for (int i = 0; i < texNames.Count; i++)// Iterate through both lists
            {
                texNameCoordDictionary.Add(texNames[i], texCoords[i]);  // Add the pairing to the dictionary
            }
        }
        else
        {
            Debug.Log("texNames and texCoords count mismatch"); // List counts are not matching
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
