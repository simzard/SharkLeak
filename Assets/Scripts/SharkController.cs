using UnityEngine;
using System.Collections;

public class SharkController : MonoBehaviour {

	//public GameObject goal;

	public float SPEED = 2.5f;
	public float ROTSPEED = 2; 

	int NUM_GOALS;

	const int STATE_SWIM_AROUND = 0;
	const int STATE_ATTACK_BOAT = 1;
	const int STATE_VISIBLE_FOR_TARGET = 2;
	const int STATE_SHARK_AWAY = 3;

	float elapsedTime;

	int state;


	int currentIndex = 1;
	Transform[] goals; 

	// Use this for initialization
	void Start () {
		goals = GameObject.Find ("Goals").GetComponentsInChildren<Transform> (true);

		state = STATE_SWIM_AROUND;
		NUM_GOALS = goals.Length - 1;

		/*
		goals = new GameObject[NUM_GOALS];
		for (int i = 1; i < NUM_GOALS+1; i++) {
			goals[i-1] = GameObject.Find ("goal_" + i);
			print (goals[i-1].transform.position);
		}
		*/	
	}
	
	// Update is called once per frame
	void Update () {

		switch (state) {
		case STATE_SWIM_AROUND:
			print ("SWIM_AROUND " + currentIndex);

			Vector3 dir = getNextGoal().position - transform.position;

			Quaternion desiredRot = Quaternion.LookRotation(dir);

			Quaternion rot = Quaternion.RotateTowards (transform.rotation, desiredRot, ROTSPEED * Time.deltaTime);

			transform.rotation = rot;
			//gameObject.transform.LookAt (getNextGoal ());

			Vector3 pos = transform.position;

			Vector3 velocity = new Vector3 (0, 0, SPEED * Time.deltaTime);

			pos += transform.rotation * velocity;

			transform.position = pos;



			//transform.Translate (SPEED * Vector3.forward * Time.deltaTime);
			break;
		case STATE_ATTACK_BOAT:
			// just dissappear and make holes in the boat
			break;
		}

	}

	public Transform getNextGoal() {
		return goals [currentIndex];
	}
		
	public void OnTriggerEnter(Collider col) {
		//print ("BANG!");
		if (col.transform != goals [currentIndex]) {
			print ("I refuse to cooperate");
		} else 
			currentIndex = (currentIndex + 1);
		if (currentIndex == NUM_GOALS + 1)
			currentIndex = 1;

		print ("currentIndex: " + currentIndex);
	}


}
