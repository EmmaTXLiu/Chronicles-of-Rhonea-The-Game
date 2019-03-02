using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnPoint : MonoBehaviour
{
    PlayerHealth playerHealth;

    GameObject player;

    public GameObject enemy;
    public float spawnTime = 3f;
    Vector3 spawnPoints;

    public float maxRange = 50f;

    public float xBound = 250f;
    public float zBound = 250f;

    float x;
    float y;
    float z;

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);

        player = GameObject.FindGameObjectWithTag("Player");

        playerHealth = player.GetComponent<PlayerHealth>();

    }


    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }


        x = Random.Range(player.transform.position.x - maxRange, player.transform.position.x + maxRange);
        z = Random.Range(player.transform.position.z - maxRange, player.transform.position.z + maxRange);


        //the following keeps x and y inside the map boundaries
        if (x > xBound)
        {
            x = xBound - 2f;
        }
        else if (x < -xBound)
        {
            x = -xBound + 2f;
        }

        if (z > zBound)
        {
            z = zBound - 2f;
        }
        else if (z < -zBound)
        {
            z = -zBound + 2f;
        }


        spawnPoints = new Vector3(x, 0f, z);

        Instantiate(enemy, spawnPoints, Random.rotation);

    }
}
