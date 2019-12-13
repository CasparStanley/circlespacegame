using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public class ScoreController : MonoBehaviour
{
    [SerializeField] CameraZoomController camZoomScript;

    // Score in-game stuff
    [Header("IN-GAME SCORE STUFF")]
    public TMP_Text scoreString;
    private int currentScore;
    //private int maxScore = 1000;
    private int bgUpdateCounter = 0;

    [SerializeField] private int[] bgUpdates;
    [SerializeField] private Color[] bgColors;
    private Color lerpedColor;
    private Color prevColor;
    [SerializeField] private Camera mainCam;

    // High Score Stuff
    [Header("HIGH SCORE STUFF")]
    [SerializeField] private int highScore;
    [SerializeField] private TMP_Text highScoreTextPrefab;
    [SerializeField] private GameObject highScoresParent;
    public static List<int> highScoresList = new List<int>();
    //[SerializeField] private List<GameObject> highScoresObjects = new List<GameObject>(); 

    private bool changeBg;

    private void Start()
    {
        LoadHighScores();

        changeBg = false;
        lerpedColor = mainCam.backgroundColor;
        prevColor = lerpedColor;
    }

    // LOAD HS FROM FILE
    private void LoadHighScores()
    {
        if (File.Exists(Application.persistentDataPath + "/savedHighScores.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedHighScores.gd", FileMode.Open);
            highScoresList = (List<int>)bf.Deserialize(file);
            file.Close();

            //highScoresList.Reverse();

            foreach (int hs in highScoresList)
            {
                TMP_Text newHighScoreText = Instantiate(highScoreTextPrefab, transform.position, transform.rotation); // Instantiate the new high score as a text
                newHighScoreText.transform.SetParent(highScoresParent.transform); // Set the new high score text as a child of the High Scores so it will position itself correctly
                newHighScoreText.text = hs.ToString(); // Set the new text to the most recent high score

                highScore = Mathf.Max(highScoresList.ToArray());
            }
        }

        /*
        if (highScoresList.Count > maxHighscoresInList) // We want to only show 15 high scores
        {
            int smallestHS = highScoresList.Min(); // Find the smallest value in the List
            highScoresList.Remove(highScoresList.IndexOf(smallestHS)); // Delete it!



            // DEBUGGING
            string hsString;
            foreach (int hs in highScoresList)
            {
                hsString = hs.ToString();
                Debug.Log(hsString + '\n');
            }

            Debug.Log("Bottom HS Deleted: " + smallestHS);
        }
        */
    }

    // SAVE HS TO FILE
    private void SaveHighScores()
    {
        //highScoresList.Add(highScore);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedHighScores.gd");
        bf.Serialize(file, highScoresList);
        file.Close();
    }

    // UPDATE SCORE IN-GAME
    public void ScoreUpdate(int points)
    {
        currentScore += points * 10;
        scoreString.text = currentScore.ToString();

        BackgroundUpdate();
    }

    public void UpdateHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            highScoresList.Sort();
            highScoresList.Add(highScore); // Add the new high score int to the list of high scores (INTS - THIS IS WHAT IS SAVED LOCALLY)
            highScoresList.Reverse();

            TMP_Text newHighScoreText = Instantiate(highScoreTextPrefab, transform.position, transform.rotation); // Instantiate the new high score as a text
            newHighScoreText.transform.SetParent(highScoresParent.transform); // Set the new high score text as a child of the High Scores so it will position itself correctly
            newHighScoreText.text = highScore.ToString(); // Set the new text to the most recent high score
            newHighScoreText.gameObject.transform.SetSiblingIndex(0);

            //highScoresObjects.Add(newHighScoreText.gameObject); // Add the new high score text object to the list of high scores (GAME OBJECTS)

            SaveHighScores();
        }
    }

    // UPDATE BACKGROUND COLOR
    public void BackgroundUpdate ()
    {
        foreach (int value in bgUpdates)
        {
            if (currentScore == value)
            {
                bgUpdateCounter++;

                mainCam.backgroundColor = bgColors[bgUpdateCounter];
                Debug.Log("bgUpdateCounter: " + bgUpdateCounter + " and value: " + value);
            }
        }
    }
}
