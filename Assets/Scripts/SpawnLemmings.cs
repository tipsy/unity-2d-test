﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpawnLemmings : MonoBehaviour {

	public Transform singupform;
	public Transform singupformDeath;

	public Transform verifyPin;
	public Transform verifyPinDeath;

	public Transform confirmationPage;
	public Transform confirmationPageDeath;

	public Transform redirected;
	public Transform redirectedCastle;

	public Transform spawnLemmings;
	List<Transform> lemmingList = new List<Transform>();
	
	void Start () {
		MakeALemmingSpawn ();
	}

	int state_increment=300;

	int frame = 0;
	void Update () {
		frame++;
		if (frame % 10 == 0) {
			MakeALemmingSpawn ();
		}

		CountAndUpdateLabels ();

		if (frame > state_increment*5) {
			singupform.GetComponent<TextMesh> ().text = "LOL";
			singupformDeath.GetComponent<TextMesh> ().text = "LOL";
			
			verifyPin.GetComponent<TextMesh> ().text = "LOL";
			verifyPinDeath.GetComponent<TextMesh> ().text = "LOL";
			
			confirmationPage.GetComponent<TextMesh> ().text = "LOL";
			confirmationPageDeath.GetComponent<TextMesh> ().text = "LOL";
			
			redirected.GetComponent<TextMesh> ().text = "LOL";
			redirectedCastle.GetComponent<TextMesh> ().text = "LOL";
		} else {
			CountAndUpdateLabels ();
		}

		if (frame > state_increment*4) {
			for (int i=0; i<2; i++) {
				MakeALemmingSpawn ();
			}
		}

		if (frame % 30 != 0) {
			return;
		}
		
		var n = Random.Range(0, 3);
		switch (n) {
		case 0:
			MakeALemmingJump ();
			break;
		case 1:
			MakeALemmingDie ();
			break;
		case 2:
			MakeALemmingSpawn ();
			break;
		}
		
		if (frame > state_increment*3) {
			for (var i=0; i<lemmingList.Count; i++) {
				if (lemmingList[i] != null) {
					lemmingList[i].GetComponent<Move>().aboutToJump = true;
				}
			}
		}
		
		if (frame > state_increment*4) {
			for (int i=0; i<lemmingList.Count; i++) {
				if (lemmingList[i] != null) {
					lemmingList[i].GetComponent<Move>().movementSpeed*=1.9f;
				}
			}
		}
	}
	
	void MakeALemmingJump() {
		var ind = randomLemmingIndex();
		if (lemmingList [ind] != null) {
			lemmingList [ind].GetComponent<Move> ().aboutToJump = true;
		}
	}
	
	void MakeALemmingDie() {
		var ind = randomLemmingIndex();
		if (lemmingList [ind] != null) {
			lemmingList [ind].GetComponent<Move> ().aboutToDie = true;
		}
	}
	
	void MakeALemmingSpawn() {
		Transform lemming = Instantiate (spawnLemmings, new Vector3 (Random.Range (-5.0f, -7.4f), 0.733f, Random.Range(-0.5f, 0f)), spawnLemmings.rotation) as Transform;
		if (lemming.rotation.y > 0) {
			lemming.Rotate (new Vector3 (0, 180, 0));
			lemming.GetComponent<Move> ().movementSpeed *= -1.0f;
		} 
		lemming.GetComponent<Move> ().movementSpeed *= 3.0f;
		if (Random.value > 0.6) {
			lemming.GetComponent<Move> ().aboutToJump=true;
		}
		lemmingList.Add(lemming);
	}
	
	int randomLemmingIndex() {
		return Random.Range(0, lemmingList.Count);
	}

	void CountAndUpdateLabels() {
		int singupFormCount = 0;
		int verifyPinCount = 0;
		int confirmationPageCount = 0;
		int redirectedPageCount = 0;

		for (var i=0; i<lemmingList.Count; i++) {
			if (lemmingList[i] != null) {
				switch (lemmingList[i].GetComponent<Move>().currentStep) {
				case 0:
					singupFormCount++;
					break;
				case 1:
					verifyPinCount++;
					break;
				case 2:
					confirmationPageCount++;
					break;
				case 3:
					redirectedPageCount++;
					break;
				}
			}
		}

		singupform.GetComponent<TextMesh> ().text = singupFormCount + " users";
		verifyPin.GetComponent<TextMesh> ().text = verifyPinCount + " users";
		confirmationPage.GetComponent<TextMesh> ().text = confirmationPageCount + " users";
		redirected.GetComponent<TextMesh> ().text = redirectedPageCount + " users";
		redirectedCastle.GetComponent<TextMesh> ().text = "" + redirectedPageCount;
	}
	
}