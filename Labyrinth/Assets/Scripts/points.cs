using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class points : MonoBehaviour
{
   
    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Agent"))
        {
            Destroy(gameObject);
            RespawnPoint();
        }

        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            CountPoint();
            RespawnPoint();
        }

    }

    void RespawnPoint()
    {
        GameObject spawnObj = GameObject.FindGameObjectWithTag("Respawn"); 

        if(spawnObj != null)
        {
            spawnObj.GetComponent<SpawnObjectOnNavMesh>().SpawnObject();
        }
        else
        {
            Debug.LogError("Other GameObject reference is null. Assign a valid reference in the Unity Editor.");
        }
    }

    void CountPoint()
    {
        GameObject pointObj = GameObject.FindGameObjectWithTag("PointTextOBJ");
        pointObj.GetComponent<PointManager>().IncreasePoint();
    }
}
