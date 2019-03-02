using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerAttacks : MonoBehaviour {

    public GameObject attackStunAOE;

    public float circleSpellCD = 10f;
    float circlecurrentCD;
    float spellCastTime; //how long it takes player to cast spell
    float currentCastTime; 
    bool isCasting;
    public bool orbOut = false;

    Animator anim;
    public Animator daggerAnim;

    private Image circleSpellCDImage;

    RefManager instance;
    PlayerBehaviourManager playerManager;

    // Use this for initialization
    void Start() {

        anim = GetComponent<Animator>();
        playerManager = GetComponent<PlayerBehaviourManager>();

        circlecurrentCD = 0f;
        currentCastTime = 0f;
        isCasting = false;

        instance = RefManager.refManager;
        circleSpellCDImage = instance.circleSpellCD;

        //tweak these
        spellCastTime = 1.5f;

    }

    // Update is called once per frame
    void Update() {

        circleSpellCDImage.fillAmount = circlecurrentCD / circleSpellCD;


        //circlespell start channeling
        if (circlecurrentCD <= 0 && currentCastTime <= 0 && !isCasting) //off cd, not casting
        {
            if (Input.GetButton("Fire2")) 
            {
                ChannelCircleSpell(); //start channeling
            }

            else //maintain parameters
            {
                circlecurrentCD = 0;
                currentCastTime = 0;
                isCasting = false; 
            }
        }

        else if (currentCastTime > 0 && isCasting) //if player is currently casting
        {
            currentCastTime -= Time.deltaTime; //decrement cast time
        }

        else if (currentCastTime <= 0 && isCasting) //the moment you finish casting
        {
            isCasting = false; //stop casting
            PlaceCircleSpell(); //place the magic
        }
        else if (currentCastTime <=0 && !isCasting && circlecurrentCD > 0) //done casting, placed spell, on CD now
        {
            circlecurrentCD -= Time.deltaTime; //now decrement CD
        }

        //basic orb attack
        if (Input.GetButtonDown("rmb") && !orbOut)
        {
            OrbBasicAttack();
            orbOut = true;
        }
    }


    //channel spell in place
    void ChannelCircleSpell() 
    {
        anim.SetTrigger("CastingCircleSpell"); //play animation
        daggerAnim.SetTrigger("CastingCircleSpell");

        playerManager.DisableArrow();
        playerManager.DisableMovement();

        isCasting = true; //player is now casting spell

        currentCastTime = spellCastTime; //initiate cast time 
        circlecurrentCD = circleSpellCD; //put skill on CD
    }


    //done casting, place spell down
    void PlaceCircleSpell() 
    {
        Instantiate(attackStunAOE, transform.position, transform.rotation);
        Invoke("ReEnableAttacks", 0.7f);
    }

    void ReEnableAttacks()
    {
        playerManager.ReEnable();
    }

    void OrbBasicAttack()
    {
        anim.SetTrigger("OrbBasic"); //play animation
        daggerAnim.SetTrigger("OrbBasic");

        playerManager.DisableArrow();
        Invoke("ReEnableAttacks", 2f);
        Invoke("OrbOver", 2f);

    }

    void OrbOver()
    {
        orbOut = false;
    }


}


