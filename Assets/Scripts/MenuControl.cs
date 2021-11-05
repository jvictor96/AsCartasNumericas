using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class MenuControl : MonoBehaviour
{
    bool submenu = false;
    GameObject clone;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.SetInt("icone0", 1); PlayerPrefs.SetInt("skin0", 1);
        PlayerPrefs.SetInt("fundo0", 1); PlayerPrefs.SetInt("fundo1", 1);
    }
    void Start()
    {
        if (PlayerPrefs.GetInt("som") == 1)
        {
            GameObject.Find("CartaS").GetComponent<AudioSource>().mute = true;
            GameObject.Find("Som").GetComponent<MenuOption>().on = false;
        }
        else
        {
            GameObject.Find("CartaS").GetComponent<AudioSource>().mute = false;
            GameObject.Find("Som").GetComponent<MenuOption>().on = true;
        }

        GameObject.Find("Moedas").GetComponent<Text>().text = "Você tem " + PlayerPrefs.GetInt("moedas").ToString() + " moedas";
        if (PlayerPrefs.GetInt("moedas") != 1)
        {
            GameObject.Find("Moedas").GetComponent<Text>().text += "s";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("rec0d") == DateTime.Today.Day)
        {
            GameObject.Find("R1").GetComponent<Text>().text = PlayerPrefs.GetInt("rec0").ToString();
        }


        if (PlayerPrefs.GetInt("rec1d") == DateTime.Today.Day)
        {
            GameObject.Find("R2").GetComponent<Text>().text = PlayerPrefs.GetInt("rec1").ToString();
        }


        if (PlayerPrefs.GetInt("rec2d") == DateTime.Today.Day)
        {
            GameObject.Find("R3").GetComponent<Text>().text = PlayerPrefs.GetInt("rec2").ToString();
        }
    }

    public void StartSurvival(int destination)
    {
        PlayerPrefs.SetInt("modo", destination);
        PlayerPrefs.SetInt("survival", 1);
        PlayerPrefs.SetInt("tutorial", 0);
        SceneManager.LoadScene("Game");
    }

    public void ChangePage(int destination)
    {
        GameObject.Find("CartaS").GetComponent<AudioSource>().Play();
        switch (destination)
        {
            case 0:
                GameObject.Find("Arcade").GetComponent<Canvas>().enabled = true;
                GameObject.Find("Deck Building").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Shopping").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Conquistas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Cartas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Icones").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Fundos").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Op1").GetComponent<MenuOption>().on = true;
                GameObject.Find("Op2").GetComponent<MenuOption>().on = false;
                GameObject.Find("Op3").GetComponent<MenuOption>().on = false;
                GameObject.Find("Op4").GetComponent<MenuOption>().on = false;
                break;
            case 1:
                GameObject.Find("Arcade").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Deck Building").GetComponent<Canvas>().enabled = true;
                GameObject.Find("Shopping").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Conquistas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Cartas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Icones").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Fundos").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Op1").GetComponent<MenuOption>().on = false;
                GameObject.Find("Op2").GetComponent<MenuOption>().on = true;
                GameObject.Find("Op3").GetComponent<MenuOption>().on = false;
                GameObject.Find("Op4").GetComponent<MenuOption>().on = false;
                break;
            case 2:
                GameObject.Find("Arcade").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Deck Building").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Shopping").GetComponent<Canvas>().enabled = true;
                GameObject.Find("Conquistas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Cartas").GetComponent<Canvas>().enabled = true;
                GameObject.Find("Icones").GetComponent<Canvas>().enabled = true;
                GameObject.Find("Fundos").GetComponent<Canvas>().enabled = true;
                GameObject.Find("Op1").GetComponent<MenuOption>().on = false;
                GameObject.Find("Op2").GetComponent<MenuOption>().on = false;
                GameObject.Find("Op3").GetComponent<MenuOption>().on = true;
                GameObject.Find("Op4").GetComponent<MenuOption>().on = false;
                break;
            case 3:
                GameObject.Find("Arcade").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Deck Building").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Shopping").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Conquistas").GetComponent<Canvas>().enabled = true;
                GameObject.Find("Cartas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Icones").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Fundos").GetComponent<Canvas>().enabled = false;
                GameObject.Find("Op1").GetComponent<MenuOption>().on = false;
                GameObject.Find("Op2").GetComponent<MenuOption>().on = false;
                GameObject.Find("Op3").GetComponent<MenuOption>().on = false;
                GameObject.Find("Op4").GetComponent<MenuOption>().on = true;
                break;
        }
    }

    public void OpenCloseConfig()
    {
        if (GameObject.Find("Config").GetComponent<Canvas>().enabled)
        {
            GameObject.Find("Config").GetComponent<Canvas>().enabled = false;
        }
        else
        {
            GameObject.Find("Config").GetComponent<Canvas>().enabled = true;
        }
    }

    public void OpenClosePodium()
    {
        if (GameObject.Find("Podium").GetComponent<Canvas>().enabled)
        {
            GameObject.Find("Podium").GetComponent<Canvas>().enabled = false;
        }
        else
        {
            GameObject.Find("Podium").GetComponent<Canvas>().enabled = true;
            GameObject.Find("Liga").GetComponent<Text>().text = PlayerPrefs.GetString("Liga");

        }
    }

    public void MuteUnmute()
    {
        if(PlayerPrefs.GetInt("som")==1)
        {
            PlayerPrefs.SetInt("som",2);
            GameObject.Find("Som").GetComponent<MenuOption>().on = true;
            GameObject.Find("CartaS").GetComponent<AudioSource>().mute = false;
            GameObject.Find("CartaS").GetComponent<AudioSource>().Play();
        } else
        {
            PlayerPrefs.SetInt("som", 1);
            GameObject.Find("Som").GetComponent<MenuOption>().on = false;
            GameObject.Find("CartaS").GetComponent<AudioSource>().mute = true;
        }
        PlayerPrefs.Save();
    }
}
