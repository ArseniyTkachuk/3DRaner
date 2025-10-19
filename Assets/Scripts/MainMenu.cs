using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text CointText;
    [SerializeField] private Text HighScoreText;   

    private void Start()
    {
        int coins = PlayerPrefs.GetInt("Coints");
        CointText.text = coins.ToString();

        int highScore = PlayerPrefs.GetInt("HighScore");
        HighScoreText.text = highScore.ToString();
    }
}
