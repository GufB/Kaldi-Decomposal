using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EntranceOpenScript : MonoBehaviour
{
    public GameObject TutorBaldi;
    public GameObject Talk;
    public AudioSource schoolMusic;
    public AudioSource audioDevice;

    private bool hasTriggered = false;
    private bool played = false;

    private void Update()
    {
        if (!audioDevice.isPlaying && played)
        {
            Talk.SetActive(false);
            played = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;
        hasTriggered = true;
        schoolMusic.Stop();
        TutorBaldi.SetActive(true);
        Talk.SetActive(true);
        played = true;
    }
}
