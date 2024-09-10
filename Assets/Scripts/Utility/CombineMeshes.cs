using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineMeshes : MonoBehaviour
{
    void Start()
    {
        // Get all child meshes
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();

        // Combine meshes
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false); // Disable original objects
        }

        // Create a new mesh renderer
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

        // Assign combined mesh
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combine, true, true);

    }
}
