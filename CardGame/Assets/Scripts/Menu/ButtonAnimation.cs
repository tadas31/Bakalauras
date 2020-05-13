using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    public GameObject options;                  // Gets options window
    public GameObject puzzleLevels;             // Gets puzzle levels window

    public Button classic;                      //gets classic button
    public Button puzzle;                       //gets puzzle button
    public Button play;                         //gets play button
    public GameObject selectGameButtons;        //gets select game buttons object

    private float SPEED = 1100.0f;              //buttons movement speed

    private bool isPlayPressed;                  //if play button pressed value is true else false
    private Vector3 classicTarget;               //target position for classic button
    private Vector3 puzzleTarget;                //target position to puzzle button

    // Start is called before the first frame update
    public void Start()
    {
        isPlayPressed = false;
    }

    // Update is called once per frame
    public void Update()
    {

        //displays classic and puzzle buttons
        if (isPlayPressed)
        {
            float step = SPEED * Time.deltaTime;    //sets movement speed of buttons
            classic.transform.localPosition = Vector3.MoveTowards(classic.transform.localPosition, classicTarget, step);    //moves classic button

            //executes if classic button is in it's destination 
            if (Vector3.Distance(classic.transform.localPosition, classicTarget) < 0.001f)
            {
                puzzle.gameObject.SetActive(true);  //sets puzzle button active
                puzzle.transform.localPosition = Vector3.MoveTowards(puzzle.transform.localPosition, puzzleTarget, step);   //moves puzzle button
            }
        }
        //hides classic and puzzle buttons
        else
        {
            float step = SPEED * Time.deltaTime;    //sets movement speed of buttons
            puzzle.transform.localPosition = Vector3.MoveTowards(puzzle.transform.localPosition, puzzleTarget, step);   //moves puzzle button

            //executes if puzzle button is in it's destination 
            if (Vector3.Distance(puzzle.transform.localPosition, puzzleTarget) < 0.001f)
            {
                puzzle.gameObject.SetActive(false);     //sets puzzle button inactive
                classic.transform.position = Vector3.MoveTowards(classic.transform.position, classicTarget, step);  //moves classic button

                //executes if classic buttonis in it's destination
                if (Vector3.Distance(classic.transform.position, classicTarget) < 0.001f)
                {
                    selectGameButtons.SetActive(false);    //sets select game button inactive
                }
            }
        }
       
    }

    //afet play button is presed sets objects active and sets destination for buttons
    public void onPlayPress()
    {
        options.SetActive(false);
        puzzleLevels.SetActive(false);

        //displays classic and puzzle buttons
        if (!isPlayPressed)
        {
            puzzle.gameObject.SetActive(false);     //sets puzzle button inactive
            selectGameButtons.SetActive(true);      //sets select game buttons active

            //sets pozition of classic button and destination
            classic.transform.position = new Vector3(play.transform.position.x, play.transform.position.y, play.transform.position.z);
            classicTarget = new Vector3(0, classic.transform.localPosition.y, classic.transform.localPosition.z);

            //sets position of puzzle button and destination
            puzzle.transform.localPosition = new Vector3(classicTarget.x, classicTarget.y, classicTarget.z);
            puzzleTarget = new Vector3(classicTarget.x, 53, classicTarget.z);

            isPlayPressed = true;
        }
        //hides classic and puzzle buttons
        else
        {
            //sets destination for puzzle button
            puzzleTarget = new Vector3(classicTarget.x, classicTarget.y, classicTarget.z);

            //sets destination for classic button
            classicTarget = new Vector3(play.transform.position.x, play.transform.position.y, play.transform.position.z);

            isPlayPressed = false; 
        }
        
    }
}
