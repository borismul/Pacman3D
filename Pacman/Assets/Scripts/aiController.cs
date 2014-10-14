using UnityEngine;
using System.Collections;

public class aiController : MonoBehaviour {

	public GameObject player;
	public Playercontroller playercontroller;
	private bool seeMe;
	private RaycastHit hit;
	private Ray ray;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if (playercontroller.playing){
			ray = new Ray (transform.position, (player.transform.position - transform.position).normalized);
			if (Physics.Raycast (ray, out hit, Mathf.Infinity) && hit.transform.tag == "Player") {

				seeMe = true;

			} else {

				seeMe = false;

			}

			if (seeMe == true) {

				Vector3 replacement = Vector3.Lerp (transform.position, player.transform.position, 1f * Time.deltaTime);
				transform.position = new Vector3 (replacement.x, 3f, replacement.z);

			}
		}
	}
}
