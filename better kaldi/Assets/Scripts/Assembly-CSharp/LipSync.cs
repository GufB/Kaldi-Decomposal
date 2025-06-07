using UnityEngine;
using UnityEngine.UI;

public class LipSync : MonoBehaviour
{
    public bool useSprites; //Used for if you're using the sprites method for lip sync, if off than for animation
    public bool altSprites; //Bool that can be turned on from another script if you wish to make the synced sprites different, you can add more bools to depend on more sprites if you like
    public float volumeThreshold = 0.1f; //Change depending on how loud or quiet the audio might be if needed
    public string animName; //Put the name of the animation you are using for lip sync, once again you can change the sync sprites based on another script changing the name to become a different animation instead (NOTE: you must make sure the animation plays backwards instead of forwards, its something i dont know how to fix)
    public Sprite[] syncedSprites;
    public Sprite[] syncedAltSprites; //If you are making a script change the sprites to be set different from the previous sync sprites, you can add as many as you want
    public AudioSource audioDevice; //Audio that makes the script work
    private SpriteRenderer spriteRenderer;
    private Image image;
    private Animator animator;

    private void Start()
    {
        if (useSprites)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            image = GetComponent<Image>();
        }
        else
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (audioDevice.isPlaying)
        {
            int currentFrame = 0;
            float timer = 0f;
            float volumeLevel = GetVolumeLevel();
            if (useSprites)
            {
                currentFrame = Mathf.FloorToInt(volumeLevel * (syncedSprites.Length - 1));
                if (volumeLevel < volumeThreshold)
                {
                    currentFrame = 0;
                }
                timer += Time.deltaTime;
                if (timer >= 0.1f)
                {
                    currentFrame = (currentFrame + 1) % syncedSprites.Length;
                    timer = 0f;
                }
                if (spriteRenderer != null)
                {
                    if (!altSprites)
                        //Using regular sync sprites
                        spriteRenderer.sprite = syncedSprites[currentFrame];
                    else
                        //Depends on whether you are changing the sprite here
                        spriteRenderer.sprite = syncedAltSprites[currentFrame];
                }
                if (image != null)
                {
                    //Same thing
                    if (!altSprites)
                        image.sprite = syncedSprites[currentFrame];
                    else
                        image.sprite = syncedAltSprites[currentFrame];
                }
            }
            else
            {
                float normalizedTime = volumeLevel;
                if (volumeLevel < volumeThreshold)
                {
                    normalizedTime = 1 - volumeLevel;
                }
                normalizedTime = Mathf.Clamp01(normalizedTime);
                animator.Play(animName, -1, normalizedTime);
            }
        }
    }

    private float GetVolumeLevel()
    {
        float[] samples = new float[256];
        audioDevice.GetOutputData(samples, 0);
        float sum = 0f;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += Mathf.Abs(samples[i]);
        }
        float average = sum / samples.Length;
        float volumeLevel = Mathf.Clamp01(average / volumeThreshold);
        return volumeLevel;
    }
}