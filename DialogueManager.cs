using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager dialogueManager;

    DialogueParser parser;

    //this is the index of the DialogueLine list where we look for the components
    public static int lineNum;

    //these are the components of the DialogueLine
    public static string dialogue, characterName;
    public static string pose;
    public string face;
    string emoji;
    string options;

    //required UI components
    GameObject storyCanvas;
    Animator storyAnim;
    Text dialogueBox;
    Text nameBox;
    GameObject button;
    Image panel;


    //for choosing options
    public bool choosing;
    List<Button> buttons = new List<Button>();
    public static string[] optionCommands;


    //for ending dialogue
    private StoryEnder ender;
    public static bool endedStory;

    //for typewriting
    public bool isTyping;
    private bool instaShow;
    float letterPause = 0.05f;

    //for resetting
    List<Blendshapes> facesShown = new List<Blendshapes>();
    List<GameObject> charShown = new List<GameObject>();

    public SpeechAudio audio;

    RefManager instance;
    SpriteRefManager spriteRef;

    // Use this for initialization
    void Start()
    {
        dialogueManager = this;
        instance = RefManager.refManager;
        spriteRef = SpriteRefManager.spritesRef;

        storyCanvas = instance.storyCanvas;
        panel = storyCanvas.transform.Find("Panel").gameObject.GetComponent<Image>();
        dialogueBox = panel.transform.Find("DialogueBox/DialogueText").gameObject.GetComponent<Text>();
        nameBox = panel.transform.Find("DialogueBox/Name").gameObject.GetComponent<Text>();
        button = instance.choiceButton;
        storyAnim = storyCanvas.GetComponent<Animator>();

        ender = instance.storyEnder;
        parser = DialogueParser.parser;

        lineNum = 0;
        dialogue = "";
        characterName = "";
        pose = "";
        emoji = "";
        face = "";

        choosing = false;
        optionCommands = new string[0];

        endedStory = false;

        isTyping = false;
        instaShow = false;
        choosing = false;
        dialogueBox.text = "";

       
    }


    // Update is called once per frame
    void Update()
    {
        //if we haven't already ended the current story and it says to end the story, end it
        if (dialogue == "!EndStory!" && !endedStory)
        {
            //reinitialize
            lineNum = 0;
            dialogue = "";
            characterName = "";
            pose = "";
            face = "";
            emoji = "";

            choosing = false;
            optionCommands = new string[0];

            endedStory = false;

            isTyping = false;
            instaShow = false;
            choosing = false;

            //this resets the text inside the box itself, REQUIRED
            dialogueBox.text = "";


            //THEN end, which will also disable this script
            ender.EndStory();


        }

        //if (!isTyping && !choosing && !storyAnim.GetBool("CanNext"))
        //{
        //    storyAnim.SetBool("CanNext", true);
        //}
        //else if ((isTyping || !choosing) && storyAnim.GetBool("CanNext"))
        //    storyAnim.SetBool("CanNext", false);

        //on lmb, if player isn't choosing anything
        if (Input.GetMouseButtonDown(0) && !choosing && !isTyping)
        {
            //show the current line
            ShowDialogue();
            storyAnim.Play("DialogueClick");

        }

        //this can only be called after the first if condition is called
        //ie. ShowDialogue has been called and lineNum has already been advanced
        else if (Input.GetMouseButtonDown(0) && !choosing && isTyping)
        {

            instaShow = true;
            storyAnim.Play("DialogueClick");


        }


        nameBox.text = characterName;


    }


    public void ShowDialogue()
    {
        //MUST reset images (turn existing ones grey) before getting the new line

        storyAnim.Play("NextButtonStop");
        storyAnim.SetBool("CanNext", false);
        ResetImages();
        ParseLine();

        //if ENDING, do NOT typewrite or advance line number any further!
        if (dialogue != "!EndStory!")
        {
            StartCoroutine(TypeText(dialogue));
            //StartCoroutine(PlayAudio(dialogue));
            lineNum++;
        }

    }

    IEnumerator PlayAudio(string message)
    {
        //send each character into an array
        char[] charArray = message.ToCharArray();

        //then add the characters one at a time to the now-blank message
        for (int i = 0; i < message.Length - 1; i += 2)
        {
            if (instaShow)
            {
                yield break;
            }

            else if (message[i] != ' ' && message[i] != '.')
            {
                //audio.PlayRandom();
                audio.PlayRoundRobin();
            }
            yield return new WaitForSecondsRealtime(letterPause *2);
        }

    }

    IEnumerator TypeText(string message)
    {
        isTyping = true;

        //send each character into an array
        char[] charArray = message.ToCharArray();

        //reset the message so the dialoguebox is empty at first
        message = "";

        //then add the characters one at a time to the now-blank message
        foreach (char letter in charArray)
        {
            if (instaShow)
            {
                instaShow = false;
                isTyping = false;


                dialogueBox.text = dialogue;
                storyAnim.SetBool("CanNext", true);
                yield break;
            }

            message += letter;

            dialogueBox.text = message;

            yield return 0;
            yield return new WaitForSecondsRealtime(letterPause);
        }

        isTyping = false;
        storyAnim.SetBool("CanNext", true);

    }

        public void ClearButtons()
    {

        for (int i = 0; i < buttons.Count; i++)
        {
            Debug.Log("clearing buttons");

            Debug.Log(buttons.Count);

            Button b = buttons[i];

            Destroy(b.gameObject);
        }

        buttons = new List<Button>();
    }

    void ParseLine()
    {

        //NOT choosing = NORMAL DIALOGUE
        if (parser.GetName(lineNum) != "Choose")
        {
            choosing = false;

            characterName = parser.GetName(lineNum);
            dialogue = parser.GetContent(lineNum);
            pose = parser.GetPose(lineNum);
            face = parser.GetFace(lineNum);
            emoji = parser.GetEmoji(lineNum);

            DisplayImages();

        }

        //if its a player talking, then get the options and make buttons for them
        else
        {

            choosing = true;

            characterName = "";
            dialogue = "";
            pose = "";
            face = "";

            options = parser.GetOptions(lineNum);

            CreateButtons(options);
        }
    }

    void CreateButtons(string options)
    {
        //this gets option1, option2, option3... and stores them in an array
        string[] optionsArray = options.Split('|');

        //the index of our button (used to move button down)
        int i = 0;

        foreach (string option in optionsArray)
        {
            //create our button prefab and turn it on
            GameObject newButton = Instantiate(button, panel.transform);

            newButton.SetActive(true);

            //get the actual button from the GameObject button prefab and the script attached to it
            Button b = newButton.GetComponent<Button>();
            ChoiceButton cb = newButton.GetComponent<ChoiceButton>();

            //split the commands and store them in an array
            optionCommands = option.Split(':');

            //display the optin text on the button
            cb.SetText(optionCommands[0]);

            //get the consequences and set it as the ChoiceBox script's option
            cb.option = optionCommands[1];

            //this should move buttons down below the previous one
            b.transform.localPosition = new Vector3(0, 213 - 55 * i);

            i++; //increment our button so the next one below the first

            //add button to our list of buttons for removal later
            buttons.Add(b);

        }


    }

    //this is called with the previous line, aka BEFORE we parse the new line
    void ResetImages()
    {
        if (characterName == "null")
        {

            return;

        }

        else if (characterName == "???")
        {

        }

         else if (characterName != "")
        {
            //find  the empty with that character
            GameObject character = spriteRef.FindCharacter(characterName);

            if (emoji != "")
            {
                ParticleSystem emojiEffect = character.transform.Find("Emoji/" + emoji).gameObject.GetComponent<ParticleSystem>();
                emojiEffect.Stop();


            }

            //string currpose = pose;

            //this should find the real character object
            //Transform trans = character.GetComponentInChildren<Transform>();

            //get the AC script
            //SpriteAC animControl = character.GetComponentInChildren<SpriteAC>();

            //this would make the character smaller by 50 on all axis's
            //trans.localScale -= new Vector3(50, 50, 50);

            EyeBlink blinker = character.GetComponentInChildren<EyeBlink>();

            //if this character can blink and its currently disabled, turn it back on
            if (blinker != null && blinker.enabled == false)
            {
                blinker.enabled = true;
            }



        }
    }



    void DisplayImages()
    {
        //update the canvas animator
        storyAnim.SetInteger("LineNum", lineNum);
        storyAnim.SetTrigger("PlayAnim");

        if (characterName == "null")
        {
            //this is the last display function called before shutdown, need to reset blendshapes
            foreach (Blendshapes face in facesShown)
                face.ResetShapes();

            foreach (GameObject charrie in charShown)
                charrie.SetActive(false);          
        }

        else if (characterName == "???" || characterName == "")
        {

        }

        else if (characterName != "")
        {
            //find  the empty with that character
            GameObject character = spriteRef.FindCharacter(characterName);
            string currpose = pose;

            //add it to the list of charries if its new
            if (!charShown.Contains(character))
            {
                charShown.Add(character);
            }

            character.SetActive(true);

            if (pose != "null" && pose != "")
            {
                //get the AC script
                SpriteAC animControl = character.GetComponentInChildren<SpriteAC>();
                //play the pose for that character
                animControl.Animate(pose);
            }

            if (face != "null" && face != "")
            {
                Blendshapes faceControl = character.GetComponentInChildren<Blendshapes>();
                EyeBlink blinker = character.GetComponentInChildren<EyeBlink>();

                //if the pose contains a no Blinking command
                if (blinker != null && face.Contains("_noBlink"))
                {
                    //remove the command from the pose and disable blinking
                    face = face.Replace("_noBlink", "");
                    blinker.enabled = false;
                }

                //turn the blinker back on if it was off previously and the current pose doesn't contain noBlink
                else if (blinker != null && blinker.enabled == false)
                {
                    blinker.enabled = true;
                }

                //change the expression of the character
                faceControl.BlendShape(face);

                //if this is a new character, add it to the list of faces shown so we can reset them all later
                if (!facesShown.Contains(faceControl))
                {
                    facesShown.Add(faceControl);
                }
            }

            if (emoji != "null" && emoji != "")
            {

                GameObject emojiHolder = character.transform.Find("Emoji").gameObject;
                ParticleSystem emojiEffect = emojiHolder.transform.Find(emoji).gameObject.GetComponent<ParticleSystem>();
                emojiEffect.Play();


            }


        }
    }

}