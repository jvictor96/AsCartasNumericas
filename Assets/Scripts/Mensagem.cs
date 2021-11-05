using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mensagem : MonoBehaviour
{
    float t;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (t > 0)
        {
            t -= Time.deltaTime;
        } else
        {
            GetComponent<Text>().text = "";
        }
    }

    public void show(string msg)
    {
        GetComponent<Text>().text = msg;
        t = 12f;
    }

    public void TurnTheCredit() {
        if(GameObject.Find("Creditos").GetComponent<Canvas>().enabled)
        {
            GameObject.Find("Creditos").GetComponent<Canvas>().enabled = false;
        } else
        {
            GameObject.Find("Creditos").GetComponent<Canvas>().enabled = true;
        }
    }
}
