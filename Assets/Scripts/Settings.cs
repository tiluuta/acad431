using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Settings : MonoBehaviour
{
    //XR controls
    public XRNode handRole = XRNode.LeftHand;

    public static GameObject userControl; 
    public static GameObject userDifficulty;
    public static GameObject userArtwork;
    public static TextMeshProUGUI titleText;

    public static GameObject controlsCanvas;
    public static GameObject difficultyCanvas;
    public static GameObject artworkCanvas;

    public GameObject portraitPrefab;
    public GameObject stillLifePrefab;


    public static GameObject startMenu;
    public static GameObject pauseMenuPrefab;
    public static int pageNum = 1;

    void Start()
    {
        //default option
        startMenu = GameObject.Find("Settings Menu");
        userControl = GameObject.Find("Shoulder Opt");
        userDifficulty = GameObject.Find("Beginner Opt");
        userArtwork = GameObject.Find("Portrait Opt");
        controlsCanvas = GameObject.Find("Controls Canvas");
        difficultyCanvas = GameObject.Find("Difficulty Canvas");
        difficultyCanvas.SetActive(false);
        artworkCanvas = GameObject.Find("Artwork Canvas");
        artworkCanvas.SetActive(false);


        //find prefabs
        pauseMenuPrefab = GameObject.Find("Pause Menu");
        pauseMenuPrefab.SetActive(false);
    }
    
    void Update(){
        InputDevices.GetDeviceAtXRNode(handRole).TryGetFeatureValue(CommonUsages.menuButton,out bool mButton);

        if(mButton){
            Debug.Log("MENU BUTTON PRESSED!!");
            PauseMenu();
        }
    }

    public static void PauseMenu(){
        Debug.Log("open pause menu");
        Instantiate(pauseMenuPrefab, GameObject.Find("UI").transform);
        
        //pause timer
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
    }

    //go to next page
    public static void ConfirmSelect(){
        if(pageNum == 1){
            controlsCanvas.SetActive(false);
            artworkCanvas.SetActive(true);
            pageNum++;
        } else if (pageNum == 2){
            artworkCanvas.SetActive(false);
            difficultyCanvas.SetActive(true);
            //Settings.GenerateDifficulties(portraitPrefab);
            pageNum++;
        } else if (pageNum == 3){
            difficultyCanvas.SetActive(false);
            startMenu.SetActive(false);
            pageNum = 0;
        }
        SaveSettings();
    }

    //change difficulty options based on what artwork user chose
    public void GenerateDifficulties(GameObject artworkPrefab){
        Instantiate(artworkPrefab,difficultyCanvas.transform);
    }

    //saves settings into data in json
    public static void SaveSettings(){
        Debug.Log("now saving");
        UserData data = new UserData();
        //load old data
        data.Load();
        //update with new control, difficulty, artwork
        data.userControl = userControl.name;
        data.userDifficulty = userDifficulty.name;
        data.userArtwork = userArtwork.name;
        data.Save();
    }


}
