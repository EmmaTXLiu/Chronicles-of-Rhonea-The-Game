using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAC : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	//void Update () {

 //       if (Input.GetButton("Fire1"))
 //           Animate();

	//}

    public void Animate(string playThis)
    {
        //this should trigger transition back to idle state from any state
        //anim.SetTrigger("Reset");

        if (anim == null)
            return;

        else
            anim.Play(playThis);
    }
}
