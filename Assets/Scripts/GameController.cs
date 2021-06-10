using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text timeField;
    public Text wordToFindField;


    private float time;  //in C# when the value is empty it's assigned 0 by default.
    private string[] wordsLocal= { "NEYMAR JR","RONALDO JR"};
    //private int[] myNums = { 1, 2, 3, 4 };
    private string chosenWord;
    private string hiddenWord;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("fist value : " + wordsLocal[0]);
        //Debug.Log("Random value : " + wordsLocal[Random.Range(0,wordsLocal.Length)]);
        chosenWord = wordsLocal[Random.Range(0, wordsLocal.Length)];

        for(int i = 0; i < chosenWord.Length; i++)
        {
            char letter = chosenWord[i];
            
            //if(letter==' ')
            if(char.IsWhiteSpace(letter))
            {
                //hiddenWord += ' ';
                hiddenWord += " ";  //string because two whitespace is added
                continue;  //skip the rest of the statements a cause the next iteration to take place.
            }
            hiddenWord += "_";
        }
        //hiddenWord = hiddenWord.Substring(0,hiddenWord.Length-1);  //remove the last char cuz it's space

        
        wordToFindField.text = hiddenWord;
        //Debug.Log(hiddenWord);


    }

    // Update is called once per frame
    void Update()
    {
        //deltaTime returns the interval in seconds from the last frame to the current one.
        time += Time.deltaTime;   //i.e if we have 1fps deltaTime=1; 10fps deltaTime=0.1
        timeField.text = ((int)time).ToString();  //just displaying the time in seconds(ignoring ms)
        //Debug.Log(Time.deltaTime); approx 0.02s, i.e. 1f in 0.02s which means it's 50fps

    }


    //Will be called every single time when a GUI event happens(i.e. keystrokes)
    private void OnGUI()
    {
        Event e = Event.current;   //returns the current event being processed.

        //Method-1
        /*
        if(e.type == EventType.KeyDown && e.keyCode.ToString().Length==1) //e.keyCode.ToString().Length==1 to prevent the "None" 
        {
            //Debug.Log(e.type);
            //Debug.Log(e.keyCode); //on one key stroke eg: 'Q' we get 'Q' followed by 'None'.
            string keyPressed = e.keyCode.ToString();
            string result = "";
            if(chosenWord.Contains(keyPressed))
            {
                if (hiddenWord.Contains(keyPressed))
                {
                    Debug.Log("ALREADY ENTERED!");
                }
                else
                {
                    for(int i = 0; i < chosenWord.Length; i++)
                    {
                        if (chosenWord.ToUpper()[i].ToString() == keyPressed)
                        {
                            result+=keyPressed;
                        }
                        else
                        {
                            result += hiddenWord[i];
                        }
                    }
                    hiddenWord = result;
                    wordToFindField.text = hiddenWord;
                }
            }
            else
            {
                Debug.Log("NOPE!");
            }
        }
        */

        if (e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1) //e.keyCode.ToString().Length==1 to prevent the "None" 
        {
            //Debug.Log(e.type);
            //Debug.Log(e.keyCode); //on one key stroke eg: 'Q' we get 'Q' followed by 'None'.
            string keyPressed = e.keyCode.ToString();
            string result = "";
            while (chosenWord.IndexOf(keyPressed) > -1)  //indexOf gives the first found index of the string, if not found then -1
            {

            }
        }
    }
}
