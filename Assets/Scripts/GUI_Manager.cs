using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUI_Manager : MonoBehaviour
{
    public bool showOptions;
    public GameObject menu, options;
    public AudioSource mainMusic;
    public Slider volumeSlider, brightnessSlider;
    public Light brightness;
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode crouch;
    public KeyCode interact;
    public KeyCode sprint;
    public KeyCode holdingKey;
    public Text forwardText, backwardText, leftText, rightText, jumpText, crouchText, interactText, sprintText;
    public Toggle fullScreen;
    public Toggle mute;
    public Dropdown resolution;

    // Initialisation
    void Start()
    {
        // audio source safety check
        if (mainMusic != null && volumeSlider != null)
        {
            // load saved volume settings
            if (PlayerPrefs.HasKey("Volume"))
            {
                Load();
            }
            volumeSlider.value = mainMusic.volume;
        }
        // light source safety check
        if (brightness != null && brightnessSlider != null)
        {
            brightnessSlider.value = brightness.intensity;
        }
        //Set our keys to the preset keys we may have change, setting the default key
        #region Key Set Up
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W"));
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
    }
    // Update refreshes multiple times a second
    void Update()
    {
        // debugging
        Debug.Log("UPDATE");
        // audio source check
        if (mainMusic != null && volumeSlider != null)
        {
            // links slider to audio source
            if (volumeSlider.value != mainMusic.volume)
            {
                mainMusic.volume = volumeSlider.value;
                Debug.Log("EDIT AUDIO");
            }
        }
        // light source check
        if (brightness != null && brightnessSlider != null)
        {
            // links slider to light source
            if (brightnessSlider.value != brightness.intensity)
            {
                brightness.intensity = brightnessSlider.value;
                Debug.Log("EDIT LIGHT");
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
                    // set forward to the new key
                    forward = e.keyCode;
                    // set holding key to none
                    holdingKey = KeyCode.None;
                    // set to new key
                    forwardText.text = forward.ToString();
                }
                else
                {
                    // set forward back to what the holding key is
                    forward = holdingKey;
                    // set holding key to none
                    holdingKey = KeyCode.None;
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
                    // set backward to the new key
                    backward = e.keyCode;
                    // set holding key to none
                    holdingKey = KeyCode.None;
                    // set to new key
                    backwardText.text = backward.ToString();
                }
                else
                {
                    // set backward back to what the holding key is
                    backward = holdingKey;
                    // set holding key to none
                    holdingKey = KeyCode.None;
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
                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == right || e.keyCode == jump || e.keyCode == sprint || e.keyCode == crouch || e.keyCode == interact))
                {
                    // set left to the new key
                    left = e.keyCode;
                    // set holding key to none
                    holdingKey = KeyCode.None;
                    // set to new key
                    leftText.text = left.ToString();
                }
                else
                {
                    // set left back to what the holding key is
                    left = holdingKey;
                    // set holding key to none
                    holdingKey = KeyCode.None;
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
                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == jump || e.keyCode == sprint || e.keyCode == crouch || e.keyCode == interact))
                {
                    // set right to the new key
                    right = e.keyCode;
                    // set holding key to none
                    holdingKey = KeyCode.None;
                    // set to new key
                    rightText.text = right.ToString();
                }
                else
                {
                    // set right back to what the holding key is
                    right = holdingKey;
                    // set holding key to none
                    holdingKey = KeyCode.None;
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
                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == jump || e.keyCode == right || e.keyCode == crouch || e.keyCode == interact))
                {
                    // set sprint to the new key
                    sprint = e.keyCode;
                    // set holding key to none
                    holdingKey = KeyCode.None;
                    // set to new key
                    sprintText.text = sprint.ToString();
                }
                else
                {
                    // set sprint back to what the holding key is
                    sprint = holdingKey;
                    // set holding key to none
                    holdingKey = KeyCode.None;
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
                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == jump || e.keyCode == right || e.keyCode == crouch || e.keyCode == sprint))
                {
                    // set interact to the new key
                    interact = e.keyCode;
                    // set holding key to none
                    holdingKey = KeyCode.None;
                    // set to new key
                    interactText.text = interact.ToString();
                }
                else
                {
                    // set interact back to what the holding key is
                    interact = holdingKey;
                    // set holding key to none
                    holdingKey = KeyCode.None;
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
                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == jump || e.keyCode == right || e.keyCode == interact || e.keyCode == sprint))
                {
                    // set crouch to the new key
                    crouch = e.keyCode;
                    // set holding key to none
                    holdingKey = KeyCode.None;
                    // set to new key
                    crouchText.text = crouch.ToString();
                }
                else
                {
                    // set crouch back to what the holding key is
                    crouch = holdingKey;
                    // set holding key to none
                    holdingKey = KeyCode.None;
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
                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == crouch || e.keyCode == right || e.keyCode == interact || e.keyCode == sprint))
                {
                    // set jump to the new key
                    jump = e.keyCode;
                    // set holding key to none
                    holdingKey = KeyCode.None;
                    // set to new key
                    jumpText.text = jump.ToString();
                }
                else
                {
                    // set jump back to what the holding key is
                    jump = holdingKey;
                    // set holding key to none
                    holdingKey = KeyCode.None;
                }
            }
        }
        #endregion
    }
    // Play button function
    public void Play()
    {
        // Load game
        SceneManager.LoadScene(1);
    }
    // Quit button function
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    // Options functions
    public void ShowOptions()
    {
        ToggleOptions();
    }
    // Options switch
    public bool ToggleOptions()
    {
        if (showOptions)
        {
            menu.SetActive(true);
            options.SetActive(false);
            showOptions = false;
            return false;
        }
        else
        {
            menu.SetActive(false);
            options.SetActive(true);
            showOptions = true;
            return true;
        }
    }
    // Save settings
    public void Save()
    {
        PlayerPrefs.SetFloat("Volume", mainMusic.volume);
        PlayerPrefs.SetFloat("Brightness", brightness.intensity);
        PlayerPrefs.SetString("Forward", forward.ToString());
        PlayerPrefs.SetString("Backward", backward.ToString());
        PlayerPrefs.SetString("Left", left.ToString());
        PlayerPrefs.SetString("Right", right.ToString());
        PlayerPrefs.SetString("Jump", jump.ToString());
        PlayerPrefs.SetString("Crouch", crouch.ToString());
        PlayerPrefs.SetString("Sprint", sprint.ToString());
        PlayerPrefs.SetString("Interact", interact.ToString());
    }
    // Load settings
    public void Load()
    {
        mainMusic.volume = PlayerPrefs.GetFloat("Volume");
        brightness.intensity = PlayerPrefs.GetFloat("Brightness");
    }

    public bool FullScreenToggle()
    {
        if (fullScreen.isOn) // if full screen is on
        {
            fullScreen.isOn = false;
            Screen.fullScreen = false;
            return false; // set it to false
        }
        else
        {
            fullScreen.isOn = true;
            Screen.fullScreen = true;
            return true; // set it to true
        }
    }

    public void ScreenToggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public bool MuteToggle()
    {
        if (mute.isOn)
        {
            mute.isOn = false;
            mainMusic.volume = volumeSlider.value;
            return false;
        }
        else
        {
            mute.isOn = true;
            mainMusic.volume = 0;
            return true;
        }
    }

    public void ChangeResolution()
    {
        // Case values correspond to dropdown values, with case 0 corresponding to the first option on the menu
        switch (resolution.value)
        {
            // set first resolution on menu
            case 0:
                Screen.SetResolution(640, 480, fullScreen.isOn);
                break;
            // set second resolution on menu etc.
            case 1:
                Screen.SetResolution(1024, 576, fullScreen.isOn);
                break;
            case 2:
                Screen.SetResolution(1280, 720, fullScreen.isOn);
                break;
            case 3:
                Screen.SetResolution(1600, 900, fullScreen.isOn);
                break;
            case 4:
                Screen.SetResolution(1920, 1080, fullScreen.isOn);
                break;
            case 5:
                Screen.SetResolution(2560, 1440, fullScreen.isOn);
                break;
            case 6:
                Screen.SetResolution(3840, 2160, fullScreen.isOn);
                break;
            case 7:
                Screen.SetResolution(7680, 4800, fullScreen.isOn);
                break;
        }
    }

    #region Controls

    public void Forward()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            //set our holding key to the key of this button
            holdingKey = forward;
            //set this buttin to none allowing us to edit only this button
            forward = KeyCode.None;
            //set the GUI to blank
            forwardText.text = forward.ToString();

        }
    }

    public void Backwards()
    {
        if (!(forward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            //set our holding key to the key of this button
            holdingKey = backward;
            //set this buttin to none allowing us to edit only this button
            backward = KeyCode.None;
            //set the GUI to blank
            backwardText.text = backward.ToString();

        }
    }

    public void Left()
    {
        if (!(backward == KeyCode.None || forward == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            //set our holding key to the key of this button
            holdingKey = left;
            //set this buttin to none allowing us to edit only this button
            left = KeyCode.None;
            //set the GUI to blank
            leftText.text = left.ToString();

        }
    }

    public void Right()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || forward == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            //set our holding key to the key of this button
            holdingKey = right;
            //set this buttin to none allowing us to edit only this button
            right = KeyCode.None;
            //set the GUI to blank
            rightText.text = right.ToString();

        }
    }

    public void Jump()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || forward == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            //set our holding key to the key of this button
            holdingKey = jump;
            //set this buttin to none allowing us to edit only this button
            jump = KeyCode.None;
            //set the GUI to blank
            jumpText.text = jump.ToString();

        }
    }

    public void Crouch()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || forward == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            //set our holding key to the key of this button
            holdingKey = crouch;
            //set this buttin to none allowing us to edit only this button
            crouch = KeyCode.None;
            //set the GUI to blank
            crouchText.text = crouch.ToString();

        }
    }

    public void Sprint()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || forward == KeyCode.None || interact == KeyCode.None))
        {
            //set our holding key to the key of this button
            sprint = forward;
            //set this buttin to none allowing us to edit only this button
            sprint = KeyCode.None;
            //set the GUI to blank
            sprintText.text = sprint.ToString();

        }
    }

    public void Interact()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || forward == KeyCode.None))
        {
            //set our holding key to the key of this button
            holdingKey = interact;
            //set this buttin to none allowing us to edit only this button
            interact = KeyCode.None;
            //set the GUI to blank
            interactText.text = interact.ToString();

        }
    }
    #endregion
}
/*
 HOMEWORK
 - how to set resolutions
 - create a mute button
 - set up a mute toggle that saves


 RESOLUTIONS 16:9
 640*480
 1024*576
 1280*720
 1600*900
 1920*1080
 2560*1440
 3840*2160
 7680*4800
     */
