using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fundo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void refresh()
    {
        GetComponent<Image>().sprite = GameObject.Find("ModelosFundos").transform.GetChild(PlayerPrefs.GetInt("fundo")).GetComponent<Image>().sprite;
    }
}
