using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // this allows us to reference Unity sliders, buttons, etc
using UnityEngine.SceneManagement;

public class GUI_Manager : MonoBehaviour
{
	// Main Camera settings > Projection > Orthographic
	// Canvas > UI Scale Mode > Scale With Screen Size
	// Canvas > Match > 0.5

	[Header("Bools")] // organising Headings for these in Unity
	public bool showOptions, showPauseOptions; // Allow access to Bool Function (Yes/No) "showOptions", defined further below.
	// public bool fullscreen; // Set bool for Fullscreen default. <= for Jayderaders
	public bool fullScreenToggle;

	[Header("Resolutions")]
	public int index;
	public int[] resX, resY;
	public Dropdown resolutionDropdown; // Dropdown has a value variable that you can use to reference arrays;


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

	[Header("References")]
	public GameObject menu, options, pauseMenu, pauseOptions; // To allow public access to these GameObjects ("menu", "options", "pauseMenu", "pauseOptions") in Unity and C#
	public AudioSource mainMusic; // Allow public access to the AudioSource which has been named "mainMusic"
	public Slider volumeSlider, brightnessSlider; // Allow public access to the Sliders, named "volumeSlider" and "brightnessSlider"
	public Light brightness; // Allow public access to Light, named "brightness"
	public Text forwardText, backwardText, leftText, rightText, jumpText, crouchText, interactText, sprintText;
	public Toggle fullScreen;

	// public Dropdown resolution; // referencing Dropdown component "resolution"



	public void Start()
	{
		// Switch to 640 x 480, fullscreen (if True) or windowed (if False)
		// Screen.SetResolution(640, 360, true);
		// Screen.SetResolution(1024, 576, false);
		// Screen.SetResolution(1280, 720, false);
		// Screen.SetResolution(1600, 900, false);
		// Screen.SetResolution(1920, 1080, false);
		// Screen.SetResolution(2560, 1440, false);
		// Screen.SetResolution(3840, 2160, false);
		// Screen.SetResolution(7680, 4800, false);

		// Camera Projection: Orthographic
		// Canvas Scaler/UI Scale Mode: Scale with Screen Size
		// Canvas Scaler/Match: 0.5

		if (mainMusic != null && volumeSlider != null) // on Startup, if mainMusic and volumeSlider are BOTH NOT NULL, then:
		{
			if(PlayerPrefs.HasKey("Volume")) // Load any previously saved Volume settings, defined further below.
			{
				Load();
			}
			volumeSlider.value = mainMusic.volume; // at the Start, make Volume Slider and Music Volume the same!
		}
		if(brightness != null && brightnessSlider != null) // similar to above.
		{
			if (PlayerPrefs.HasKey("Brightness")) // 
			{
				Load();
			}
			brightnessSlider.value = brightness.intensity; // Make slider = actual intensity
		}

		// Set Resolutions at Start <= Jayderaders
		/* fullscreen = true;
        Screen.SetResolution(640, 360, false);
        Screen.SetResolution(1024, 576, false);
        Screen.SetResolution(1280, 720, false);
        Screen.SetResolution(1600, 1000, fullscreen);
        Screen.SetResolution(1920, 1080, false);
        Screen.SetResolution(2560, 1440, false);
        Screen.SetResolution(3840, 2160, false);
        Screen.SetResolution(7680, 4800, false); */

		#region Key Set Up
		forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W")); // saves default Forward key to W
		backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S"));
		left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
		right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
		jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space"));
		crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "LeftControl"));
		sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift"));
		interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E"));

		forwardText.text = forward.ToString();
		backwardText.text = backward.ToString();
		leftText.text = left.ToString();
		rightText.text = right.ToString();
		jumpText.text = jump.ToString();
		crouchText.text = crouch.ToString();
		sprintText.text = sprint.ToString();
		interactText.text = interact.ToString();
		#endregion

		//Load(); // could just replace the mainMusic and brightnessSlider checks above?

		#region Resolution
		index = PlayerPrefs.GetInt("Res",3); // Default is Element 3 in Res X,Y resolutions
		int fullWindow = PlayerPrefs.GetInt("FullWindow", 1);

		if(fullWindow == 0)
		{
			fullScreen.isOn = false; // the Bool up top
			fullScreenToggle = false; // the Toggle up top
		}
		else if(fullWindow == 1)
		{
			fullScreen.isOn = true; // the Bool up top
			fullScreenToggle = true; // the Toggle up top
		}

		resolutionDropdown.value = index;
		Screen.SetResolution(resX[index], resY[index], fullScreenToggle);
		Screen.fullScreen = fullScreenToggle;

		#endregion
	}

	public void Update()
	{
		Debug.Log("UPDATE");
		if (volumeSlider.value != mainMusic.volume) // only running if Volume Slider and Music Volume are NOT the same
		{
			mainMusic.volume = volumeSlider.value; // makes Music Volume and Volume Slider the same!
			Debug.Log("EDIT AUDIO");
		}

		if (brightness != null && brightnessSlider != null) // similar to above, 
		{
			if (brightnessSlider.value != brightness.intensity)
			{
				brightness.intensity = brightnessSlider.value;
				Debug.Log("LIGHT");
			}
		}

	}

	void OnGUI()
	{
		#region Set New Key or Set Key Back

		Event e = Event.current;
		if (forward == KeyCode.None)
		{
			// if an event is triggered by a key press
			if (e.isKey)
			{
				Debug.Log("Key Code: " + e.keyCode);

				// if this key is not the same as the other keys
				if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == sprint || e.keyCode == crouch || e.keyCode == interact))
				{
					// Set forward to the new key
					forward = e.keyCode;

					// set Holding Key to None
					holdingKey = KeyCode.None;

					// Set to new key
					forwardText.text = forward.ToString();
				}
				else
				{
					// set Forward back to what the Holding Key is
					forward = holdingKey;

					// set Holding Key to None
					holdingKey = KeyCode.None;

					// set back to last key
					forwardText.text = forward.ToString();
				}
			}
		}

		if (backward == KeyCode.None)
		{
			// if an event is triggered by a key press
			if (e.isKey)
			{
				Debug.Log("Key Code: " + e.keyCode);

				// if this key is not the same as the other keys
				if (!(e.keyCode == forward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == sprint || e.keyCode == crouch || e.keyCode == interact))
				{
					// Set backward to the new key
					backward = e.keyCode;

					// set Holding Key to None
					holdingKey = KeyCode.None;

					// Set to new key
					backwardText.text = backward.ToString();
				}
				else
				{
					// set backward back to what the Holding Key is
					backward = holdingKey;

					// set Holding Key to None
					holdingKey = KeyCode.None;

					// set back to last key
					backwardText.text = backward.ToString();
				}
			}
		}

		if (left == KeyCode.None)
		{
			// if an event is triggered by a key press
			if (e.isKey)
			{
				Debug.Log("Key Code: " + e.keyCode);

				// if this key is not the same as the other keys
				if (!(e.keyCode == backward || e.keyCode == forward || e.keyCode == right || e.keyCode == jump || e.keyCode == sprint || e.keyCode == crouch || e.keyCode == interact))
				{
					left = e.keyCode;
					holdingKey = KeyCode.None;
					leftText.text = left.ToString();
				}
				else
				{
					left = holdingKey;
					holdingKey = KeyCode.None;
					leftText.text = left.ToString();
				}
			}
		}

		if (right == KeyCode.None)
		{
			// if an event is triggered by a key press
			if (e.isKey)
			{
				Debug.Log("Key Code: " + e.keyCode);

				// if this key is not the same as the other keys
				if (!(e.keyCode == backward || e.keyCode == forward || e.keyCode == left || e.keyCode == jump || e.keyCode == sprint || e.keyCode == crouch || e.keyCode == interact))
				{
					right = e.keyCode;
					holdingKey = KeyCode.None;
					rightText.text = right.ToString();
				}
				else
				{
					right = holdingKey;
					holdingKey = KeyCode.None;
					rightText.text = right.ToString();
				}
			}
		}

		if (jump == KeyCode.None)
		{
			// if an event is triggered by a key press
			if (e.isKey)
			{
				Debug.Log("Key Code: " + e.keyCode);

				// if this key is not the same as the other keys
				if (!(e.keyCode == backward || e.keyCode == forward || e.keyCode == right || e.keyCode == left || e.keyCode == sprint || e.keyCode == crouch || e.keyCode == interact))
				{
					jump = e.keyCode;
					holdingKey = KeyCode.None;
					jumpText.text = jump.ToString();
				}
				else
				{
					jump = holdingKey;
					holdingKey = KeyCode.None;
					jumpText.text = jump.ToString();
				}
			}
		}

		if (sprint == KeyCode.None)
		{
			// if an event is triggered by a key press
			if (e.isKey)
			{
				Debug.Log("Key Code: " + e.keyCode);

				// if this key is not the same as the other keys
				if (!(e.keyCode == backward || e.keyCode == forward || e.keyCode == right || e.keyCode == jump || e.keyCode == left || e.keyCode == crouch || e.keyCode == interact))
				{
					sprint = e.keyCode;
					holdingKey = KeyCode.None;
					sprintText.text = sprint.ToString();
				}
				else
				{
					sprint = holdingKey;
					holdingKey = KeyCode.None;
					sprintText.text = sprint.ToString();
				}
			}
		}

		if (crouch == KeyCode.None)
		{
			// if an event is triggered by a key press
			if (e.isKey)
			{
				Debug.Log("Key Code: " + e.keyCode);

				// if this key is not the same as the other keys
				if (!(e.keyCode == backward || e.keyCode == forward || e.keyCode == right || e.keyCode == jump || e.keyCode == sprint || e.keyCode == left || e.keyCode == interact))
				{
					crouch = e.keyCode;
					holdingKey = KeyCode.None;
					crouchText.text = crouch.ToString();
				}
				else
				{
					crouch = holdingKey;
					holdingKey = KeyCode.None;
					crouchText.text = crouch.ToString();
				}
			}
		}

		if (interact == KeyCode.None)
		{
			// if an event is triggered by a key press
			if (e.isKey)
			{
				Debug.Log("Key Code: " + e.keyCode);

				// if this key is not the same as the other keys
				if (!(e.keyCode == backward || e.keyCode == forward || e.keyCode == right || e.keyCode == jump || e.keyCode == sprint || e.keyCode == crouch || e.keyCode == left))
				{
					interact = e.keyCode;
					holdingKey = KeyCode.None;
					interactText.text = interact.ToString();
				}
				else
				{
					interact = holdingKey;
					holdingKey = KeyCode.None;
					interactText.text = interact.ToString();
				}
			}
		}

		#endregion
	}







	public void Play() // Function "Play", loads Scene 1 from SceneManager
	{
		SceneManager.LoadScene(1);
	}

	public void Exit() // Function "Exit" quits Application (does not work in Editor, only when Game is Built)
	{
		Application.Quit();
		Debug.Log("Quit");
	}

	public void ShowOptions() // VOID does not need something to be returned. Function ShowOptions runs bool ToggleOptions, defined further below.
	{
		ToggleOptions();
	}

	public bool ToggleOptions() // BOOL needs something to be returned. Check if ShowOptions>ToggleOptions is activated (true/false), then check and execute the following:
	{
		if (showOptions) // If showOptions is True (we're asking: is the Options screen open?), then Activate GameObject "menu", Deactivate GameObject "options", and set showOptions to False.
		{
			menu.SetActive(true); // Bring up "menu" screen
			options.SetActive(false); // Close "options" screen
			showOptions = false; // showOptions set to False.
			return false; //
		}
		else // If showOptions is False (we're asking: is the Menu screen open?), then deactivate GameObject "menu", activate GameObject "options", and set showOptions to True.
		{
			menu.SetActive(false); // Close "menu" screen
			options.SetActive(true); // Bring up "options" screen
			showOptions = true; // set showOptions to True.
			return true; // 
		}
	}

	public void ShowPauseOptions() // VOID does not need something to be returned. Function ShowPauseOptions runs bool TogglePauseOptions, defined further below.
	{
		TogglePauseOptions();
	}

	public bool TogglePauseOptions() // BOOL needs something to be returned. Check if ShowPauseOptions>TogglePauseOptions is activated (true/false), then check and execute the following:
	{
		if (showPauseOptions) // If showPauseOptions is True (we're asking: is the Options screen open?), then Activate GameObject "pauseMenu", Deactivate GameObject "pauseOptions", and set showPauseOptions to False.
		{
			pauseMenu.SetActive(true); // Bring up "pauseMenu" screen
			pauseOptions.SetActive(false); // Close "pauseOptions" screen
			showPauseOptions = false; // showPauseOptions set to False.
			return false; //
		}
		else // If showPauseOptions is False (we're asking: is the Menu screen open?), then deactivate GameObject "pauseMenu", activate GameObject "pauseOptions", and set showPauseOptions to True.
		{
			pauseMenu.SetActive(false); // Close "pauseMenu" screen
			pauseOptions.SetActive(true); // Bring up "pauseOptions" screen
			showPauseOptions = true; // set showPauseOptions to True.
			return true; // 
		}
	}

	public void Save() // saves in Registry: HKEY_CURRENT_USER/Software/Unity/UnityEditor/DefaultCompany
	{
		PlayerPrefs.SetFloat("Volume",mainMusic.volume);
		PlayerPrefs.SetFloat("Brightness", brightness.intensity);
		// how to save chosen Resolution setting?
		// how to save Mute toggle choice?

		PlayerPrefs.SetString("Forward", forward.ToString());
		PlayerPrefs.SetString("Backward", backward.ToString());
		PlayerPrefs.SetString("Left", left.ToString());
		PlayerPrefs.SetString("Right", right.ToString());
		PlayerPrefs.SetString("Jump", jump.ToString());
		PlayerPrefs.SetString("Crouch", crouch.ToString());
		PlayerPrefs.SetString("Interact", interact.ToString());
		PlayerPrefs.SetString("Sprint", sprint.ToString());

		// Resolution
		PlayerPrefs.SetInt("Res",index);

		if (fullScreenToggle)
		{
			PlayerPrefs.SetInt("FullWindow", 1);
		}
		else
		{
			PlayerPrefs.SetInt("FullWindow", 0);
		}
	}

	public void Load() // loads from above location
	{
		mainMusic.volume = PlayerPrefs.GetFloat("Volume");
		brightness.intensity = PlayerPrefs.GetFloat("Brightness");

		forwardText.text = PlayerPrefs.GetString("Forward");
		backwardText.text = PlayerPrefs.GetString("Backward");
		leftText.text = PlayerPrefs.GetString("Left");
		rightText.text = PlayerPrefs.GetString("Right");
		jumpText.text = PlayerPrefs.GetString("Jump");
		crouchText.text = PlayerPrefs.GetString("Crouch");
		sprintText.text = PlayerPrefs.GetString("Sprint");
		interactText.text = PlayerPrefs.GetString("Interact");
	}

	/*
    
    RESOLUTIONS
     640*360 (Not *480!)
     1024*576
     1280*720
     1600*900
     1920*1080
     2560*1440
     3840*2160
     7680*4800

     */

	#region Controls
	public void Forward()
	{
		// FORWARD
		if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
		{
			// set out holding key to the key of this button
			holdingKey = forward;

			// set this button to none allowing us to edit only this button
			forward = KeyCode.None;

			//Set the GUI to blank
			forwardText.text = forward.ToString();
		}
	}


	public void Backward()
	{

		if (!(forward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
		{
			// set out holding key to the key of this button
			holdingKey = backward;

			// set this button to none allowing us to edit only this button
			backward = KeyCode.None;

			backwardText.text = backward.ToString();
		}
	}

	public void Left()
	{
		if (!(backward == KeyCode.None || forward == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
		{
			// set out holding key to the key of this button
			holdingKey = left;

			// set this button to none allowing us to edit only this button
			left = KeyCode.None;

			leftText.text = left.ToString();
		}
	}

	public void Right()
	{ 
		if (!(backward == KeyCode.None || left == KeyCode.None || forward == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
		{
			// set out holding key to the key of this button
			holdingKey = right;

			// set this button to none allowing us to edit only this button
			right = KeyCode.None;

			rightText.text = right.ToString();
		}

	}

	public void Jump()
	{

		if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || forward == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
		{
			// set out holding key to the key of this button
			holdingKey = jump;

			// set this button to none allowing us to edit only this button
			jump = KeyCode.None;

			jumpText.text = jump.ToString();
		}
	}

	public void Crouch()
	{

		if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || forward == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
		{
			// set out holding key to the key of this button
			holdingKey = crouch;

			// set this button to none allowing us to edit only this button
			crouch = KeyCode.None;

			crouchText.text = crouch.ToString();
		}
	}

	public void Sprint()
	{

		if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || forward == KeyCode.None || interact == KeyCode.None))
		{
			// set out holding key to the key of this button
			holdingKey = sprint;

			// set this button to none allowing us to edit only this button
			sprint = KeyCode.None;

			sprintText.text = sprint.ToString();
		}
	}

	public void Interact()
	{

		if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || forward == KeyCode.None))
		{
			// set out holding key to the key of this button
			holdingKey = interact;

			// set this button to none allowing us to edit only this button
			interact = KeyCode.None;

			interactText.text = interact.ToString();
		}
	}

	#endregion

	/*
    public bool FullScreenToggle()
    {
        if (fullScreen.isOn) // if Fullscreen is on
        {
            fullScreen.isOn = false;
            Screen.fullScreen = false;
            return false; // set it to False
        }
        else
        {
            fullScreen.isOn = true;
            Screen.fullScreen = true;
            return true; // set it to True
        }
    } */

	// ScreenToggle to maintain Fullscreen/Windowed choice with Resolution selection as well
	public void ScreenToggle()
	{
		Screen.fullScreen = !Screen.fullScreen;
		fullScreenToggle = !fullScreenToggle; // ensure ScreenToggle works with fullscreen bool;
	}

	public void MuteToggle()
	{
		mainMusic.mute = !mainMusic.mute;

	}

	// creating resDrop function <= for Jayderaders
	/*public void resDrop()
    {
        switch (resolution.captionText.text) // using Switch to reference Option names in Dropdown component
        {
            case "640 x 360":
                Screen.SetResolution(640, 360, fullscreen); // if Case Name matches Option Name, do Screen.SetResolution command.
                break;
            case "1024 x 576":
                Screen.SetResolution(1024, 576, fullscreen);
                break;
            case "1280 x 720":
                Screen.SetResolution(1280, 720, fullscreen);
                break;
            case "1600 x 900":
                Screen.SetResolution(1600, 1000, fullscreen);
                break;
            case "1920 x 1080":
                Screen.SetResolution(1920, 1080, fullscreen);
                break;
            case "2560 x 1440":
                Screen.SetResolution(2560, 1440, fullscreen);
                break;
            case "3840 x 2160":
                Screen.SetResolution(3840, 2160, fullscreen);
                break;
            case "7680 x 4800":
                Screen.SetResolution(7680, 4800, fullscreen);
                break;

            default: // Sets Default resolution before choices are made
                Screen.SetResolution(1600, 1000, fullscreen);
                break;
            
        }
        
        // 

        // Screen.SetResolution(640, 360, true);
        // Screen.SetResolution(1024, 576, false);
        // Screen.SetResolution(1280, 720, false);
        // Screen.SetResolution(1600, 900, false);
        // Screen.SetResolution(1920, 1080, false);
        // Screen.SetResolution(2560, 1440, false);
        // Screen.SetResolution(3840, 2160, false);
        // Screen.SetResolution(7680, 4800, false);
    } */

	// Screen.SetResolution(x,y,bool);

	public void ResolutionChange()
	{
		index = resolutionDropdown.value;
		Screen.SetResolution(resX[index], resY[index], fullScreenToggle);
	}

}
