using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PussyOutScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    public void PussyOut()
    {
        PlayerPrefs.SetInt("Branull", 0);
        PlayerPrefs.Save();
        titleMain.SetActive(true);
        titleNull.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public GameObject titleMain;
    public GameObject titleNull;
}
