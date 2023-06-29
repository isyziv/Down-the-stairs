using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] GameObject[] floorPrefab;
    public void spawnFloor()
    {
        int r = Random.Range(0, floorPrefab.Length);
        GameObject floor = Instantiate(floorPrefab[r], transform);
        floor.transform.position = new Vector3(Random.Range(-2.25f,2.25f),Random.Range(-6,-7), 0);
        
    }
}
