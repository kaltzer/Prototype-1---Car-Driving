using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnCar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCar()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(enemyPrefab, new Vector3(Random.Range(-5f, 5f), 3f, 180f), enemyPrefab.transform.rotation);
            yield return new WaitForSeconds(2);
        }
    }
}
