using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;

public class DialogueParser : MonoBehaviour
{
    public static DialogueParser parser;

    //define DialogueLine struct
    struct DialogueLine
    {
        public string name;
        public string content;
        public string pose;
        public string face;
        public string emoji;
        public string options;

        public DialogueLine(string Name, string Content, string Pose, string Face, string Emoji, string Options)
        {
            name = Name;
            content = Content;
            pose = Pose;
            face = Face;
            emoji = Emoji;
            options = Options;
        }
    }

    public int fileNum = 0;
    RefManager instance;
    List<DialogueLine> lines = new List<DialogueLine>();

    void Awake()
    {
        parser = this;
        instance = RefManager.refManager;
    }

    //use this to re-initialize new story by erasing the lists
    public void ResetParser()
    {
        lines = new List<DialogueLine>();

    }


    public void Import(TextAsset file)
    {
        Debug.Log("parsing " + file.name);

        fileNum = int.Parse(file.name);

        //creates array of all the lines in the text file
        string[] rawLines = file.text.Split('\n');

        //an array to store the pieces of each line
        string[] lineData = new string[0];

        //these pieces will be used to make a new entry into our final DialogueLine list
        DialogueLine lineEntry = new DialogueLine();

        foreach (string line in rawLines)
        {
           
                //sort the pieces of each line in the array
                lineData = line.Split(';');

                //put each of those pieces into a new dialogue line:
                // 0 = name, 1 = dialogue, 2 = pose, 3 = face, 4 = options
                lineEntry = new DialogueLine(lineData[0], lineData[1], lineData[2], lineData[3], lineData[4], lineData[5]);

                //add our newly constructed DialogueLine to the list
                lines.Add(lineEntry);  
        }

        instance.storyCanvas.GetComponent<StoryCanvasAnimManager>().UpdateAnim(fileNum);
    }

    //public void UpdateAnim(int fN)
    //{
    //    Debug.Log(fN);
    //    instance.storyCanvas.GetComponent<Animator>().SetInteger("FileNum", fN);
    //}

    //call these functions from DialogueManager to get the info from Parser
    //these will get the struct components of the DialogueLine with index [lineNumber] in the list 


    public string GetName(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].name;
        }
        return "";
    }

    public string GetContent(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].content;
        }
        return "";
    }

    public string GetPose(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].pose;
        }
        return "";
    }

    public string GetFace(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].face;
        }
        return "";
    }

    public string GetEmoji(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].emoji;
        }
        return "";
    }

    public string GetOptions(int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].options;
        }
        return "";
    }

}