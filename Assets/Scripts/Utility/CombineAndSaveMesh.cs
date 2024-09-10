using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CombineAndSaveMesh : MonoBehaviour
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

        // Create a new GameObject to hold the combined mesh
        GameObject combinedObject = new GameObject("CombinedMeshObject");

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = combinedObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = combinedObject.AddComponent<MeshRenderer>();

        // Assign combined mesh to MeshFilter
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combine, true, true);

        // Optionally set materials here
        // meshRenderer.material = yourMaterial;

        // Save as a prefab
        string savePath = "Assets/Meshes/CombinedMeshPrefab.prefab";
        PrefabUtility.SaveAsPrefabAsset(combinedObject, savePath);

        // Debug log
        Debug.Log("Combined mesh saved as prefab: " + savePath);

        // Destroy the temporary combined object
        Destroy(combinedObject);
    }
}
