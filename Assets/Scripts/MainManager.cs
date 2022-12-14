using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text savedText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Awake() //take note of this, you'd want to load the score saved after gameover and display the text before anything else
    {
        RetentionManager.Instance.LoadNamenScore();
        savedText.text = $"Highscore : {RetentionManager.Instance.savedPlayerName} : {RetentionManager.Instance.playerScore}";
    }
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            SetScore(); //to call method written below 

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void SetScore()
    {
        if (RetentionManager.Instance.playerScore == 0) //this is for your first loop when there isnt a score saved 
        {
            RetentionManager.Instance.playerScore = m_Points; //store the innitial points you got into the player score variable you made
            RetentionManager.Instance.SaveNamenScore(); // proceed to save that first score and load it on void awake above

            savedText.text = $"Highscore : {RetentionManager.Instance.savedPlayerName} : {m_Points}"; //this will be your first highscore
        }
        if (m_Points > RetentionManager.Instance.playerScore) //once your previous high score has been beaten, this will come into play
        {
            RetentionManager.Instance.playerScore = m_Points; //store that score into player score and proceed to save it as the new highscore which will load at awake
            RetentionManager.Instance.SaveNamenScore();

            savedText.text = $"Highscore : {RetentionManager.Instance.savedPlayerName} : {m_Points}";

        }
        else if (m_Points < RetentionManager.Instance.playerScore) //added this in incase the score is lesser than your previous highscore 
        {
            savedText.text = $"You didnt make the Highscore, better luck next round! ";
        }
    }

}
