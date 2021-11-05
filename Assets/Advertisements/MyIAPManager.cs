using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class MyIAPManager : MonoBehaviour
{
    public void MilMoedas()
    {
        PlayerPrefs.SetInt("moedas", PlayerPrefs.GetInt("moedas") + 1000);
        GameObject.Find("Moedas").GetComponent<Text>().text = "Você tem " + PlayerPrefs.GetInt("moedas").ToString() + " moeda";
    }
}