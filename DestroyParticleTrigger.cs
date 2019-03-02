using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleTrigger : MonoBehaviour
{

 //attach this to a particle effect dialogue trigger empty with the StoryInitializer script
 //after a frame time-dependent delay this will stop all animations and destroy the gameObject

    Collider trigger;

    private float timer;

    bool readyDestroy;

    Component[] particles;

    // Use this for initialization
    void Start()
    {

        timer = 0f;

        particles = GetComponentsInChildren<ParticleSystem>();

        readyDestroy = false;

    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (readyDestroy)
        {
            DestroySlowly();
        }



    }

    void OnTriggerEnter(Collider other)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Collider playerCollider = player.GetComponent<Collider>();

        if (other == playerCollider)
        {

            timer = 0f;

            readyDestroy = true;

        }
    }

    void DestroySlowly()
    {

        if (timer >= 10f)
        {
            Debug.Log("destroying");
            Destroy(gameObject);
        }

        else if (timer >= 3f)
        {
            foreach (ParticleSystem p in particles)
                p.Stop();

        }

    }
}

