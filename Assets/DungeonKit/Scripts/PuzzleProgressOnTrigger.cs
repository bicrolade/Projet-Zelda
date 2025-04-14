using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PuzzleProgressOnTrigger : MonoBehaviour
{
	[Header("Collision")]
	[SerializeField] bool useTag = true;
	[SerializeField] string tagName = "Case Sensitive";

	[Header("Ref")]
	[SerializeField] RoomManager roomManager;
	[SerializeField] AudioClip triggerSFX = null;
	[SerializeField] AudioSource audioSource;

	bool hasTriggered = false;

	private void Awake ()
	{
		if(roomManager == null)
		{
			roomManager = GetComponentInParent<RoomManager>();
		}

		if(audioSource == null)
		{
			audioSource = GetComponent<AudioSource>();
		}
	}

	private void OnTriggerEnter (Collider other)
	{
		if(!hasTriggered)
		{
			if(useTag)
			{
				Rigidbody rigid = other.GetComponent<Rigidbody>();
				if(rigid == null)
				{
					rigid = other.GetComponentInParent<Rigidbody>();
				}
				string tagCheck = rigid.gameObject.tag;

				if(tagName == tagCheck)
				{
					hasTriggered = true;
					roomManager.ProgressPuzzle();
					if(audioSource && triggerSFX)
					{
						audioSource.PlayOneShot(triggerSFX);
					}
				}
			}
			else
			{
				hasTriggered = true;
				roomManager.ProgressPuzzle();
			}
		}
	}
}
