using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S0StoryInitialize : MonoBehaviour {

    //necessary components
    private GameObject storyCanvas;
    DialogueParser dialogueParser;
    private DialogueManager dialogueManager;
    private GameObject HUDCanvas;
    S0Manager storyManager;

    Collider trigger;


    private static int orbsHit;
    private TextAsset fileToLoad;
    bool newEntry;

    private RefManager instance;

    // Use this for initialization
    void Start () {

        dialogueParser = DialogueParser.parser;
        storyManager = S0Manager.sManager;

        newEntry = true;

        instance = RefManager.refManager;
        storyCanvas = instance.storyCanvas;
        dialogueManager = instance.dialogueManager;
        HUDCanvas = instance.HUDCanvas;

    }




    void OnTriggerEnter(Collider other)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Collider playerCollider = player.GetComponent<Collider>();

        if (other == playerCollider && newEntry)
        {
            Time.timeScale = 0;

            //choose file depending on orbs
            storyManager.LoadFile();
            fileToLoad = storyManager.fileToLoad;

            dialogueParser.ResetParser();
            dialogueParser.Import(fileToLoad);

            //enable the manager and restart a new dialogue
            dialogueManager.enabled = true;
            DialogueManager.endedStory = false;


            if (HUDCanvas != null)
            {
                HUDCanvas.SetActive(false);
            }


            storyCanvas.SetActive(true);
            Animator storyAnim = storyCanvas.GetComponent<Animator>();
            storyAnim.Play("StoryCanvasStart");
            storyAnim.SetBool("CanNext", true);


            //increment orb hit
            S0Manager.orbsHit++;

            newEntry = false;
        }

    }



}
