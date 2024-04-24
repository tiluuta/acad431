using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.EnhancedTouch;


public class Settings : MonoBehaviour
{

    public static GameObject userControl; 
    public static GameObject userDifficulty;
    public static GameObject userArtwork;
    public static GameObject userSize;
    public static TextMeshProUGUI titleText;

    public static GameObject controlsCanvas;
    public static GameObject difficultyCanvas;
    public static GameObject artworkCanvas;
    public static GameObject sizeCanvas;

    public static GameObject portraitDiff;
    public static GameObject stillLifeDiff;


    public static GameObject settingsMenu;

    public static int pageNum = 1;

    void Start()
    {
        //default option
        settingsMenu = GameObject.Find("Settings Menu(Clone)");
        userControl = GameObject.Find("Shoulder Opt");
        userSize = GameObject.Find("Small");
        userArtwork = GameObject.Find("Artwork Canvas").transform.Find("Row").gameObject.transform.Find("1").gameObject;
        userDifficulty = GameObject.Find("Difficulty Canvas").transform.Find("Portrait_Difficulties").gameObject.transform.Find("1").gameObject;


        portraitDiff = GameObject.Find("Portrait_Difficulties");
        stillLifeDiff = GameObject.Find("Still Life_Difficulties");

        controlsCanvas = GameObject.Find("Controls Canvas");
        difficultyCanvas = GameObject.Find("Difficulty Canvas");
        difficultyCanvas.SetActive(false);
        artworkCanvas = GameObject.Find("Artwork Canvas");
        artworkCanvas.SetActive(false);
        sizeCanvas = GameObject.Find("Sizes Canvas");
        sizeCanvas.SetActive(false);
    }

    //select object if different from already selected object
    public static void NewSelected(GameObject newOpt){
        if(userControl != newOpt){
            if(newOpt.tag == "Control"){
                userControl.GetComponent<HoldSelect>().slider.value = 0;
                userControl.GetComponent<HoldSelect>().selected = false;

                userControl = newOpt;
                Debug.Log("Selected: " + userControl);
            }
        }
        if(userDifficulty != newOpt){
            if(newOpt.tag == "Difficulty"){
                userDifficulty.GetComponent<HoldSelect>().slider.value = 0;
                userDifficulty.GetComponent<HoldSelect>().selected = false;

                userDifficulty = newOpt;
                Debug.Log("Selected: " + userDifficulty);
            }
        }
        if(userArtwork != newOpt){
            if(newOpt.tag == "Artwork"){
                userArtwork.GetComponent<HoldSelect>().slider.value = 0;
                userArtwork.GetComponent<HoldSelect>().selected = false;

                userArtwork = newOpt;
                Debug.Log("Selected: " + userArtwork);
            }
        }
        if(userSize != newOpt){
            if(newOpt.tag == "Size"){
                userSize.GetComponent<HoldSelect>().slider.value = 0;
                userSize.GetComponent<HoldSelect>().selected = false;

                userSize = newOpt;
                Debug.Log("Selected: " + userSize);
            }
        }
    }

    //go to next page
    public static void ConfirmSelect(){
        if(pageNum == 1){
            controlsCanvas.SetActive(false);
            sizeCanvas.SetActive(true);
            pageNum++;
        }else if(pageNum == 2){
            sizeCanvas.SetActive(false);
            artworkCanvas.SetActive(true);
            pageNum++;
        } else if (pageNum == 3){
            artworkCanvas.SetActive(false);
            difficultyCanvas.SetActive(true);

            //change images for difficulty depending on artwork chosen
            Debug.Log(userArtwork.name);
            if (userArtwork.name == "2"){
                stillLifeDiff.SetActive(true);
                portraitDiff.SetActive(false);
            } else{
                stillLifeDiff.SetActive(false);
                portraitDiff.SetActive(true);
            }

            pageNum++;
        } else if (pageNum == 4){
            difficultyCanvas.SetActive(false);
            pageNum = 1;
            PauseMenu.CloseMenu(GameObject.Find("Settings Menu(Clone)"));
        }

        SaveSettings();
    }

    //saves settings into data in json
    public static void SaveSettings(){
        UserData data = new UserData();
        //load old data
        data.Load();
        //update with new control, difficulty, artwork
        data.userControl = userControl.name;
        if(userDifficulty == null){
            data.userDifficulty = "1";
        } else{
            data.userDifficulty = userDifficulty.name;
        }
        if(userArtwork == null){
            data.userArtwork = "1";
        } else{
            data.userArtwork = userArtwork.name;
        }
        data.userSize = userSize.name;
        data.Save();
    }

}
