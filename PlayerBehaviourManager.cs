using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourManager : MonoBehaviour {

    PlayerHealth health;
    public PlayerAttacks circlespell;
    PlayerMoveGame movement;

    public PlayerFire zephirFire;
    public ParticleAttack arrowAttacks;


	void Start () {

        health = GetComponent<PlayerHealth>();
        circlespell = GetComponent<PlayerAttacks>();
        movement = GetComponent<PlayerMoveGame>();

	}

    public void DisableMovement()
    {
        movement.enabled = false;

    }

    public void DisableFire()
    {
        zephirFire.enabled = false;
    }

    public void DisableArrow()
    {
        arrowAttacks.enabled = false;
    }

    public void DisableCircleSpell()
    {
        circlespell.enabled = false;
    }

    public void DisableAll()
    {
        movement.enabled = false;
        zephirFire.enabled = false;
        arrowAttacks.enabled = false;
        circlespell.enabled = false;
    }

    public void ReEnable()
    {
        if (!movement.isActiveAndEnabled)
        {
            movement.enabled = true;
        }
        if (!zephirFire.isActiveAndEnabled)
        {
            zephirFire.enabled = true;
        }
        if (!arrowAttacks.isActiveAndEnabled)
        {
            arrowAttacks.enabled = true;
        }
        if (!circlespell.isActiveAndEnabled)
        {
            circlespell.enabled = true;
        }
    }

    public void enableFire()
    {
        if (!zephirFire.isActiveAndEnabled)
        {
            zephirFire.enabled = true;
        }
    }
	

}
