using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
	public bool closeDoorsOnEnter = false;
	[SerializeField] private string playerTag = "Player";
	[SerializeField] private List<GameObject> doorsToCloseOnEnter = new List<GameObject>();

	//[Header("Enemy kill track")]
	public bool trackEnemyKill = false;
	[SerializeField] private List<GameObject> enemiesToKill = new List<GameObject>();
	[SerializeField] private List<GameObject> doorsToOpenOnEnemyEnd = new List<GameObject>();
	public bool spawnRewardOnEnemyEnd = false;
	[SerializeField] private GameObject rewardToSpawnOnEnemyEnd = null;
	[SerializeField] private Transform killRewardSpawnMarker = null;

	//[Header("Puzzle track")]
	public int puzzlesToClear = 0;
	[SerializeField] private List<GameObject> doorsToOpenOnPuzzleEnd = new List<GameObject>();
	public bool spawnRewardOnPuzzleEnd = false;
	[SerializeField] private GameObject rewardToSpawnOnPuzzleEnd = null;
	[SerializeField] private Transform puzzleRewardSpawnMarker = null;

	[SerializeField] private AudioClip closeSFX = null;
	[SerializeField] private AudioClip openSFX = null;
	[SerializeField] private AudioSource audioSource = null;


	private int puzzleCompletion = 0;

	private bool hasClosed = false;
	private bool puzzleEnded = false;
	private bool fightEnded = false;

	[HideInInspector] public bool showEnter = true;
	[HideInInspector] public bool showKillTrack = true;
	[HideInInspector] public bool showPuzzleTrack = true;

	private void Awake ()
	{
		switch (trackEnemyKill)
		{
			case true when enemiesToKill.Count == 0:
			{
				Life[] mobs = GetComponentsInChildren<Life>();
				foreach(Life l in mobs)
				{
					enemiesToKill.Add(l.gameObject);
				}

				break;
			}
			case false when puzzlesToClear == 0:
				closeDoorsOnEnter = false;
				Debug.LogError("No Door opening condition set ! Set either Enemy kill tracking or puzzle completion to open doors ! Opening them now by default", gameObject);
				break;
		}

		if(audioSource == null)
		{
			audioSource = GetComponent<AudioSource>();
		}
	}

	public void ProgressPuzzle ()
	{
		Debug.Log("Progress Puzzle", gameObject);
		puzzleCompletion++;
		
		if (puzzleCompletion < puzzlesToClear || puzzleEnded) return;
		
		OpenDoors(doorsToOpenOnPuzzleEnd);
		
		if(spawnRewardOnPuzzleEnd)
		{
			if(rewardToSpawnOnPuzzleEnd != null)
			{
				GameObject g = Instantiate(rewardToSpawnOnPuzzleEnd, transform.position, Quaternion.identity);
				g.transform.parent = transform;
				g.transform.position = puzzleRewardSpawnMarker.position;
			}
		}
		puzzleEnded = true;
	}

	private void OpenDoors(List<GameObject> doors)
	{
		foreach (GameObject door in doors)
		{
			door.SetActive(!door.activeSelf);
		}

		if(openSFX && audioSource)
		{
			audioSource.PlayOneShot(openSFX);
		}
	}

	private void TrackEnemyKill()
	{
		if (!trackEnemyKill) return;
		
		for(int i = enemiesToKill.Count -1; i>=0; i--)
		{
			if(enemiesToKill[i] == null)
			{
				enemiesToKill.RemoveAt(i);
			}
		}

		if (enemiesToKill.Count != 0 || fightEnded) return;
		
		OpenDoors(doorsToOpenOnEnemyEnd);
		
		if (spawnRewardOnEnemyEnd)
		{
			if (rewardToSpawnOnEnemyEnd != null)
			{
				GameObject g = Instantiate(rewardToSpawnOnEnemyEnd, transform.position, Quaternion.identity);
				g.transform.parent = transform;
				g.transform.position = killRewardSpawnMarker.position;
			}
		}
		fightEnded = true;
	}

	private void Update ()
	{
		TrackEnemyKill();
	}

	private void OnTriggerEnter (Collider other)
	{
		if (!closeDoorsOnEnter || hasClosed) return;

		if (doorsToCloseOnEnter.Count <= 0) return;
		
		Rigidbody r = other.GetComponent<Rigidbody>();
		if(r == null)
		{
			r = other.GetComponentInParent<Rigidbody>();
		}

		if (!r.gameObject.CompareTag(playerTag)) return;
		
		foreach (GameObject door in doorsToCloseOnEnter)
		{
			hasClosed = true;
			door.SetActive(true);
		}

		if (closeSFX && audioSource)
		{
			audioSource.PlayOneShot(closeSFX);
		}
	}
}
