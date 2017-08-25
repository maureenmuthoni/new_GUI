using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
	[Header("Keys")]
	public KeyCode forward;
	public KeyCode backward;
	public KeyCode left;
	public KeyCode right;
	public KeyCode jump;
	public KeyCode crouch;
	public KeyCode interact;
	public KeyCode sprint;
	public KeyCode holdingKey;


	[Header("Resolutions")]
	public int index;
	public int[] resX, resY;



	[Header("References")]
	public GameObject menu, options, pauseMenu, pauseOptions; // To allow public access to these GameObjects ("menu", "options", "pauseMenu", "pauseOptions") in Unity and C#
	public AudioSource mainMusic; // Allow public access to the AudioSource which has been named "mainMusic"
	public float volumeSlider, brightnessSlider; // Allow public access to the Sliders, named "volumeSlider" and "brightnessSlider"
	public sliderbrightnessSlider; // Allow public access to Light, named "brightness"

	void Start()
	{

	scrW = Screen.width / 16;
	scrH = Screen.height/ 9;
	}
	
	void OnGUI()
	{
		if(showOptions) // if we are on menu
		if(scrw != ScreenWidth/16)
		scrW = Screen.width / 16;
		scrH = Screen.height/ 9;
	}
		

				GUI.Box (new Rect (0, 0, Screen.width, Screen.height), ""); // background box
			int i = 0
			GUI.Box (new Rect(4*scrW,0.25f*scrH,8*scrw,2*scrH), "I Love Programming" //Title
				// Buttons

				if (GUI.Button(new Rect(6*scrW,4*scrH,4*scrw,scrH),"Play"))
				{
				SceneManager.LoadScene(1);
				}
			if(GUI.Button(new Rect(6*scrW,5*scrH,4*scrw,scrH),"Options"))
					{
						showOptions = true;
					}
					

				if (GUI.Button(new Rect(6*scrW,6*scrH,4*scrw,scrH),"Exit"))
						{
							Application.Quit();
				}
				scrW = Screen.width /16;
				scrH = Screen.height /19;
				GUI.Box (new Rect (0, 0, Screen.width, Screen.height), ""); // background box		
				int i = 0

				if (GUI.Button(new Rect(6*scrW,5*scrH,4*scrw,scrH),"Options"))

				}