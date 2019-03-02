using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAttack : MonoBehaviour {

    public ParticleSystem arrow;

    float orbDamage = 25f;

    public ParticleSystem[] chargedArrows;

    public float cd = 3f;

    float timer;
    int charges = 0;

    Animator playerAnim;
    public Animator daggerAnim;

	// Use this for initialization
	void Start () {

        timer = 0f;

        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {

        if (timer <=0 && Input.GetButton("Space"))
        {
            if (charges >= 4)
            {
                foreach (ParticleSystem p in chargedArrows)
                {
                    p.Stop();
                    p.Play();

                    charges = 0;
                }

            }

            else
            {
                arrow.Stop();
                arrow.Play();

                charges++;
            }

            playerAnim.SetTrigger("ArrowAttack");
            daggerAnim.SetTrigger("ArrowAttack");

            timer = cd;

        }
        else if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
		
	}



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
