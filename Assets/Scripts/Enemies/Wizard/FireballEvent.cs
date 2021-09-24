using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballEvent : MonoBehaviour
{
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnFireball()
    {
        Instantiate(fireballPrefab, spawnPoint.transform.position, Quaternion.identity);
    }
}
