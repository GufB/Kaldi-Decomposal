using UnityEngine;
using UnityEngine.Video;

public class RandomizedVideos : MonoBehaviour
{
    public VideoClip[] howToPlayVideos;
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        PlayRandomVideo();
    }

    void PlayRandomVideo()
    {
        int randomIndex = Random.Range(0, howToPlayVideos.Length);
        videoPlayer.clip = howToPlayVideos[randomIndex];
        videoPlayer.Play();
    }
}
