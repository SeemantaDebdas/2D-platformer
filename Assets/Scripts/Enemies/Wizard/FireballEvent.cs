using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballEvent : MonoBehaviour
{
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] GameObject spawnPoint;

    public void SpawnFireball()
    {
        Instantiate(fireballPrefab, spawnPoint.transform.position, Quaternion.identity);
    }

    
}
