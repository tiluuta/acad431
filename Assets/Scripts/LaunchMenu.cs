using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;


public class LaunchMenu : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bestText;
    private UserData data = new UserData();
    public bool dataFound;



    void Start()
    {
        timeText.enabled = false;
        bestText.text = "";

        string path = Application.persistentDataPath + "/userdata.json";

        if(File.Exists(path)){
            dataFound = true;
            data.Load();
            GetData();
        } else{
            dataFound = false;
        }
    }

    public void GetData(){
        timeText.enabled = true;
        int totalMin = (int)data.bestTime;
        int hours = totalMin / 60;
        int min = totalMin % 60;
        bestText.text = "" + hours + ":" + min;
    }

    public void StartButton(){
        //if user has played before: skip settings
        if(dataFound){
            //scene with main game
        }
        //if user is new: go to Settings menu
        if(!dataFound){
            SceneManager.LoadScene("Settings");
        }
    }
}
