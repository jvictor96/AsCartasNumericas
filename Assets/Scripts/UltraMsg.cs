using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltraMsg : MonoBehaviour
{
    float life;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(life>0)
        {
            life -= Time.deltaTime;
        } else
        {
            GetComponent<Canvas>().enabled = false;
        }
    }
    
    public void show(string msg)
    {
        life = 5;
        GetComponent<Canvas>().enabled = true;
        transform.GetChild(1).GetComponent<Text>().text = msg;
    }
}
