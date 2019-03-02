using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RefManager : MonoBehaviour {

    public static RefManager refManager;



    public Camera cam;

    public Image blinkCD;
    public Slider healthSlider;
    public Image dmgImage;
    public Image circleSpellCD;
    public Slider manaSlider;

    public DialogueManager dialogueManager;

    public GameObject HUDCanvas;
    public GameObject storyCanvas;
    public StoryEnder storyEnder;

    public GameObject choiceButton;


	// Use this for initialization
	void Awake () {
        refManager = this;
		
	}
	

}
