﻿using UnityEngine;

public class SwingingDoorScript : MonoBehaviour
{

	public GameControllerScript gc;

	public BaldiScript baldi;

	public MeshCollider barrier;

	public GameObject obstacle;

	public MeshCollider trigger;

	public MeshRenderer inside;

	public MeshRenderer outside;

	public Material closed;

	public Material open;

	public Material locked;

	public AudioClip doorOpen;

	public AudioClip baldiDoor;

	private float openTime;

	private float lockTime;

	public bool bDoorOpen;

	public bool bDoorLocked;

	private bool requirementMet;

	private AudioSource myAudio;

	public bool requirementSkip;

	private void Start()
	{
		myAudio = GetComponent<AudioSource>();
		bDoorLocked = true;
	}

	private void Update()
	{
		if ((!requirementMet & (gc.notebooks >= 2)) || requirementSkip)
		{
			requirementMet = true;
			UnlockDoor();
		}
		if (openTime > 0f)
		{
			openTime -= 1f * Time.deltaTime;
			if (requirementSkip)
			{
				inside.material = open;
				outside.material = open;
			}
		}
		if (lockTime > 0f)
		{
			lockTime -= Time.deltaTime;
		}
		else if (bDoorLocked & requirementMet)
		{
			UnlockDoor();
		}
		if ((openTime <= 0f) & bDoorOpen & !bDoorLocked)
		{
			bDoorOpen = false;
			inside.material = closed;
			outside.material = closed;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (!bDoorLocked)
		{
			bDoorOpen = true;
			inside.material = open;
			outside.material = open;
			openTime = 2f;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		_ = (gc.notebooks < 2) & !requirementSkip;
		if (!((gc.notebooks < 2) & (other.tag == "Player")) && !bDoorLocked && !bDoorOpen)
		{
			if (!gc.player.bootsActive)
			{
				myAudio.PlayOneShot(doorOpen, 0.5f);
			}
			else
			{
				myAudio.PlayOneShot(doorOpen, 1f);
			}
			if (other.tag == "Player" && baldi.isActiveAndEnabled)
			{
				if (!gc.player.bootsActive)
				{
					baldi.Hear(base.transform.position, 1f);
					myAudio.PlayOneShot(doorOpen, 1f);
				}
			}
		}
		if (other.tag == "NPC")
		{
			myAudio.PlayOneShot(doorOpen, 1f);
		}
		if (requirementSkip && !bDoorLocked && !bDoorOpen)
		{
			inside.material = open;
			outside.material = open;
			myAudio.PlayOneShot(doorOpen, 1f);
			if (other.tag == "Player" && baldi.isActiveAndEnabled)
			{
				baldi.Hear(base.transform.position, 1f);
			}
		}
	}

	public void LockDoor(float time)
	{
		barrier.enabled = true;
		obstacle.SetActive(value: true);
		bDoorLocked = true;
		lockTime = time;
		inside.material = locked;
		outside.material = locked;
	}

	private void UnlockDoor()
	{
		requirementMet = true;
		barrier.enabled = false;
		obstacle.SetActive(value: false);
		bDoorLocked = false;
		inside.material = closed;
		outside.material = closed;
	}
}
