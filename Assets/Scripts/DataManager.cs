using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public float highScore;
    public string inputName;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //PlayerPrefs.DeleteKey("highScoreTable");
    }
    public HighScores GetHighScores()
    {
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
        return highScores;
    }
    public HighScoreEntry GetHighScore()
    {
        if (PlayerPrefs.HasKey("highScoreTable"))
        {
            string jsonString = PlayerPrefs.GetString("highScoreTable");
            HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
            return highScores.highScoreEntryList[0];
        }
        else
        {
            return new HighScoreEntry { name = "", score = 0 };
        }

    }
    public HighScoreEntry GetLastScore()
    {
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
        int count = highScores.highScoreEntryList.Count;
        return highScores.highScoreEntryList[count-1];
    }
    public void RemoveScore()
    {
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
        highScores.highScoreEntryList.RemoveAt(14);
        //Save updated HighScores
        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
    }
    public void AddHighScoreEntry(int score, string name)
    {
        HighScores highScores = new HighScores();
        highScores.highScoreEntryList = new List<HighScoreEntry>();
        string json;
        //Create highScoreEntry
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name };

        if (!PlayerPrefs.HasKey("highScoreTable"))
        {
            highScores.highScoreEntryList.Add(highScoreEntry);
            json = JsonUtility.ToJson(highScores);
            PlayerPrefs.SetString("highScoreTable", json);
            PlayerPrefs.Save();
        }
        else
        {

            //Load saved highScore
            string jsonString = PlayerPrefs.GetString("highScoreTable");

            highScores = JsonUtility.FromJson<HighScores>(jsonString);
            //Add new entry to HighScores
            highScores.highScoreEntryList.Add(highScoreEntry);
            //Sort list
            //https://zetcode.com/csharp/sortlist/
            highScores.highScoreEntryList.Sort((s1, s2) =>
            {
                return s2.score.CompareTo(s1.score);
            });
            //Save updated HighScores
            json = JsonUtility.ToJson(highScores);
            PlayerPrefs.SetString("highScoreTable", json);
            PlayerPrefs.Save();
        }

    }
    public bool CheckScoreCount()
    {
        if (PlayerPrefs.HasKey("highScoreTable"))
        {
            string jsonString = PlayerPrefs.GetString("highScoreTable");
            HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
            if (highScores.highScoreEntryList.Count > 14)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
       
    }

}
public class HighScores
{
    public List<HighScoreEntry> highScoreEntryList;

}
[System.Serializable]
//This line is required for JsonUtility
public class HighScoreEntry
{
    public int score;
    public string name;
}
