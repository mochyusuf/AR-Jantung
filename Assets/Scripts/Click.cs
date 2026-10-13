using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenAdmin()
	{
		Application.OpenURL("https://server-ar-jantung.000webhostapp.com/");
	}
}
