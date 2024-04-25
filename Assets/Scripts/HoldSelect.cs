using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HoldSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Slider slider;
    public float slider_EndValue = 1;
    public float currValue = 0;
    private float incr;
    public int secToSelect = 3;
    public bool selected;

    void Start(){
        selected = false;

        incr = slider_EndValue/secToSelect;
    }

    IEnumerator time(){
    while (true)
    {
        if(currValue < slider_EndValue){
            currValue += incr;
            slider.value = currValue;
        }

        yield return new WaitForSeconds(1);
    }
}
    void Update()
    {
        if(slider.value == slider_EndValue){
            selected = true;
        }
        
    }

    public void OnPointerEnter (PointerEventData eventData) 
	{
        currValue = 0;
        StartCoroutine(time());
	}

    public void OnPointerExit (PointerEventData eventData) 
	{
        if(selected == false){
            StopAllCoroutines();
            //reset slider
            currValue = 0;
            slider.value = 0;
        } else if(selected == true){
            if (this.gameObject.tag == "Control" || this.gameObject.tag == "Difficulty" || this.gameObject.tag == "Artwork" || this.gameObject.tag == "Size"){
                Settings.NewSelected(this.gameObject);    //saves the selected game object as user's control
            } else if (this.gameObject.tag == "Confirm" || this.gameObject.tag == "Done"){
                Settings.ConfirmSelect();
            } else if (this.gameObject.tag == "Play"){
                Debug.Log("Starting new game");
                SceneManager.LoadScene("Main Scene");
            } else if (this.gameObject.tag == "Unpause"){
                /*GameObject pauseMenu = GameObject.Find("Pause Menu");
                pauseMenu.SetActive(false);*/
                if(GameObject.Find("Pause Menu(Clone)")){
                    Destroy(GameObject.Find("Pause Menu(Clone)"));
                } else{
                    Destroy(GameObject.Find("Pause Menu"));
                }
            }
        }
	}


}
