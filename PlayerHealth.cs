using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    private Slider healthSlider;
    private Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public Animator zanim;
    public ParticleSystem deathps;

    public ParticleSystem hitParticles;
    public Animator anim;

    PlayerBehaviourManager playerManager;

    bool isDead;
    bool damaged;

    RefManager instance;

    void Start()
    {
        instance = RefManager.refManager;
        healthSlider = instance.healthSlider;
        damageImage = instance.dmgImage;

        anim = GetComponent<Animator>();
        //playerAudio = GetComponent<AudioSource>();

        currentHealth = startingHealth;

        playerManager = GetComponent<PlayerBehaviourManager>();


    }


    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        hitParticles.Play();

        //playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    private void Death()
    {
        isDead = true;

        deathps.Play();

        anim.SetTrigger("Die");
        zanim.SetTrigger("Die");

        playerManager.DisableAll();

    }

}