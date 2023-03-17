using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemies;
    [SerializeField] private GameObject coins;
    [SerializeField] Vector2 range;
    [SerializeField] private GameObject Square;
    [SerializeField] private float intervalEnemy = 1.0f;
    [SerializeField] private float intervalCoin = 3.0f;

    public Transform spawnPos;
    public bool hasStartedCoroutine;


    public void StartCoroutines(bool isPlaying)
    {
        if(isPlaying == true)
        {
            hasStartedCoroutine = true;
            StartCoroutine(SpawnEnemies());
            StartCoroutine(SpawnCoins());
        }
        else
        {
            StopAllCoroutines();
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            float height = Square.transform.localScale.y;
            Vector2 pos = spawnPos.position + new Vector3(0, Random.Range(-range.y, range.y));
            Instantiate(enemies, pos, Quaternion.identity);
            yield return new WaitForSeconds(intervalEnemy);
        }
    }
    IEnumerator SpawnCoins()
    {
        while (true)
        {
            float height = Square.transform.localScale.y;
            Vector2 pos = spawnPos.position + new Vector3(0, Random.Range(-range.y, range.y));
            Instantiate(coins, pos, Quaternion.identity);
            yield return new WaitForSeconds(intervalCoin);
        }
    }
}