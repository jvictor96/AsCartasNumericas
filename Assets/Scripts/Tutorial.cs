using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public List<int> baralho;
    public int step=0, init, target;
    bool boom = false;

    void Start()
    {
        if (PlayerPrefs.GetInt("tutorial")==1)
        {
            target = 5;
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void play(string id)
    {
        if (step == 0 && id == "Card5")
        {
            GameObject.Find("Card5").GetComponent<PlayerHand>().click();
            step++;
            target = 6;
        }
        if (step == 1 && id == "Card6")
        {
            GameObject.Find("Card6").GetComponent<PlayerHand>().click();
            step++;
            target = 8;
        }
        if (step == 2 && id == "Card8")
        {
            GameObject.Find("Card8").GetComponent<PlayerHand>().click();
            step++;
            target = 9;
        }
        if (step == 3 && id == "TableTop")
        {
            GameObject.Find("TableTop").GetComponent<Go>().click();
            step++;
            target = 1;
        }
        if (step == 4 && id == "Card1")
        {
            GameObject.Find("Card1").GetComponent<PlayerHand>().click();
            step++;
            target = 2;
        }
        if (step == 5 && id == "Card2")
        {
            GameObject.Find("Card2").GetComponent<PlayerHand>().click();
            step++;
            target = 3;
        }
        if (step == 6 && id == "Card3")
        {
            GameObject.Find("Card3").GetComponent<PlayerHand>().click();
            step++;
            target = 4;
        }
        if (step == 7 && id == "Card4")
        {
            GameObject.Find("Mensagem").GetComponent<Mensagem>().show("Uma jogada com vários fatores também é válida, note que 2x24x2=96");
            GameObject.Find("Card4").GetComponent<PlayerHand>().click();
            step++;
            target = 9;
        }
        if (step == 8 && id == "TableTop")
        {
            GameObject.Find("TableTop").GetComponent<Go>().click();
            step++;
            target = 0;
        }
        if (step == 9)
        {
            if (id == "TableTop")
            {
                GameObject.Find("TableTop").GetComponent<Go>().click();
            } else
            {
                GameObject.Find(id).GetComponent<PlayerHand>().click();
            }
        }
    }
}
