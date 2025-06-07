using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class randomstupidtextforyctp : MonoBehaviour
{
    public string[] texts;
    public TextMeshProUGUI textDisplay;

    void Start()
    {
        DisplayRandomText();
    }

    void DisplayRandomText()
    {
        int randomIndex = Random.Range(0, texts.Length);
        textDisplay.text = texts[randomIndex];
    }
}
