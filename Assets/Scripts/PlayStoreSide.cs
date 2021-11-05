using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class PlayStoreSide : MonoBehaviour
{
    PlayGamesPlatform platform;
    int r2 = 0, r5 = 0, rC = 0, loadState, qtt2 = 0, qtt5 = 0, qttC = 0;
    // Start is called before the first frame update
    void Start()
    {
        loadState = 0;
        //checkLiga();
        if (!PlayGamesPlatform.Instance.IsAuthenticated())
        {
            GameObject.Find("PodiumB").GetComponent<Image>().enabled = false;
            login();
        } else
        {
            loadScores1();
            GameObject.Find("PodiumB").GetComponent<Image>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((r2 == 1 && qtt2 > 2) || (r5 == 1 && qtt5 > 2) || (rC == 1 && qttC > 2))
        {
            if (DateTime.Now.Hour >= 20)
            {
                GameObject.Find("TakeMedals").GetComponent<Text>().text = "Você detém o primeiro lugar!\nResgatar medalhas";
            } else
            {
                GameObject.Find("TakeMedals").GetComponent<Text>().text = "Você detém o primeiro lugar!\nVolte após 8 da noite para resgatar a sua medalha";
            }
        }

        switch (loadState)
        {
            case 1:
                loadState++;
                loadScores2();
                break;
            case 3:
                loadState++;
                loadScores3();
                break;
            case 5:
                loadState++;
                loadScores4();
                break;
            case 7:
                if (DateTime.Now.Hour > 7)
                {
                    if (rC > 0) { GameObject.Find("posC").GetComponent<Text>().text = rC + "º colocado"; } else { GameObject.Find("posC").GetComponent<Text>().text = "fora do top 20"; }
                    if (r2 > 0) { GameObject.Find("pos2").GetComponent<Text>().text = r2 + "º colocado"; } else { GameObject.Find("pos2").GetComponent<Text>().text = "fora do top 20"; }
                    if (r5 > 0) { GameObject.Find("pos5").GetComponent<Text>().text = r5 + "º colocado"; } else { GameObject.Find("pos5").GetComponent<Text>().text = "fora do top 20"; }
                }
                loadState++;
                break;
        }
    }

    public void checkConection()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            GameObject.Find("PodiumB").GetComponent<Image>().enabled = true;
        }
        else
        {
            GameObject.Find("PodiumB").GetComponent<Image>().enabled = false;
        }
    }

    void checkLiga()
    {
        PlayerPrefs.SetString("Liga", "Sem Liga");
        if (PlayerPrefs.GetInt("Bronzes") > 0 || PlayerPrefs.GetInt("Pratas") > 0 || PlayerPrefs.GetInt("Ouros") > 0)
        {
            PlayerPrefs.SetString("Liga", "Bronze");
        }
        if (PlayerPrefs.GetInt("Ouros") > 2)
        {
            PlayerPrefs.SetString("Liga", "Prata");
        }
        if (PlayerPrefs.GetInt("Ouros") > 9)
        {
            PlayerPrefs.SetString("Liga", "Ouro");
        }
        PlayerPrefs.Save();
    }

    public void TakePrize()
    {
        int o = PlayerPrefs.GetInt("Ouros"), p = PlayerPrefs.GetInt("Pratas"), b = PlayerPrefs.GetInt("Bronzes");
        if (DateTime.Now.Hour >= 20 && PlayerPrefs.GetInt("Dia") != DateTime.Today.Day)
        {
            if (qtt2 > 2)
            {
                if (r2 == 1) { o++; } else if (r2 == 2) { p++; } else if (r2 == 3) { b++; }
            }
            if (qtt5 > 2)
            {
                if (r5 == 1) { o++; } else if (r5 == 2) { p++; } else if (r5 == 3) { b++; }
            }
            if (qttC > 2)
            {
                if (rC == 1) { o++; } else if (rC == 2) { p++; } else if (rC == 3) { b++; }
            }
            if (o != PlayerPrefs.GetInt("Ouros") || p != PlayerPrefs.GetInt("Pratas") || b != PlayerPrefs.GetInt("Bronzes"))
            {
                PlayerPrefs.SetInt("Ouros", o); PlayerPrefs.SetInt("Pratas", p); PlayerPrefs.SetInt("Bronzes", b);
                PlayerPrefs.SetInt("Dia", DateTime.Today.Day);
            }
            if (o > 0)
            {
                Social.ReportProgress(AsCartas.GPGSIds.achievement_campeo, 100.0f, (bool success) => {
                    if (success)
                    {
                        PlayerPrefs.SetInt("Campeão", 1);
                    }
                });
            }
            PlayerPrefs.Save();
        }
    }

    public void login()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
            if (!PlayGamesPlatform.Instance.IsAuthenticated())
            {
                GameObject.Find("PodiumB").GetComponent<Image>().enabled = false;
            }
            else
            {
                loadScores1();
                GameObject.Find("PodiumB").GetComponent<Image>().enabled = true;
            }
        });
    }

    public void ShowLB()
    {
        Social.ShowLeaderboardUI();
    }

    public void PostScores()
    {
        if (PlayerPrefs.GetInt("rec0d") == DateTime.Today.Day)
        {
            Social.ReportScore(PlayerPrefs.GetInt("rec0"), AsCartas.GPGSIds.leaderboard_deck_building_sem_5, (bool success) => {
                if (success) { GameObject.Find("Span").GetComponent<UltraMsg>().show("Sucesso"); } else { GameObject.Find("Span").GetComponent<UltraMsg>().show("Falha"); }
            });
        }
    }

    void loadScores1()
    {
        string query = AsCartas.GPGSIds.leaderboard_deck_building_completo;
        string[] ids = new string[20];
        PlayGamesPlatform.Instance.LoadScores(
            query,
            LeaderboardStart.TopScores,
            20,
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.Daily,
            (data) =>
            {
                int index = 0;
                qttC = 0;
                foreach (IScore id in data.Scores)
                {
                    ids[index] = data.Scores[index].userID;
                    qttC++;
                    index++;
                }
                if (index > 0)
                {
                    Social.LoadUsers(ids, (users) =>
                    {
                        index = 0;
                        foreach (IUserProfile user in users)
                        {
                            if (index < 3)
                            {
                                GameObject.Find("LC").transform.GetChild(index).GetComponent<Text>().text = user.userName + " " + data.Scores[index].value;
                            }

                            if(user.userName == Social.localUser.userName)
                            {
                                rC = index + 1;
                            }
                            index++;
                        }
                    });
                }
                loadState++;
            });
    }

    void loadScores2()
    {
        string query = AsCartas.GPGSIds.leaderboard_deck_building_sem_2;
        string[] ids = new string[20];
        PlayGamesPlatform.Instance.LoadScores(
            query,
            LeaderboardStart.TopScores,
            20,
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.Daily,
            (data) =>
            {
                int index = 0;
                qtt2 = 0;
                foreach (IScore id in data.Scores)
                {
                    ids[index] = data.Scores[index].userID;
                    qtt2++;
                    index++;
                }
                if (index > 0)
                {
                    Social.LoadUsers(ids, (users) =>
                    {
                        index = 0;
                        foreach (IUserProfile user in users)
                        {
                            if (index < 3)
                            {
                                GameObject.Find("LS2").transform.GetChild(index).GetComponent<Text>().text = user.userName + " " + data.Scores[index].value;
                            }

                            if (user.userName == Social.localUser.userName)
                            {
                                r2 = index + 1;
                            }
                            index++;
                         }
                    });
                }
                loadState++;
            });
    }

    void loadScores3()
    {
        string query = AsCartas.GPGSIds.leaderboard_deck_building_sem_5;
        string[] ids = new string[20];
        PlayGamesPlatform.Instance.LoadScores(
            query,
            LeaderboardStart.TopScores,
            10,
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.Daily,
            (data) =>
            {
                int index = 0;
                qtt5 = 0;
                foreach (IScore id in data.Scores)
                {
                    ids[index] = data.Scores[index].userID;
                    qtt5++;
                    index++;
                }
                if (index > 0)
                {
                    Social.LoadUsers(ids, (users) =>
                    {
                        index = 0;
                        foreach (IUserProfile user in users)
                        {
                            if (index < 3)
                            {
                                GameObject.Find("LS5").transform.GetChild(index).GetComponent<Text>().text = user.userName + " " + data.Scores[index].value;
                            }

                            if (user.userName == Social.localUser.userName)
                            {
                                r5 = index + 1;
                            }
                            index++;
                        }
                    });
                }
                loadState++;
            });
    }

    void loadScores4()
    {
        string[] ids = new string[3];
        PlayGamesPlatform.Instance.LoadScores(
            AsCartas.GPGSIds.leaderboard_medalhas_de_ouro,
            LeaderboardStart.TopScores,
            10,
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.AllTime,
            (data) =>
            {
                int index = 0;
                foreach (IScore id in data.Scores)
                {
                    if (index < 5)
                    {
                        ids[index] = data.Scores[index].userID;
                    }
                    index++;
                }
                if(index>0)
                {
                    Social.LoadUsers(ids, (users) =>
                    {
                        index = 0;
                        foreach (IUserProfile user in users)
                        {
                            GameObject.Find("LMV").transform.GetChild(index).GetComponent<Text>().text = user.userName + " " + data.Scores[index].value + " ouros";
                            index++;
                        }
                    });
                }
                loadState++;
            });
    }
}
