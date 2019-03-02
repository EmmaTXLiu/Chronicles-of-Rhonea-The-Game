using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryEnder : MonoBehaviour
{
    public static StoryEnder storyEnder;

    private GameObject storyCanvas;

    private DialogueManager dialogueManager;


    private GameObject HUDCanvas;

    private RefManager instance;


    // Use this for initialization
    void Start()
    {
        storyEnder = this;

        instance = RefManager.refManager;
        dialogueManager = instance.dialogueManager;
        HUDCanvas = instance.HUDCanvas;
        storyCanvas = instance.storyCanvas;


    }



    public void EndStory()
    {
        DialogueManager.endedStory = true;
        dialogueManager.enabled = false;

        storyCanvas.SetActive(false);
        Debug.Log("endingstory");

        HUDCanvas.SetActive(true);
        Time.timeScale = 1;

    }


}
