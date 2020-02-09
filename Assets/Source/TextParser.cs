using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextParser : MonoBehaviour
{
	public Text parsingComponent;
	private Text textComponent;
	private void OnEnable() 
	{
		textComponent = GetComponent<Text>();
		if(parsingComponent!=null)
			textComponent.text = parsingComponent.text;
	}
	private void OnDisable() 
	{
		textComponent = GetComponent<Text>();
		if(parsingComponent!=null)
			textComponent.text = parsingComponent.text;
	}
}
