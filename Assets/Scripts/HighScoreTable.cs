using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highScoreEntyTransfomList;
    private void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);
        HighScores highScores = DataManager.Instance.GetHighScores();

        highScoreEntyTransfomList = new List<Transform>();
        if (PlayerPrefs.HasKey("highScoreTable"))
        {
            foreach (HighScoreEntry highScoreEntry in highScores.highScoreEntryList)
            {
                CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntyTransfomList);
            }
        }
    }
    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry,Transform container,List<Transform> transformList)
    {
        float templateHeight = 30f; 
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, 120+-templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;

        switch (rank)
        {
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            default: rankString = rank + "TH"; break;
        }
        int score = highScoreEntry.score;
        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();
        string name = highScoreEntry.name;
        entryTransform.Find("NameText").GetComponent<Text>().text = name;
        entryTransform.Find("BackgroundScore").gameObject.SetActive(rank % 2 == 1);
        transformList.Add(entryTransform);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
   
}


