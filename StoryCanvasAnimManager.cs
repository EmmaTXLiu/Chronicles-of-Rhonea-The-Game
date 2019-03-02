using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCanvasAnimManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateAnim(int fN)
    {
        Debug.Log(fN);
        GetComponent<Animator>().SetInteger("FileNum", fN);
    }

}
