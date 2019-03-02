using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerFire : MonoBehaviour
{
    public Animator zAnim;
    bool firing = false;

    public int maxMana = 1000;
    public int manaPerTick = 1;
    public int manaRegen = 5;
    private Slider manaSlider;
    int currentMana;

    public ParticleSystem attackParticles;
    public float damagePerShot = 40;

    float timer;

    private RefManager instance;

    // Use this for initialization
    private void Start ()
    {
        instance = RefManager.refManager;
        manaSlider = instance.manaSlider;

        manaSlider.maxValue = maxMana;
        manaSlider.value = maxMana;

     }

    private void Fire()
    {
        //only play attack animation
        attackParticles.Stop();
        attackParticles.Play();
        manaSlider.value -= manaPerTick;
        firing = true;


    }

    private void OnTriggerStay(Collider other)
    {
        
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        //if shooting and hit object has an enemy health script
        if (Input.GetButton("Fire1") && enemyHealth != null && manaSlider.value > manaPerTick) 
        {
            //stop then play attack particles
            attackParticles.Stop();
            attackParticles.Play();

            enemyHealth.TakeDamage(damagePerShot * Time.deltaTime, other.transform.position);

            manaSlider.value -= manaPerTick;
            firing = true;

        }
        else
        {
            attackParticles.Stop();
        }
    }




    // Update is called once per frame
    private void Update ()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {

            if (manaSlider.value > manaPerTick)
            {
                Fire();
            }

            else
            {
                manaSlider.value -= manaPerTick;
            }
        }

        else
        {
            manaSlider.value += manaRegen;
            firing = false;
        }

        zAnim.SetBool("Firing", firing);

    }
}
