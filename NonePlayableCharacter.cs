using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonePlayableCharacter : MonoBehaviour
{

    public float displayTime = 4.0f;            //display how long the dialog will be displayed
    public GameObject dialogBox;                //to store the dialogbox
    float timerDisplay;                          //how long to display the dialog    
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerDisplay >= 0){
            timerDisplay -= Time.deltaTime;
            if(timerDisplay < 0)
                dialogBox.SetActive(false);
        }
    }

    public void DisplayDialog(){                //will be called when interact with NPC frog
        timerDisplay = displayTime;             //update the dialog time
        dialogBox.SetActive(true);
    }


}
