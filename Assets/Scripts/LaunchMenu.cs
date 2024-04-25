using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.EventSystems;



public class LaunchMenu : MonoBehaviour
{
    //XR controls
    public XRNode handRole = XRNode.LeftHand;

    public GameObject SettingsMenu;
    public GameObject settingsCanvas;


    //user score data
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScore;


    //user settings data
    public GameObject controlSetting;
    public TextMeshProUGUI sizeSetting;
    public GameObject artworkSetting;
    public GameObject difficultySetting;


    public List<Sprite> settingImages= new List<Sprite>();
    private UserData data = new UserData();
    public bool dataFound;
    public GameObject pauseMenu;


    void Start()
    {
        GameObject.Find("Pause Menu").SetActive(false);
        SettingsMenu.SetActive(false);
        string path = Application.persistentDataPath + "/userdata.json";

        if(File.Exists(path)){
            dataFound = true;
            data.Load();
        } else{
            dataFound = false;
            data.Save();
        }

        GetData();
    }

    void Update(){
        InputDevices.GetDeviceAtXRNode(handRole).TryGetFeatureValue(CommonUsages.menuButton,out bool mButton);

        if(mButton){
            PauseMenu();
        }
    }

    public void GetData(){
        if(dataFound && data.bestTime > 0f){
            Debug.Log("Update best time");
            int totalSec = (int)data.bestTime;
            int min = totalSec / 60;
            int sec = totalSec % 60;
            bestScore.text = string.Format("{0:00}:{1:00}", min, sec);
        } else{
            scoreText.enabled = false;
            bestScore.enabled = false;
        }

        foreach (Sprite i in settingImages){
            if (i.name == data.userControl){
                controlSetting.GetComponent<Image>().sprite = i;
                if(data.userControl == "Shoulder Opt"){
                    controlSetting.GetComponent<RectTransform>().offsetMax = new Vector2(130,0);
                }
            }
            
            if (data.userArtwork == "1"){
                if(i.name == "Portrait Opt"){
                    artworkSetting.GetComponent<Image>().sprite = i;
                } else if(i.name == data.userDifficulty + "_Portrait"){
                    difficultySetting.GetComponent<Image>().sprite = i;
                }
            } else if(data.userArtwork == "2"){
                if(i.name == "Still Life Opt"){
                    artworkSetting.GetComponent<Image>().sprite = i;
                } else if(i.name == data.userDifficulty + "_Still Life"){
                    difficultySetting.GetComponent<Image>().sprite = i;
                }
            }
        }

        sizeSetting.text = data.userSize;

    }

    public void PauseMenu(){
        if(GameObject.Find("Pause Menu") || GameObject.Find("Pause Menu(Clone)")){
            pauseMenu.SetActive(false);
        } else{
            pauseMenu.SetActive(true);
        }
    }
}
