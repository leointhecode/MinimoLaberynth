using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnObjectOnNavMesh : MonoBehaviour
{
    public GameObject objectToSpawn;

    void Start()
    {
        SpawnObject();
    }

    public void SpawnObject()
    {
        // Get a random point on the NavMesh
        Vector3 randomPoint = RandomPointOnNavMesh();

        // Instantiate the object at the random point
        Instantiate(objectToSpawn, randomPoint, Quaternion.identity);
    }

    Vector3 RandomPointOnNavMesh()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        // Get a random triangle index
        int randomTriangleIndex = Random.Range(0, navMeshData.indices.Length / 3);

        // Get the vertices of the selected triangle
        Vector3[] triangleVertices = {
            navMeshData.vertices[navMeshData.indices[randomTriangleIndex * 3]],
            navMeshData.vertices[navMeshData.indices[randomTriangleIndex * 3 + 1]],
            navMeshData.vertices[navMeshData.indices[randomTriangleIndex * 3 + 2]]
        };

        // Get random barycentric coordinates within the triangle
        Vector2 randomBarycentric = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        randomBarycentric.Normalize();

        // Calculate the random point within the triangle
        Vector3 randomPoint = triangleVertices[0] +
                              randomBarycentric.x * (triangleVertices[1] - triangleVertices[0]) +
                              randomBarycentric.y * (triangleVertices[2] - triangleVertices[0]);

        return randomPoint;
    }
}
