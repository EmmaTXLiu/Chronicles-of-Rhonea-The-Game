using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectionManager : MonoBehaviour {

    public static AffectionManager affectionManager;

	// Use this for initialization
	void Start () {

        //establish singleton
        if (affectionManager == null)
        {
            DontDestroyOnLoad(gameObject);
            affectionManager = this;
        }
        else if (affectionManager != null)
        {
            Destroy(gameObject);
        }
	}

    public void ChangeAffection(string name, int change)
    {
        //if (name == "Zander")
        //{
        //    GameControl.affection.zander += change;
        //}
        //else if (name == "Nathaniel")
        //{
        //    GameControl.affection.nathaniel += change;
        //}
        //else if (name == "Thistle")
        //{
        //    GameControl.affection.thistle += change;
        //}
        //else if (name == "Embrey")
        //{
        //    GameControl.affection.embrey += change;
        //}
        //else if (name == "Luna")
        //{
        //    GameControl.affection.luna += change;
        //}
        //else if (name == "Lily")
        //{
        //    GameControl.affection.lily += change;
        //}
        //else
        //{
        //    return;
        //}
    }
}
