﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceController : MonoBehaviour {

	ChildColliderEnter collider;

	List<int> seq = new List<int> ();
	private int flag = 0;
	private List<int> grow_seq = new List<int> {1,2}; 
	private List<int> rot_seq = new List<int> {2,1}; 
	private bool firstFrame = true;

	AudioSource growSound;
	AudioSource rotSound;

	// Use this for initialization
	void Start () {
		AudioSource[] sources = GetComponents<AudioSource> ();
		growSound = sources [0];
		rotSound = sources [1];
	}
	
	// Update is called once per frame
	void Update () {
		collider = GameObject.Find ("RigidRoundHand/index/bone3").GetComponent<ChildColliderEnter>();
		if (collider) {
			seq = collider.touchSequence ();
			flag = collider.isTouching ();
		}

		if (seq.Count == 2) {
			bool isGrow = CompareList (seq, grow_seq);
			bool isRot = CompareList (seq, rot_seq);
			if (isGrow && firstFrame) {
				Debug.Log ("成長！");
				growSound.Play ();
				firstFrame = false;
			} else if (isRot && firstFrame) {
				Debug.Log ("腐敗！");
				rotSound.Play ();
				firstFrame = false;
			}
		}
	}

	bool CompareList(List<int> seq1, List<int> seq2) {
		bool result = true;
		for (int i = 0; i < seq1.Count; i++){

			if (seq1 [i] != seq2 [i]) {
				result = false;
				break;
			}
		}
		return result;
	}

}
