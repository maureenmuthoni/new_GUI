using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerClockHardCode : MonoBehaviour
{
    public float timer; // Set this to the time you want in seconds + 1 second for the pc load start

    // Use this for initiallization
    void Start()
    {
        Time.timeScale = 1;

    }
   // This is called once per frame
 void Update()
    {
        if (timer > 0) // if we are greater than 0
        {
            timer -= Time.deltaTime; // count down.... this may take us below 0
        }
    }

void LateUpdate()
{
    if(timer < 0)
    {
        timer = 0; // Sets us back to 0
    }
}     
void OnGUI()
{
  //Screen by aspect ratio 16.9 
    float scrW = Screen.width / 16;
    float scrH = Screen.height / 9;

    int mins = Mathf.FloorToInt(timer / 60);
 int secs = Mathf.FloorToInt(timer - mins * 60);

    string clockTime = string.Format("{0:0}:{1:00}", mins, secs);
        GUI.Box(new Rect(scrW, scrH, scrW, scrH), clockTime); // dISPLAY CLOCK
}
	
 
}
