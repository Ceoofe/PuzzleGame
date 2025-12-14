using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    TMP_Text result;
    TMP_Text score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        result = transform.Find("Result").GetComponent<TMP_Text>();
        score = transform.Find("Score").GetComponent<TMP_Text>();
        if (GameController.gameOver == true)
        {
            result.text = "Game Over!";
        }
        else
        {
            result.text = "You Win!";
        }    
        score.text = "Score: " + GameController.score;
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}
