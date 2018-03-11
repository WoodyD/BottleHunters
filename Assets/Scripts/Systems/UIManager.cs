using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Text leftCornerText;
	
	public void SetLeftCornerText(string text){
		leftCornerText.text = text;
	}
}
