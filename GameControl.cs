using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

    public static GameControl control;

    // ~~ ** //DECLARE VARIABLES HERE // ~~ ** //
    //public static int coin;
    //public static Affection affection;
    //public static GameObject playerModel;
    public static int highScore;


	void Awake () {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }

        CreateSaveFile();
    }

    public static void CreateSaveFile()
    {
        if (!File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            FileStream newFile = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        }
    }


    public static void Save()
    {
        //this thing writes for us
        BinaryFormatter bf = new BinaryFormatter();

        //this gets the file path from Unity's persistent path, the file called playerInfo.dat, and we're opening it
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

        //instantiate a new PlayerData class to store
        PlayerData data = new PlayerData();

        // ~~ ** //SAVE VARIABLES HERE // ~~ ** //
        //data.coin = coin;
        //data.affection = affection;
        //data.playerModel = playerModel;
        data.highScore = highScore;
        //Debug.Log(highScore);


        //bf will take data, which is the serializeable class, and save it to the file which FileStream opened
        bf.Serialize(file, data);

        //then close the file
        file.Close();
        

    }

    public static void Load()
    {
            if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            //the (PlayerData) casts it from a generic data to a PlayerData class
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();


            // ~~ ** //LOAD VARIABLES HERE // ~~ ** //
            //coin = data.coin;
            //affection = data.affection;
            //playerModel = data.playerModel;
            highScore = data.highScore;

        }
    }

}

//this is a private class that will mirror the above monobehaviour
//MAKE SURE ALL THE VARIABLES EXIST HERE TOO

//the following class can be saved to a file
[Serializable]
class PlayerData
{
    //public int coin;
    //public Affection affection;
    //public GameObject playerModel;
    public int highScore;

}
