using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    // Use this for initialization
    public Text buttonText;
    private string originalString;
	void Start () {
        //buttonText = this.GetComponent<Text>();		
        originalString = buttonText.text;
	}
 

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Hit");
        buttonText.text = ">" + buttonText.text + "<";
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.text = originalString;
    }
}
