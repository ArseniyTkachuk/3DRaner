using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;


public class ButtonController : MonoBehaviour
{
    private Transform player;
    private int index;
    private void Start()
    {
        player = GameObject.Find("Player").transform;
        index = PlayerPrefs.GetInt("chosenSkin");
            for (int i = 0; i < player.childCount; i++)
                player.GetChild(i).gameObject.SetActive(false);
            player.GetChild(index).gameObject.SetActive(true);
        
    }




    public void LoadScene(string NameScene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(NameScene);
    }

    public void PlusMoney()
    {
        int coins = PlayerPrefs.GetInt("Coints");
        coins += 1000;
        PlayerPrefs.SetInt("Coints", coins);
    }


}
