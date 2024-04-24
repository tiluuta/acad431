using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class User{

}

[System.Serializable]
public class UserData
{
    public float latestTime;    //total time of last game
    public float bestTime;  //if new time is less than latest time
    public string userDifficulty;   //last selected difficulty level
    public string userControl;  //last selected control type
    public string userArtwork;  //last selected artwork
    public string userSize;  //last selected canvas size



    //default constructor
    public UserData(){
        latestTime = 0;
        bestTime = 0;
        userControl = "Shoulder Opt";
        userDifficulty = "1";
        userArtwork = "1";
        userSize = "Small";
    }

    public UserData(UserData newData){
        latestTime = newData.latestTime;
        bestTime = newData.bestTime;
        userDifficulty = newData.userDifficulty;
        userControl = newData.userControl;
        userArtwork = newData.userArtwork;
        userSize = newData.userSize;
    }

    //load from json
    public void Load(){
        string path = Application.persistentDataPath + "/userdata.json";

        if(File.Exists(path))
            {
                string textData = File.ReadAllText(path);
                latestTime = JsonUtility.FromJson<UserData>(textData).latestTime;
                bestTime = JsonUtility.FromJson<UserData>(textData).bestTime;
                userControl = JsonUtility.FromJson<UserData>(textData).userControl;
                userDifficulty = JsonUtility.FromJson<UserData>(textData).userDifficulty;
                userArtwork = JsonUtility.FromJson<UserData>(textData).userArtwork;
                userSize = JsonUtility.FromJson<UserData>(textData).userSize;
            }
    }

    //save into json
    public void Save(){
        string path = Application.persistentDataPath + "/userdata.json";
        Debug.Log("Saved into: " + path);
        string jsonString = JsonUtility.ToJson(this, true);
        File.WriteAllText(path, jsonString);
    }

    

}
