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
    [SerializeField] private Camera mainCam;

    // High Score Stuff
    [Header("HIGH SCORE STUFF")]
    [SerializeField] private int highScore;
    [SerializeField] private TMP_Text highScoreTextPrefab;
    [SerializeField] private GameObject highScoresParent;
    public static List<int> highScoresList = new List<int>();

    private void Start()
    {
        LoadHighScores();
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
            // UPDATE HIGH SCORE TEXT AND FILE
            highScore = currentScore;
            highScoresList.Sort();
            highScoresList.Add(highScore); // Add the new high score int to the list of high scores (INTS - THIS IS WHAT IS SAVED LOCALLY)
            highScoresList.Reverse();

            TMP_Text newHighScoreText = Instantiate(highScoreTextPrefab, transform.position, transform.rotation); // Instantiate the new high score as a text
            newHighScoreText.transform.SetParent(highScoresParent.transform); // Set the new high score text as a child of the High Scores so it will position itself correctly
            newHighScoreText.text = highScore.ToString(); // Set the new text to the most recent high score
            newHighScoreText.gameObject.transform.SetSiblingIndex(0); // Set the text object as the first child to have it at the top

            SaveHighScores();
            ShowNewHighScorePanel(highScore);
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

    private void ShowNewHighScorePanel (int hs)
    {

    }
}
