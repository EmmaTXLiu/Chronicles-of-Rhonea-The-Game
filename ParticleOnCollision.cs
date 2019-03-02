using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnCollision : MonoBehaviour {

    float orbDamage = 80f;

    void OnParticleCollision(GameObject other)
    {

        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        EnemyMovement enemyMove = other.GetComponent<EnemyMovement>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(orbDamage, other.transform.position); //enemy take damage

            enemyMove.Interrupt();

        }
    }
}
