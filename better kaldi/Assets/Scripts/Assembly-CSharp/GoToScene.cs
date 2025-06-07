using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GoToScene : MonoBehaviour
{
	public AudioSource audioDevice;
    public RawImage fadeImage;
    public GameObject fadeImagee;
    private string sceneToLoad = "DrPerky";
    public float fadeDuration = 1.5f;
	public AudioClip aud_buttonpress;
    public void LoadScene()
    {
		this.audioDevice.PlayOneShot(this.aud_buttonpress);
        fadeImagee.SetActive(true);
        StartCoroutine(FadeAndLoad());
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
