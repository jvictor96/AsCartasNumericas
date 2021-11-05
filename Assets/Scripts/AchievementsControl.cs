using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        refresh();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Medalhas").GetComponent<Text>().text = "Você possui " + PlayerPrefs.GetInt("Ouros").ToString() + " ouros, " + PlayerPrefs.GetInt("Pratas").ToString() + " pratas e " + PlayerPrefs.GetInt("Bronzes").ToString() + " bronzes";
    }

    void refresh()
    {
        if (PlayerPrefs.GetInt("Aspirante") == 1)
        {
            GameObject.Find("A1Star").GetComponent<Image>().color = Color.yellow;
        } else
        {
            GameObject.Find("A1Star").GetComponent<Image>().color = new Color(30, 30, 30);
        }


        if (PlayerPrefs.GetInt("Pro") == 1)
        {
            GameObject.Find("A2Star").GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            GameObject.Find("A2Star").GetComponent<Image>().color = new Color(30,30,30);
        }


        if (PlayerPrefs.GetInt("CardGamer") == 1)
        {
            GameObject.Find("A3Star").GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            GameObject.Find("A3Star").GetComponent<Image>().color = new Color(30, 30, 30);
        }


        if (PlayerPrefs.GetInt("Magnata") == 1)
        {
            GameObject.Find("A4Star").GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            GameObject.Find("A4Star").GetComponent<Image>().color = new Color(30, 30, 30);
        }


        if (PlayerPrefs.GetInt("Campeao") == 1)
        {
            GameObject.Find("A5Star").GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            GameObject.Find("A5Star").GetComponent<Image>().color = new Color(30, 30, 30);
        }
    }

    public void checkAchiv()
    {
        if (PlayerPrefs.GetInt("moedas") > 1000)
        {
            Social.ReportProgress(AsCartas.GPGSIds.achievement_magnata, 100.0f, (bool success) => {
                if (success)
                {
                    PlayerPrefs.SetInt("Magnata", 1);
                }
            });
        }

        if (PlayerPrefs.GetInt("rec0") > 400)
        {
            Social.ReportProgress(AsCartas.GPGSIds.achievement_card_gamer, 100.0f, (bool success) => {
                if (success)
                {
                    PlayerPrefs.SetInt("CardGamer", 1);
                }
            });
        }

        if (PlayerPrefs.GetInt("rec1") > 400)
        {
            Social.ReportProgress(AsCartas.GPGSIds.achievement_card_gamer, 100.0f, (bool success) => {
                if (success)
                {
                    PlayerPrefs.SetInt("CardGamer", 1);
                }
            });
        }

        if (PlayerPrefs.GetInt("rec2") > 400)
        {
            Social.ReportProgress(AsCartas.GPGSIds.achievement_card_gamer, 100.0f, (bool success) => {
                if (success)
                {
                    PlayerPrefs.SetInt("CardGamer", 1);
                }
            });
        }

        PlayerPrefs.Save();
        refresh();
    }
}
