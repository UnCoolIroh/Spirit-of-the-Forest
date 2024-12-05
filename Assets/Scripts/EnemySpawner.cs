using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();

    public void Spawning()
    {
        Instantiate(Enemies[Random.Range(0, Enemies.Count)], transform.position, Quaternion.identity);
    }
}
