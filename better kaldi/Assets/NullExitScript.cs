using System;
using UnityEngine;

public class NullExitScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!hastriggered && gc.exitsReached == 3 && gc.finaleMode && other.tag == "Player")
        {
            hastriggered = true;
            braynull.SetActive(true);
            es.Lower();
            audioDevice.PlayOneShot(aud_Switch, 0.8f);
        }
    }

    public GameControllerScript gc;
    public EntranceScript es;
    public GameObject braynull;
    public AudioSource audioDevice;
    public AudioClip aud_Switch;

    private bool hastriggered = false;
}
