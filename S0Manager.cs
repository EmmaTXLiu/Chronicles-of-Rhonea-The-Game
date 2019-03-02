using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S0Manager : MonoBehaviour {

    public static S0Manager sManager;

    GameObject EnemyManager;

    //these choose which files to load for the orbs
    public TextAsset[] files;
    public TextAsset fileToLoad;
    public static int orbsHit;


    //these things need to be off at the beginning of this scene

    public GameObject zephirMat;
    private SkinnedMeshRenderer Zephir;
    public GameObject dagger;

    GameObject player;
    PlayerBehaviourManager playerManager;
    RandomSpawnPoint enemySpawner;
    bool began = false;

    // Use this for initialization
    void Awake () {

        sManager = this;

        Cursor.visible = false;

        orbsHit = 0;

        EnemyManager = GameObject.Find("EnemyManager");

        player = GameObject.FindGameObjectWithTag("Player");

        zephirMat = player.transform.Find("Zephir 1.1/Zephir 1.1/Body_001").gameObject;

        //make Zephir invisible
        Zephir = zephirMat.GetComponent<SkinnedMeshRenderer>();
        Zephir.enabled = false;
        dagger.SetActive(false);

        playerManager = player.GetComponent<PlayerBehaviourManager>();
        playerManager.DisableArrow();
        playerManager.DisableCircleSpell();
        playerManager.DisableFire();


        //do not spawn mobs
        enemySpawner = EnemyManager.GetComponent<RandomSpawnPoint>();
        enemySpawner.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {

        //begin spawning enemies after all orb dialogue has been triggered
        if (orbsHit >= 5 && !began)
        {
            //this function will turn everything back on
            BeginGame();

        }
		
	}

    public void LoadFile()
    {
        if (orbsHit == 0)
        {
            fileToLoad = files[0];
        }

        else if (orbsHit == 1)
        {
            fileToLoad = files[1];
        }

        else if (orbsHit == 2)
        {
            fileToLoad = files[2];
        }

        else if (orbsHit == 3)
        {
            fileToLoad = files[3];
        }

        else if (orbsHit == 4)
        {
            fileToLoad = files[4];
        }
    }


    void BeginGame()
    {
        Zephir.enabled = true;
        playerManager.enableFire();
        enemySpawner.enabled = true;
        began = true;
    }
}
