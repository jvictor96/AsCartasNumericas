using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public float reaction;
    public int vidas, level, finishes, moedas, selec;
    string trilha;
    bool survival = false, tutorial;
    public GameObject TheUI;
    // Start is called before the first frame update
    void Start()
    {
        tutorial = PlayerPrefs.GetInt("tutorial") == 1;
        moedas = PlayerPrefs.GetInt("moedas");

        if (PlayerPrefs.GetInt("survival") == 1)
        {
            survival = true;
            if (PlayerPrefs.GetInt("rec" + PlayerPrefs.GetInt("modo").ToString() + "d") == DateTime.Today.Day)
            {
                GameObject.Find("RAntigo").GetComponent<Text>().text = PlayerPrefs.GetInt("rec" + PlayerPrefs.GetInt("modo")).ToString();
            }
        } else
        {
            level = PlayerPrefs.GetInt("nivel");
            trilha = GameObject.Find("Trilhas").transform.GetChild(PlayerPrefs.GetInt("trilha")).name;
            finishes = PlayerPrefs.GetInt("finishes");

            for (int i = 0; i < finishes; i++)
            {
                GameObject.Find("Star" + i.ToString()).GetComponent<Image>().color = Color.yellow;
            }
        }
        setColors(GameObject.Find("Card1").transform.GetChild(0).GetComponent<Skin>().baseColor);


        if (PlayerPrefs.GetInt("som") == 1)
        {
            GameObject.Find("CartaS").GetComponent<AudioSource>().mute = true;
            GameObject.Find("VitoriaS").GetComponent<AudioSource>().mute = true;
            GameObject.Find("AcertoS").GetComponent<AudioSource>().mute = true;
            GameObject.Find("ErroS").GetComponent<AudioSource>().mute = true;
            GameObject.Find("BeginS").GetComponent<AudioSource>().mute = true;
        }
        else
        {
            GameObject.Find("CartaS").GetComponent<AudioSource>().mute = false;
            GameObject.Find("VitoriaS").GetComponent<AudioSource>().mute = false;
            GameObject.Find("AcertoS").GetComponent<AudioSource>().mute = false;
            GameObject.Find("ErroS").GetComponent<AudioSource>().mute = false;
            GameObject.Find("BeginS").GetComponent<AudioSource>().mute = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Level").GetComponent<Text>().text = "Nível " + (level + 1);
        GameObject.Find("Baralho").GetComponent<Text>().text = GameObject.Find("TableTop").GetComponent<Go>().baralho.Count + " cartas restantes";
        if (survival)
        {
            GameObject.Find("Level").GetComponent<Text>().text = "Deck Building";
        }

        for (int i = 0; i < 4; i++)
        {
            GameObject.Find("Erros").transform.GetChild(i).GetComponent<Image>().enabled = true;
        }
        for (int i = 0; i < 4 - vidas; i++)
        {
            GameObject.Find("Erros").transform.GetChild(i).GetComponent<Image>().enabled = false;
        }

        if (reaction > 1)
        {
            reaction -= Time.deltaTime;
            setColors(new Color(0.5f * (Mathf.Sin(reaction * 20) + 1) / 2, 0.8f * (Mathf.Sin(reaction * 20) + 1) / 2, 1f));
        }else if (reaction < -1)
        {
            reaction += Time.deltaTime;
            setColors(new Color(1f, 0.8f * (Mathf.Sin(reaction * 20) + 1) / 2, 0.5f * (Mathf.Sin(reaction * 20) + 1) / 2));
        } else
        {
            setColors(GameObject.Find("Card1").transform.GetChild(0).GetComponent<Skin>().baseColor);
        }

        spoiler();
    }

    void setColors(Color c)
    {
        foreach (Transform g in TheUI.transform)
        {
            if (g.tag == "Text")
            {
                g.GetComponent<Text>().color = c;
            }
            if (g.tag == "Image")
            {
                g.GetComponent<Image>().color = c;
            }

            foreach (Transform t in g)
            {
                if (t.tag == "Image")
                {
                    t.GetComponent<Image>().color = c;
                }
            }
        }
    }

    void spoiler()
    {
        int bigOne = 0;
        List<int> quem = new List<int>();

        selec = 0;
        foreach (Transform g in GameObject.Find("Hand").transform)
        {
            if (g.GetComponent<PlayerHand>().select)
            {
                selec++;
                if(g.GetComponent<PlayerHand>().i > bigOne)
                {
                    bigOne = g.GetComponent<PlayerHand>().i;
                }
                quem.Add(g.GetComponent<PlayerHand>().i);
            }

        }

        if(GameObject.Find("Mensagem").GetComponent<Text>().text.Contains("Pegando")|| GameObject.Find("Mensagem").GetComponent<Text>().text.Contains("Jogando"))
        {
            GameObject.Find("Mensagem").GetComponent<Mensagem>().show("");
        }

        if (!GetComponent<Go>().venceu&&(!GetComponent<Go>().tutorial||GetComponent<Tutorial>().step>8))
        {
            if (quem.Count > 2)
            {
                quem.Remove(bigOne);
                string msg = "Jogando\n\n" + quem[0].ToString();
                quem.RemoveAt(0);
                foreach (int i in quem)
                {
                    msg += " x " + i.ToString();
                }
                msg += " = " + bigOne.ToString(); ;
                GameObject.Find("Mensagem").GetComponent<Mensagem>().show(msg);
            }
        } else if (GetComponent<Go>().venceu && quem.Count > 0)
        {
            string msg = "Pegando\n\n";
            int index = 0;
            foreach (int i in quem)
            {
                if(index != 0) {
                    msg += " . ";
                }
                if(i==-1)
                {
                    msg += " completar vidas ";
                } else
                {
                    msg += i.ToString();
                }
                index++;
            }
            GameObject.Find("Mensagem").GetComponent<Mensagem>().show(msg);
        }
    }

    public void menu()
    {
        string modo = "";
        if(survival)
        {
            GameObject.Find("MenuBuilding").GetComponent<Canvas>().enabled = true;
            switch(PlayerPrefs.GetInt("modo"))
            {
                case 0:
                    modo = "Modo sem o 5";
                    break;
                case 1:
                    modo = "Modo completo";
                    break;
                case 2:
                    modo = "Modo sem o 2";
                    break;
            }
            GameObject.Find("MenuBuilding").transform.GetChild(1).GetComponent<Text>().text = "Deck Building - " + modo;
            GameObject.Find("RNovo").GetComponent<Text>().text = GetComponent<Go>().score.ToString();
        } else
        {
            GameObject.Find("MenuArcade").transform.GetChild(1).GetComponent<Text>().text = trilha + " - Nível " + (level+1);
            GameObject.Find("MenuArcade").GetComponent<Canvas>().enabled = true;
        }
    }

    public void react(bool resultado)
    {
        if (resultado)
        {
            GameObject.Find("AcertoS").GetComponent<AudioSource>().Play();
            reaction = 2;
        } else
        {
            GameObject.Find("ErroS").GetComponent<AudioSource>().Play();
            reaction = -2;
        }
        if (!tutorial)
        {
            if (resultado)
            {
                GameObject.Find("Mensagem").GetComponent<Mensagem>().show("Muito bem! É assim que se faz!");
                if (survival && GetComponent<Go>().lastHand > 100)
                {
                    GameObject.Find("Mensagem").GetComponent<Mensagem>().show("Uau! Cartas altas?\nVocê ganhou uma moeda! (e um monte de pontos)");
                    moedas++;
                    PlayerPrefs.SetInt("moedas", moedas);
                    PlayerPrefs.Save();
                    GetComponent<Go>().score += 5;
                }
                if (survival && GetComponent<Go>().lastHand > 200)
                {
                    GameObject.Find("Mensagem").GetComponent<Mensagem>().show("Mais de 200?!\nVocê ganhou duas moedas! (e um monte de pontos)");
                    moedas++;
                    PlayerPrefs.SetInt("moedas", moedas);
                    PlayerPrefs.Save();
                    GetComponent<Go>().score += 5;
                }
            }
            else
            {
                GameObject.Find("Mensagem").GetComponent<Mensagem>().show("O produto não foi igual :(");
            }
        }
        else if (GetComponent<Tutorial>().step > 5)
        {
            if (resultado)
            {
                GameObject.Find("Mensagem").GetComponent<Mensagem>().show("Muito bem! É assim que se faz!");
            }
            else
            {
                GameObject.Find("Mensagem").GetComponent<Mensagem>().show("O produto não foi igual :(");
            }
        }
    }

    public void win()
    {
        GameObject.Find("AcertoS").GetComponent<AudioSource>().Stop();
        GameObject.Find("ErroS").GetComponent<AudioSource>().Stop();
        GameObject.Find("VitoriaS").GetComponent<AudioSource>().Play();
        if (survival)
        {
            GameObject.Find("Mensagem").GetComponent<Mensagem>().show("Uhull! Você rodou o baralho\nEscolha mais 3 cartas para entrarem nele");
            GetComponent<SurvivalEngine>().sort();
        }
        else
        {
            GameObject.Find("closeA").GetComponent<Text>().enabled = false;
            GameObject.Find("closeD").GetComponent<Text>().enabled = false;
            if (level>=5)
            {
                PlayerPrefs.SetInt("moedas",moedas + 10);
            } else
            {
                PlayerPrefs.SetInt("moedas", moedas + 2);
            }
            if (finishes < 2)
            {
                finishes++;
            }
            if(level == 5&& GameObject.Find("Trilhas").transform.childCount>PlayerPrefs.GetInt("trilha") + 1)
            {
                PlayerPrefs.SetInt(GameObject.Find("Trilhas").transform.GetChild(PlayerPrefs.GetInt("trilha")+1).name, 1);
            } 
            PlayerPrefs.SetInt(trilha + level.ToString(), finishes);
            PlayerPrefs.Save();
            GameObject.Find("MenuArcade").GetComponent<Canvas>().enabled = true;
            GameObject.Find("Retry").GetComponent<Text>().enabled = false;
            GameObject.Find("Resultado").GetComponent<Text>().text = "Parabéns! Você fechou o level";
            for (int i = 0; i < finishes; i++)
            {
                GameObject.Find("Star" + i.ToString()).GetComponent<Image>().color = Color.yellow;
            }
        }
    }

    public void lose()
    {
        if (survival)
        {
            GetComponent<UIControl>().vidas = 0;
            GameObject.Find("RNovo").GetComponent<Text>().text = GameObject.Find("TableTop").GetComponent<Go>().score.ToString();
            if (GetComponent<Go>().score > PlayerPrefs.GetInt("rec" + PlayerPrefs.GetInt("modo")) || PlayerPrefs.GetInt("rec" + PlayerPrefs.GetInt("modo") + "d") != DateTime.Today.Day)
            {
                GameObject.Find("MenuBuilding").transform.GetChild(1).GetComponent<Text>().text = "Parabéns! Você quebrou o seu record";
            }
            else
            {
                GameObject.Find("MenuBuilding").transform.GetChild(1).GetComponent<Text>().text = "As suas vidas terminaram :(\nVocê mantém o record";
            }
            GameObject.Find("MenuBuilding").GetComponent<Canvas>().enabled = true;
        }
        else
        {
            GetComponent<UIControl>().vidas = 0;
            GameObject.Find("MenuArcade").GetComponent<Canvas>().enabled = true;
            GameObject.Find("MenuArcade").transform.GetChild(1).GetComponent<Text>().text = "As suas vidas terminaram :(";
        }
        GameObject.Find("closeA").GetComponent<Text>().enabled = false;
        GameObject.Find("closeD").GetComponent<Text>().enabled = false;
    }

    void setScore()
    {
        switch(PlayerPrefs.GetInt("modo"))
        {
            case 0:
                GetComponent<PostScores>().postModoSem5Score(GetComponent<Go>().score);
                break;
            case 1:
                GetComponent<PostScores>().postModoCompletoScore(GetComponent<Go>().score);
                break;
            case 2:
                GetComponent<PostScores>().postModoSem2Score(GetComponent<Go>().score);
                break;
        }
    }

    public void sair()
    {
        if (survival && (GetComponent<Go>().score > PlayerPrefs.GetInt("rec" + PlayerPrefs.GetInt("modo")) || PlayerPrefs.GetInt("rec" + PlayerPrefs.GetInt("modo") + "d") != DateTime.Today.Day))
        {
            setScore();
            PlayerPrefs.SetInt("rec" + PlayerPrefs.GetInt("modo"), GetComponent<Go>().score);
            PlayerPrefs.SetInt("rec" + PlayerPrefs.GetInt("modo") + "d", DateTime.Today.Day);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene("Home");
    }

    public void close()
    {
        GameObject.Find("MenuArcade").GetComponent<Canvas>().enabled = false;
        GameObject.Find("MenuBuilding").GetComponent<Canvas>().enabled = false;
    }
}
