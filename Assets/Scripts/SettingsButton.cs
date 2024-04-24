using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject settingsMenu;
    public Slider slider;
    private float slider_EndValue = 1;
    private float incr;
    private float currValue;
    private bool selected;

    void Start(){
        currValue = 0;
        incr = slider_EndValue/5;
        selected = false;
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
        if(selected == true){
            PauseMenu.OpenMenu(settingsMenu);
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
        }
    }

    
}
