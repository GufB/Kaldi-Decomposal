using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class LeaveScene : MonoBehaviour
{
	public AudioSource audioDevice;
    public RawImage fadeImage;
    private string sceneToLoad = "MainMenu";
    public float fadeDuration = 1.5f;
	public AudioClip aud_buttonpress;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
            this.audioDevice.PlayOneShot(this.aud_buttonpress);
            StartCoroutine(FadeAndLoad());
		}
    }
    private IEnumerator FadeAndLoad()
    {
        float timer = 0f;
        Color color = fadeImage.color;
        fadeImage.gameObject.SetActive(true);
        color.a = 0f;
        fadeImage.color = color;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
