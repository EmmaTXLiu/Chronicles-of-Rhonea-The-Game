using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerManager : MonoBehaviour {

    public GameObject bow;
    public GameObject sword;
    public GameObject orb;

    public ParticleSystem orbFX;
    public ParticleSystem orbSpellFX;

    public bool bowMode = true;
    public bool swordMode = false;
    public bool orbMode = false;

    PlayerBehaviourManager playerManager;

    float cooldown = 5f;
    float timer = 0f;

	void Start () {

        playerManager = GetComponentInParent<PlayerBehaviourManager>();
        timer = cooldown;
        
        ActivateBow();

	}

    void Update()
    {

        if (timer <= 0f)
        {
            if (Input.GetButton("1"))
            {
                ActivateBow();

            }
            //else if (Input.GetButton("2"))
            //{
            //    ActivateOrb();
            //}
            else if (Input.GetButton("3"))
            {
                ActivateSword();
            }
        }

        else
        {
            timer -= Time.deltaTime;
        }
        
    }

    void ActivateBow()
    {
        bowMode = true;
        swordMode = false;
        orbMode = false;

        orbFX.Play();

        bow.SetActive(true);
        sword.SetActive(false);
        orb.SetActive(true);

        timer = cooldown;
    }

    void ActivateSword()
    {
        bowMode = false;
        swordMode = true;
        orbMode = false;

        playerManager.DisableArrow();
        playerManager.DisableCircleSpell();
        orbFX.Stop();

        bow.SetActive(false);
        sword.SetActive(true);
        orb.SetActive(false);

        timer = cooldown;
    }

    //void ActivateOrb()
    //{
    //    bowMode = false;
    //    swordMode = false;
    //    orbMode = true;

    //    orbFX.Play();

    //    playerManager.DisableArrow();

    //    bow.SetActive(false);
    //    sword.SetActive(false);
    //    orb.SetActive(true);

    //    timer = cooldown;
    //}

    public void PlayOrbSpellEffect()
    {
        orbSpellFX.Play();
    }
	
}
