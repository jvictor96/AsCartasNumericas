using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class SurvivalEngine : MonoBehaviour
{
    public int[] op;
    public int selec;
    List<int> baralho, universe;

    void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        universe = GameObject.Find("Modos").transform.GetChild(PlayerPrefs.GetInt("modo")).GetComponent<Level>().universe;
    }

    // Update is called once per frame
    void Update()
    {
        selec = 0;
        foreach (Transform g in GameObject.Find("Hand").transform) { 
            if(g.GetComponent<PlayerHand>().select) {
                selec++;
            }
        }
    }

    public void sort()
    {
        for(int i=0; i<8; i++)
        {
            bool goOn = false;
            while(!goOn)
            {
                int r = Random.Range(-1, universe.Count);
                GameObject.Find("Hand").transform.GetChild(i).GetComponent<PlayerHand>().i = r;
                if(r >= 0)
                {
                    GameObject.Find("Hand").transform.GetChild(i).GetComponent<PlayerHand>().i = universe[r];
                }
                goOn = true;
                for (int j = 0; j < i; j++)
                {
                    if (GameObject.Find("Hand").transform.GetChild(i).GetComponent<PlayerHand>().i == GameObject.Find("Hand").transform.GetChild(j).GetComponent<PlayerHand>().i)
                    {
                        goOn = false;
                    }
                }
            } 
            GameObject.Find("Hand").transform.GetChild(i).GetComponent<PlayerHand>().done = false;
        }
    }

    public void addCards()
    {
        int n = 0;
        foreach(Transform g in GameObject.Find("Hand").transform)
        {
            if(g.GetComponent<PlayerHand>().select)
            {
                n++;
            }
        }
        if(n==3)
        {
            foreach (Transform g in GameObject.Find("Hand").transform)
            {
                if (g.GetComponent<PlayerHand>().select)
                {
                    if (g.GetComponent<PlayerHand>().i>=0)
                    {
                        GetComponent<Go>().deck.Add(g.GetComponent<PlayerHand>().i);
                    } else
                    {
                        GetComponent<UIControl>().vidas = 4;
                    }
                }
            }

            GetComponent<Go>().venceu = false;
            GetComponent<Go>().reset();
        }
    }
}

