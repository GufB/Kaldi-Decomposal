using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using System.IO;


public class MathGameScript : MonoBehaviour
{
    private void Start()
    {
        if (gc.mode == "null")
        {
            if (gc.notebooks >= 1)
            {
                yctpMain.SetActive(false);
                yctpNull.SetActive(true);
                SuperIdol.Stop();
                balIntroAudio.Stop();
                videoPlayer.Stop();
            }
        }
        gc.ActivateLearningGame();

        if (gc.mode != "null")
        {
            if (gc.notebooks >= 2)
        {
            string videoFolder = Path.Combine(Application.streamingAssetsPath, "YCTPVideos");
            string[] videoFiles = Directory.GetFiles(videoFolder, "*.mp4");


            if (videoFiles.Length > 0)
            {
                string selectedVideo = videoFiles[UnityEngine.Random.Range(0, videoFiles.Length)];
                videoPlayer.url = selectedVideo;
                videoPlayer.loopPointReached += ExitGame;
                videoPlayer.Play();
            }

            Video.SetActive(true);
            YCTP.SetActive(false);

            GameObject learnMusicObject = GameObject.Find("LearnMusic");
            if (learnMusicObject != null)
            {
                learnMusicAudio = learnMusicObject.GetComponent<AudioSource>();
                if (learnMusicAudio != null && learnMusicAudio.isPlaying)
                {
                    learnMusicAudio.Stop();
                }
            }

            if (balIntroAudio != null && balIntroAudio.isPlaying)
            {
                balIntroAudio.Stop();
            }
        }
        else if (gc.notebooks == 1)
        {
            Video.SetActive(false);
            text.SetActive(false);
            StartCoroutine(TextFade());
        }

        NewProblem();

        if (gc.spoopMode)
        {
            baldiFeedTransform.position = new Vector3(-1000f, -1000f, 0f);
        }
    }
        }


    private IEnumerator TextFade()
    {
        for (int i = 0; i < 12; i++)
        {
            textMeshPro.text = texts[i];
            yield return StartCoroutine(Fading(Color.black, 0.2f));
            yield return new WaitForSecondsRealtime(0.27f);
            float fadeOutDuration = (i == 11) ? 1.7f : 0.2f;
            yield return StartCoroutine(Fading(Color.white, fadeOutDuration));
            yield return new WaitForSecondsRealtime(0.4f);
        }

        SuperIdol.Stop();
        text.SetActive(true);
        ExitGame();
    }

    private IEnumerator Fading(Color targetColor, float duration)
    {
        Color startColor = textMeshPro.color;
        float startTime = Time.realtimeSinceStartup;
        float endTime = startTime + duration;
        while (Time.realtimeSinceStartup < endTime)
        {
            float num = Time.realtimeSinceStartup - startTime;
            textMeshPro.color = Color.Lerp(startColor, targetColor, num / duration);
            yield return null;
        }
        textMeshPro.color = targetColor;
    }

    private void Update()
    {
        if (!this.baldiAudio.isPlaying)
        {
            if (this.audioInQueue > 0 && !this.gc.spoopMode)
            {
                this.PlayQueue();
            }
            this.baldiFeed.SetBool("talking", false);
        }
        else
        {
            this.baldiFeed.SetBool("talking", true);
        }

        if ((Input.GetKeyDown("return") || Input.GetKeyDown("enter")) && this.questionInProgress)
        {
            this.questionInProgress = false;
            this.CheckAnswer();
        }

        if (this.problem > 3)
        {
            this.endDelay -= 1f * Time.unscaledDeltaTime;
            if (this.endDelay <= 0f)
            {
                GC.Collect();
                this.ExitGame();
            }
        }
    }

    private void NewProblem()
    {
        this.playerAnswer.text = string.Empty;
        this.problem++;
        this.playerAnswer.ActivateInputField();

        if (this.problem <= 3)
        {

        }
        else
        {
            this.endDelay = 5f;
            if (!this.gc.spoopMode)
            {
                this.questionText.text = "WOW! YOU EXIST!";
            }
            else if (this.gc.mode == "endless" && this.problemsWrong <= 0)
            {
                int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
                this.questionText.text = this.endlessHintText[num];
            }
            else if (this.gc.mode == "story" && this.problemsWrong >= 3)
            {
                this.questionText.text = "I HEAR MATH THAT BAD";
                this.questionText2.text = string.Empty;
                this.questionText3.text = string.Empty;
                if (this.baldiScript.isActiveAndEnabled)
                    this.baldiScript.Hear(this.playerPosition, 7f);
                this.gc.failedNotebooks++;
            }
            else
            {
                int num2 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
                this.questionText.text = this.hintText[num2];
                this.questionText2.text = string.Empty;
                this.questionText3.text = string.Empty;
            }
        }
    }

    public void OKButton() => this.CheckAnswer();

    public void CheckAnswer()
    {
        if (this.playerAnswer.text == "31718")
        {
            StartCoroutine(this.CheatText("THIS IS WHERE IT ALL BEGAN"));
            SceneManager.LoadSceneAsync("TestRoom");
        }
        else if (this.playerAnswer.text == "53045009")
        {
            StartCoroutine(this.CheatText("USE THESE TO STICK TO THE CEILING!"));
            this.gc.Fliparoo();
        }

        if (this.problem <= 3)
        {
            if (this.playerAnswer.text == this.solution.ToString() && !this.impossibleMode)
            {
                this.results[this.problem - 1].texture = this.correct;
                this.baldiAudio.Stop();
                this.ClearAudioQueue();
                int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 4f));
                this.QueueAudio(this.bal_praises[num]);
                this.NewProblem();
            }
            else
            {
                this.problemsWrong++;
                this.results[this.problem - 1].texture = this.incorrect;
                if (!this.gc.spoopMode)
                {
                    this.baldiFeed.SetTrigger("angry");
                    this.gc.ActivateSpoopMode();
                }

                if (this.gc.mode == "story")
                {
                    this.baldiScript.GetAngry(this.problem == 3 ? 1f : 0.25f);
                }
                else
                {
                    this.baldiScript.GetAngry(1f);
                }

                this.ClearAudioQueue();
                this.baldiAudio.Stop();
                this.NewProblem();
            }
        }
    }

    private void QueueAudio(AudioClip sound)
    {
        this.audioQueue[this.audioInQueue] = sound;
        this.audioInQueue++;
    }

    private void PlayQueue()
    {
        this.baldiAudio.PlayOneShot(this.audioQueue[0]);
        this.UnqueueAudio();
    }

    private void UnqueueAudio()
    {
        for (int i = 1; i < this.audioInQueue; i++)
        {
            this.audioQueue[i - 1] = this.audioQueue[i];
        }
        this.audioInQueue--;
    }

    private void ClearAudioQueue() => this.audioInQueue = 0;

    public void ExitGame(VideoPlayer vp)
    {
        ExitGame();
    }

    public void ExitGame()
    {
        if (this.gc.notebooks >= 2)
        {
            if (!this.gc.spoopMode)
            {
                this.gc.ActivateSpoopMode();
            }
            this.baldiScript.GetAngry(1f);
        }
        this.gc.DeactivateLearningGame(gameObject);
    }

    public void ButtonPress(int value)
    {
        if (value >= 0 && value <= 9)
        {
            this.playerAnswer.text += value;
        }
        else if (value == -1)
        {
            this.playerAnswer.text += "-";
        }
        else
        {
            this.playerAnswer.text = string.Empty;
        }
    }

    private IEnumerator CheatText(string text)
    {
        while (true)
        {
            this.questionText.text = text;
            this.questionText2.text = string.Empty;
            this.questionText3.text = string.Empty;
            yield return new WaitForEndOfFrame();
        }
    }
    public GameControllerScript gc;
    public BaldiScript baldiScript;
    public Vector3 playerPosition;
    public GameObject mathGame;
    public RawImage[] results = new RawImage[3];
    public Texture correct;
    public Texture incorrect;
    public TMP_InputField playerAnswer;
    public TMP_Text questionText;
    public TMP_Text questionText2;
    public TMP_Text questionText3;
    public Animator baldiFeed;
    public Transform baldiFeedTransform;
    public AudioClip bal_plus;
    public AudioClip bal_minus;
    public AudioClip bal_times;
    public AudioClip bal_divided;
    public AudioClip bal_equals;
    public AudioClip bal_howto;
    public AudioClip bal_intro;
    public AudioClip bal_screech;
    public AudioClip[] bal_numbers = new AudioClip[10];
    public AudioClip[] bal_praises = new AudioClip[5];
    public AudioClip[] bal_problems = new AudioClip[3];
    public Button firstButton;
    public float endDelay;
    public int problem;
    public int audioInQueue;
    public AudioClip[] audioQueue = new AudioClip[10];
    public bool questionInProgress;
    public TMP_Text textMeshPro;
    public string[] texts;
    public GameObject text;
    public GameObject YCTP;
    public AudioSource SuperIdol;
    public AudioSource baldiAudio;
    public bool impossibleMode;
    public float num1, num2, num3, solution;
    public int sign;
    public string[] endlessHintText;
    public string[] hintText;
    public int problemsWrong;
    public VideoPlayer videoPlayer;
    public GameObject Video;
    private AudioSource learnMusicAudio;
    public AudioSource balIntroAudio;
    public GameObject yctpMain;
    public GameObject yctpNull;

}
