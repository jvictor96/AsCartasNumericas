using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostScores : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void postModoSem2Score(int score)
    {
        Social.ReportScore(score, AsCartas.GPGSIds.leaderboard_deck_building_sem_2, (bool success) => {});
    }

    public void postModoSem5Score(int score)
    {
        Social.ReportScore(score, AsCartas.GPGSIds.leaderboard_deck_building_sem_5, (bool success) => { });
    }

    public void postModoCompletoScore(int score)
    {
        Social.ReportScore(score, AsCartas.GPGSIds.leaderboard_deck_building_completo, (bool success) => { });
    }
}
