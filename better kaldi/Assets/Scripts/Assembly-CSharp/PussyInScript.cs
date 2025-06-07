using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PussyInScript : MonoBehaviour
{
    void Start()
    {
    }
    public void PussyIn()
    {
        PlayerPrefs.SetInt("Branull", 1);
        PlayerPrefs.Save();
        titleMain.SetActive(false);
        titleNull.SetActive(true);
    }


    void Update()
    {

    }
    public GameObject titleMain;
    public GameObject titleNull;
}
