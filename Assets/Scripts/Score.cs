using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Text scoreText;

    private void Update()
    {
        scoreText.text = ((int)(player.position.z / 2)).ToString();
        if (((int)(player.position.z / 2)) > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", (int)(player.position.z / 2));
        }
    }
}
