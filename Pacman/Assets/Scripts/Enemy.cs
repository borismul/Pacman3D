using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    // Variables
    public float speed = 20;
    public float rotateSpeed = 10;
    private int dIrection = -1;
    public float maxDistance = 5;
    public float directionDistance = 5;
    public float targetDistance = 5;
    public float followSpeed = 11;
    private Transform Target;
    private RaycastHit hit;
	private Playercontroller playercontroller;
    // End variables

	public void setTarget(Transform target)
   	{
       this.Target = target;
   	}

	public void setPlayerController(Playercontroller playercontroller)
	{
		this.playercontroller = playercontroller;
	}

    // Update is called once per frame
    void Update()
    {
if (playercontroller.playing == true) {
			animation.Play ();
						//Check Distance between current object and Target
						float dist = Vector3.Distance (Target.position, transform.position);

						if (Physics.Raycast (new Ray (transform.position, (Target.transform.position - transform.position).normalized), out hit, targetDistance) && hit.collider.tag == "Player") {
								transform.LookAt (new Vector3 (Target.position.x, this.transform.position.y, Target.position.z));
								transform.Translate (Vector3.forward * followSpeed * Time.smoothDeltaTime);
						}
						//Check if there is a collider in a certain distance of the object if not then do the following
						if (!Physics.Raycast (transform.position, transform.forward, maxDistance)) {
								// Move forward
								transform.Translate (Vector3.forward * speed * Time.smoothDeltaTime);
						} else {
								// If there is a object at the right side of the object then give a random direction
								if (Physics.Raycast (transform.position, transform.right, directionDistance)) {
										dIrection = Random.Range (-1, 2);
								}
								// If there is a object at the left side of the object then give a random direction
								if (Physics.Raycast (transform.position, -transform.right, directionDistance)) {
										dIrection = Random.Range (-1, 2);
								}
								// rotate 90 degrees in the random direction
								transform.Rotate (Vector3.up, 90 * rotateSpeed * Time.smoothDeltaTime * dIrection);
						}

						transform.rotation = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0);

		
		} else {
			animation.Stop();
		}

	} 
}