using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditor : Editor
{

	RoomManager myObject;
	SerializedObject soTarget;


	private SerializedProperty closeDoorsOnEnter;
	private SerializedProperty playerTag;
	private SerializedProperty doorsToCloseOnEnter;

	private SerializedProperty trackEnemyKill;
	private SerializedProperty enemiesToKill;
	private SerializedProperty doorsToOpenOnEnemyEnd;
	private SerializedProperty spawnRewardOnEnemyEnd;
	private SerializedProperty rewardToSpawnOnEnemyEnd;
	private SerializedProperty killRewardSpawnMarker;

	private SerializedProperty puzzlesToClear;
	private SerializedProperty doorsToOpenOnPuzzleEnd;
	private SerializedProperty spawnRewardOnPuzzleEnd;
	private SerializedProperty rewardToSpawnOnPuzzleEnd;
	private SerializedProperty puzzleRewardSpawnMarker;

	private SerializedProperty closeSFX;
	private SerializedProperty openSFX;
	private SerializedProperty source;

	private void OnEnable ()
	{

		myObject = (RoomManager)target;
		soTarget = new SerializedObject(target);


		////

		closeDoorsOnEnter = soTarget.FindProperty("closeDoorsOnEnter");
		playerTag = soTarget.FindProperty("playerTag");
		doorsToCloseOnEnter = soTarget.FindProperty("doorsToCloseOnEnter");

		trackEnemyKill = soTarget.FindProperty("trackEnemyKill");
		enemiesToKill = soTarget.FindProperty("enemiesToKill");
		doorsToOpenOnEnemyEnd = soTarget.FindProperty("doorsToOpenOnEnemyEnd");
		spawnRewardOnEnemyEnd = soTarget.FindProperty("spawnRewardOnEnemyEnd");
		rewardToSpawnOnEnemyEnd = soTarget.FindProperty("rewardToSpawnOnEnemyEnd");
		killRewardSpawnMarker = soTarget.FindProperty("killRewardSpawnMarker");

		puzzlesToClear = soTarget.FindProperty("puzzlesToClear");
		doorsToOpenOnPuzzleEnd = soTarget.FindProperty("doorsToOpenOnPuzzleEnd");
		spawnRewardOnPuzzleEnd = soTarget.FindProperty("spawnRewardOnPuzzleEnd");
		rewardToSpawnOnPuzzleEnd = soTarget.FindProperty("rewardToSpawnOnPuzzleEnd");
		puzzleRewardSpawnMarker = soTarget.FindProperty("puzzleRewardSpawnMarker");

		closeSFX = soTarget.FindProperty("closeSFX");
		openSFX = soTarget.FindProperty("openSFX");
		source = soTarget.FindProperty("source");

	}


	public override void OnInspectorGUI ()
	{

		UIHelper.InitializeStyles();
		
		soTarget.Update();
		EditorGUI.BeginChangeCheck();

		EditorGUILayout.BeginVertical(UIHelper.mainStyle);
		{
			EditorGUILayout.BeginHorizontal();
			{
				if (GUILayout.Button(" Enter ", UIHelper.headerStyle, GUILayout.MaxHeight(20f)))
				{
					myObject.showEnter = !myObject.showEnter;
				}

				if (GUILayout.Button(" + ", UIHelper.headerStyle, GUILayout.MaxWidth(20f)))
				{
					myObject.showEnter = true;
					myObject.showKillTrack = true;
					myObject.showPuzzleTrack = true;
				}

				if (GUILayout.Button(" - ", UIHelper.headerStyle, GUILayout.MaxWidth(20f)))
				{
					myObject.showEnter = false;
					myObject.showKillTrack = false;
					myObject.showPuzzleTrack = false;
				}
			}
			EditorGUILayout.EndHorizontal();

			if (myObject.showEnter)
			{
				EditorGUILayout.BeginVertical(UIHelper.subStyle1);
				{
					EditorGUILayout.PropertyField(closeDoorsOnEnter);

					if(myObject.closeDoorsOnEnter)
					{
						EditorGUILayout.BeginVertical(UIHelper.subStyle2);
						{
							EditorGUILayout.PropertyField(playerTag);
							EditorGUILayout.PropertyField(doorsToCloseOnEnter);

							EditorGUILayout.Space();

							EditorGUILayout.PropertyField(closeSFX);
							EditorGUILayout.PropertyField(openSFX);
						}
						EditorGUILayout.EndVertical();
					}

				}
				EditorGUILayout.EndVertical();
			}

			if (GUILayout.Button("Enemy Kill", UIHelper.headerStyle, GUILayout.MaxHeight(20f)))
			{
				myObject.showKillTrack = !myObject.showKillTrack;
			}

			if (myObject.showKillTrack)
			{
				EditorGUILayout.BeginVertical(UIHelper.subStyle1);
				{

					EditorGUILayout.PropertyField(trackEnemyKill);

					if(myObject.trackEnemyKill)
					{
						EditorGUILayout.PropertyField(enemiesToKill);

						if (myObject.closeDoorsOnEnter)
						{
							EditorGUILayout.PropertyField(doorsToOpenOnEnemyEnd);
						}
						EditorGUILayout.PropertyField(spawnRewardOnEnemyEnd);

						if (myObject.spawnRewardOnEnemyEnd)
						{
							EditorGUILayout.PropertyField(rewardToSpawnOnEnemyEnd);
							EditorGUILayout.PropertyField(killRewardSpawnMarker);
						}
					}
				}
				EditorGUILayout.EndVertical();
			}

			if (GUILayout.Button("Puzzle Completion", UIHelper.headerStyle, GUILayout.MaxHeight(20f)))
			{
				myObject.showPuzzleTrack = !myObject.showPuzzleTrack;
			}

			if (myObject.showPuzzleTrack)
			{
				EditorGUILayout.BeginVertical(UIHelper.subStyle1);
				{
					EditorGUILayout.PropertyField(puzzlesToClear);

					if (myObject.puzzlesToClear > 0)
					{
						if (myObject.closeDoorsOnEnter)
						{
							EditorGUILayout.PropertyField(doorsToOpenOnPuzzleEnd);
						}

						EditorGUILayout.PropertyField(spawnRewardOnPuzzleEnd);
						
						if (myObject.spawnRewardOnPuzzleEnd)
						{
							EditorGUILayout.PropertyField(rewardToSpawnOnPuzzleEnd);
							EditorGUILayout.PropertyField(puzzleRewardSpawnMarker);
						}
					}
				}
				EditorGUILayout.EndVertical();
			}
		}
		EditorGUILayout.EndVertical();

		if (EditorGUI.EndChangeCheck())
		{
			soTarget.ApplyModifiedProperties();
		}

		EditorGUILayout.Space();

	}
}