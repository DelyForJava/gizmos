using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMesh : MonoBehaviour
{
    public float modifyX;
    public float modifyY;
    public float modifyZ;
    public GameObject prefab;
    void ShowLogMesh()
    {
        var clone = Instantiate(prefab);
        clone.name = "clone";
        var meshFilter = clone.GetComponent<MeshFilter>();
        var vertices = meshFilter.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            var vertex = vertices[i];
            vertices[i] = new Vector3(vertex.x * modifyX, vertex.x * modifyY, vertex.x * modifyZ);
            Debug.Log("x:" + vertices[i].x + ",y:" + vertices[i].y + ",z:" + vertices[i].z);
        }
        meshFilter.mesh.vertices = vertices;
        //meshFilter.mesh.RecalculateBounds();
        meshFilter.mesh.RecalculateNormals();
        //meshFilter.mesh.RecalculateTangents();

    }

    // Start is called before the first frame update
    void Awake()
    {
        ShowLogMesh();
    }


}
