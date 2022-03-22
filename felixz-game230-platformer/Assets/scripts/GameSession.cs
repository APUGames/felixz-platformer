using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int Lives = 3;
    [SerializeField] int Points = 0;
    [SerializeField] Text lives;

    [SerializeField] Text score;

    private void Awake()
    {
        
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }

        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessPlayerDeath()
    {
        if(Lives > 1)
        {
            SubtractLife();
        }

        else
        {
            ResetGameSession();
        }
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);

        Destroy(gameObject);
    }

    public void AddLife()
    {
        Lives++;
    }

    private void SubtractLife()
    {
        Lives--;

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        lives.text = Lives.ToString();
    }

    public void ProcssPlayerScore(int points)
    {
        Points += points;
        score.text = Points.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        lives.text = Lives.ToString();
        score.text = Points.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
