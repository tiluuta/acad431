using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordTime : MonoBehaviour
{
    private static float startTime;
    public static float elapsedTime;
    public static bool isPaused = false;

    void Start()
    {
        ResetTime();
    }

    void Update()
    {
        if(GameObject.Find("Pause Menu") || GameObject.Find("Pause Menu(Clone)")){
           PauseTime();
        }else {
            ResumeTime();
        }
    }

    public static void SaveTime(){
        Debug.Log("Elapsed time: " + elapsedTime.ToString("F2"));

        UserData data = new UserData();
        //load old data
        data.Load();

        data.latestTime = elapsedTime;
        if(elapsedTime > data.bestTime){
            data.bestTime = elapsedTime;
        }

        //save latest time (and best time)
        data.Save();
    }

    public static void PauseTime(){
        isPaused = true;
    }

    public static void ResumeTime(){
        isPaused = false;
        elapsedTime = (Time.realtimeSinceStartup - startTime)/60f;
    }

    public static void ResetTime(){
        startTime = Time.realtimeSinceStartup;        
    }
}
