using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public InputField inputField;
    public TextMeshProUGUI highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        //Load HighScore
        HighScoreEntry highScore = DataManager.Instance.GetHighScore();
        DataManager.Instance.highScore = highScore.score;
        inputField.text = highScore.name;
        highScoreText.text= "High Score: " + highScore.name + ": " + highScore.score;
    }
    public void StartNew()
    {
        DataManager.Instance.inputName = inputField.text;
        SceneManager.LoadScene(1);
    }
    public void GetHighScoreScene()
    {
        SceneManager.LoadScene(2);
    }
    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
             Application.Quit(); // original code to quit Unity player
        #endif
    }
   
}

