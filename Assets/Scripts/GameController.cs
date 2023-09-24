using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour
{
    public Text timeField;
   // [SerializeField] private Text timeField;
    public Text wordToFindField;
    public Text messageField;
    public GameObject[] hangman;   //array of game objects to store the hangman related game objects.
    public GameObject winText;  //to control the win screen
    public GameObject loseText;
    public GameObject replayButton;
    

    private float time;  //in C# when the value is empty it's assigned 0 by default.
    //private string[] wordsLocal= { "NEYMAR JR","RONALDO JR"};
    private string[] words = File.ReadAllLines(@"Assets/Texts/Words.txt");  //loading words from file
    private string[] hints = File.ReadAllLines(@"Assets/Texts/Hints.txt");
    private string hint;
    //private int[] myNums = { 1, 2, 3, 4 };
    private string chosenWord;
    private string hiddenWord;
    private int countChance = 0;// we have 5 chances(6 will end the game with losing screen), as our hangman has 6 body elements.
    private bool gameEnd = false;
    private int questionNo;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("fist value : " + wordsLocal[0]);
        //Debug.Log("Random value : " + wordsLocal[Random.Range(0,wordsLocal.Length)]);
        questionNo = Random.Range(0, words.Length);  //random = 5 
        chosenWord = words[questionNo].ToUpper();    //word = 5th line in file
        hint = hints[questionNo].ToUpper();
        Debug.Log(chosenWord);
        Debug.Log(questionNo);

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
        if (gameEnd == false)   //stop the timer once the game is over.
        {
            //deltaTime returns the interval in seconds from the last frame to the current one.
            time += Time.deltaTime;   
            timeField.text = ((int)time).ToString();  //just displaying the time in seconds(ignoring ms)
            //Debug.Log(Time.deltaTime); approx 0.02s, i.e. 1f in 0.02s which means it's 50fps
        }
        messageField.text = hint;

    }


    //Will be called every single time when a GUI event happens(i.e. keystrokes)
    private void OnGUI()
    {
        Event e = Event.current;   //returns the current event being processed.

   


      
        if (e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1 && gameEnd==false) //e.keyCode.ToString().Length==1 to prevent the "None" 
        {
            //Debug.Log(e.type);
            //Debug.Log(e.keyCode); //on one key stroke eg: 'Q' we get 'Q' followed by 'None'.
            string keyPressed = e.keyCode.ToString(); 
            messageField.text = "";

            if (!hiddenWord.Contains(keyPressed) && !chosenWord.Contains(keyPressed))
            {
                //Debug.Log("WRONG LETTER");
                hangman[countChance].SetActive(true);
                countChance += 1;
                if (countChance == hangman.Length)  //if it's equal to 6 then show lose screen
                {
                    //Debug.Log("YOU LOSE!");
                    loseText.SetActive(true);

                    //play lose audio
                    AudioSource a = loseText.GetComponent<AudioSource>(); //get the audio source component from the game object
                    a.Play();  //the game object loseText in this case will have to be in active state for this to work

                    replayButton.SetActive(true);   //show the replay button to reload the scene
                    gameEnd = true;
                }
            }
            else if(hiddenWord.Contains(keyPressed))
            {
                //Debug.Log("ALREADY ENTERED LETTER");
                messageField.text = "Character already entered!";
            }
            else  //the entered input is a letter in the chosenWord
            {
                int index = chosenWord.IndexOf(keyPressed);
                while (index > -1)  //indexOf gives the first found index of the string, if not found then -1
                {
                    hiddenWord = hiddenWord.Substring(0, index) + keyPressed + hiddenWord.Substring(index + 1);
                    chosenWord= chosenWord.Substring(0, index) + '_' + chosenWord.Substring(index + 1);
                    index = chosenWord.IndexOf(keyPressed);
                }
                wordToFindField.text = hiddenWord;

                if (!hiddenWord.Contains("_"))// if there's no more underscores in hiddenWord
                {
                    //Debug.Log("YOU WIN!");
                    winText.SetActive(true);

                    //Playing win audio
                    AudioSource a = winText.GetComponent<AudioSource>(); //get the audio source component from the game object
                    a.Play();  //the game object loseText in this case will have to be in active state for this to work

                    replayButton.SetActive(true);   //show the replay button to reload the scene
                    gameEnd = true;
                }
            }   

        }
    }
}
