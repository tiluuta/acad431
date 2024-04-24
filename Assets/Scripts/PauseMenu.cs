using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Button pauseButt;
    public GameObject pauseMenu;
        public GameObject parentCanvas;

	void Start () {
        parentCanvas = GameObject.Find("UI");

		Button btn = pauseButt.GetComponent<Button>();
		btn.onClick.AddListener(PauseGame);
	}

	public void PauseGame(){
        if(!GameObject.Find("Pause Menu(Clone)") && !GameObject.Find("Pause Menu")){
            Instantiate(pauseMenu,parentCanvas.transform);
            pauseMenu.SetActive(true);
        }
            
	}

    public static void OpenMenu(GameObject menuPrefab){        
        if(GameObject.Find("Settings Menu(Clone)")){
            Destroy(GameObject.Find("Settings Menu"));
            //GameObject.Find("Settings Menu").SetActive(false);
        } else{
            Instantiate(menuPrefab,GameObject.Find("Settings").transform).SetActive(true);
        }
        
    }

    public static void CloseMenu(GameObject menu){
        Destroy(menu);
        SceneManager.LoadScene("XRMenu");
    }

    
}
