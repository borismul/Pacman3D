using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;



#region PlayerController
public class Playercontroller : MonoBehaviour {

	#region variables
	// Public variables
	public GameObject FirstPersonCamera;
	public GameObject Canvases;
	public GameObject StartCanvas;
	public GameObject GameOptionsCanvas;
	public GameObject InGameOptionsCanvas;
	public GameObject GameOverMenu;
	public GameObject HUD;
	public GameObject WinMenuCanvas;
	public Text LivesText;
	public Text Score;
	public Text Highscore;
	public int spawnedCoins;
	public GameManager gameManager;
	public AudioSource startgeluid;
	public AudioSource coingeluid;
	public AudioSource doodgeluid;
	public GameObject topviewCamera;

	// Private variables
	private Vector3 Rotation = new Vector3 (0f, 0f, 0f);
	private float SpeedKeyboard=350f;
	private float SpeedMouse=100f;
	private Vector3 beginPos = new Vector3(3f,3f,3f);
	public int lives;
	private int score;
	public bool playing;

	#endregion

	#region StartMenu
	// void which is runned when the game is just started
	public void StartMenu() {

		// Display StartMenu
		StartCanvas.SetActive (true);

		// Disable GameOptionsCanvas
		GameOptionsCanvas.SetActive (false);
		GameOverMenu.SetActive (false);

		// Not playing so turn playing is set to false
		playing = false;

		// Pauze game
		EndPlaying ();

	}
	#endregion

	#region GameOptions
	// Void which is runned when the player wants the ingame menu
	public void GameOptions() {

		// Activate InGameOptionsCanvas
		GameOptionsCanvas.SetActive (true);

		// Disable StartCanvas
		StartCanvas.SetActive (false);


	}
	#endregion

	#region InGameOptionsMenu

	// Void for the InGameOptionsMenu
	public void InGameOptionsMenu() {

		// activate InGameOptionsCanvas
		InGameOptionsCanvas.SetActive (true);

		// Deactivate InGameMenu
		GameOptionsCanvas.SetActive (false);

	}
	#endregion
	
	#region GameoveMenu
	// Void which is runned when the player is gameover
	void GameoverMenu() {

		GameOverMenu.SetActive (true);

		// Pauze game
		EndPlaying ();

	}
	#endregion

	#region WinMenu
	public void WinMenu(){

		WinMenuCanvas.SetActive (true);

	}
	#endregion

	#region SetCoinList
	public void setCoinList(int coins){

		spawnedCoins = coins;
		
	}
	#endregion

	#region StartPlaying
	// Void which can be called when game starts playing
	public void StartPlaying(){

		// Hide the StartCanvas
		StartCanvas.SetActive (false);

		// The game is being playd so playing is true
		playing = true;
		
		// Hiding the mouse pointer
		Screen.showCursor = false;

		// Set Initial Camera Rotation
		transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f));

		transform.position = new Vector3 (3f, 3f, 3f);

		rigidbody.useGravity = true;

		lives = 3;

		LivesText.text = lives.ToString();

		score = 0;

	
		startgeluid.Play ();

	}
	#endregion

	#region ContinuePlaying
	public void ContinuePlaying(){

		StartCanvas.SetActive (false);
		GameOptionsCanvas.SetActive (false);
		Screen.showCursor = false;
		playing = true;
		rigidbody.useGravity = true;

		
	}
	#endregion

	#region EndPlaying
	// Void which can be called when there is a game menu
	void EndPlaying(){

		// The game is being playd so playing is true
		playing = false;

		// Hiding the mouse pointer
		Screen.showCursor = true;

		rigidbody.useGravity = false;

		
	}
	#endregion

	#region QuitGame
	// Method for quitting the game
	public void QuitGame(){

		// Quit the game
		Application.Quit();

	}
	#endregion

	#region PlayerMovement
	// Method for determining the Camera Movement
	private Vector3 PlayerMovement() {

		// initializing jumping that will decide whether the player is able to jump
		int jumping;

		// Inputs form thekeyboard
		float MovementVertical = SpeedKeyboard * Input.GetAxisRaw ("Vertical") * Time.deltaTime;
		float MovementHorizontal = SpeedKeyboard * Input.GetAxisRaw ("Horizontal") * Time.deltaTime;
	
		// if space is pressed and the object is on the ground within the diameter of 0.55 a jump can be performed
		if (Input.GetKeyDown (KeyCode.Space) && (Physics.Raycast (FirstPersonCamera.transform.position+new Vector3(0.55f,0f,0f),-Vector3.up,3f) ||Physics.Raycast (FirstPersonCamera.transform.position+new Vector3(-0.55f,0f,0f),-Vector3.up,3f)||Physics.Raycast (FirstPersonCamera.transform.position+new Vector3(0f,0f,0.55f),-Vector3.up,3f)||Physics.Raycast (FirstPersonCamera.transform.position+new Vector3(0f,0f,-0.55f),-Vector3.up,3f))) {

			jumping = 1;

		} else {

			jumping = 0;

		}

		// Finding out the angles of the camera w.r.t. the origin axes
		Vector3 CamAngles = FirstPersonCamera.transform.rotation.eulerAngles;

		// Determining the movement
		Vector3 Movement=new Vector3((Mathf.Sin (CamAngles.y / 180 * Mathf.PI) * MovementVertical + Mathf.Cos (CamAngles.y / 180 * Mathf.PI) * MovementHorizontal),jumping*7f, (Mathf.Cos (CamAngles.y / 180 * Mathf.PI) * MovementVertical - Mathf.Sin (CamAngles.y / 180 * Mathf.PI) * MovementHorizontal));

		// Return the movement
		return Movement;
	}
	#endregion
	
	#region CameraRotation
	// Method for determining the camera rotation
	private Quaternion CameraRotation() {

		// Getting the mouse input and camera angle
		float RotationVertical = SpeedMouse * Input.GetAxisRaw ("Mouse X") * Time.deltaTime;
		float RotationHorizontal = SpeedMouse * Input.GetAxisRaw ("Mouse Y") * Time.deltaTime;

		// summing the rotation with the new rotation ordered by the mouse changes and transform the camera angles with this
		Rotation = Rotation + new Vector3 (-RotationHorizontal, RotationVertical, 0f);

		// limiting the movement of the camera
		Rotation.x = Mathf.Clamp (Rotation.x, -90f, 90f);

		return Quaternion.Euler ((Rotation));

	}
	#endregion

	#region Reset
	public void Reset(){

		transform.position = beginPos;
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;


	}
	#endregion

	#region HardReset
	public void HardReset(){
		
		transform.position = beginPos;
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;

		foreach(Coin coin in gameManager.coinSpawner.spawnedCoins)
		{
			coin.gameObject.SetActiveRecursively(true);
		}
	}
	#endregion

	#region Start
	// Use this for initialization
	void Start () {

		rigidbody.position = beginPos;
		Vector3 topviewstart = new Vector3 (transform.position.x, 100f, transform.position.z);
		topviewCamera.transform.position = topviewstart;


		// Run StartMenu Method
		StartMenu ();
	}
	#endregion

	#region OnTriggerEnter
	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag == "Coin") {

			score = score + 10;
			coingeluid.Play();
			other.gameObject.SetActive (false);
			spawnedCoins--;

			if (spawnedCoins == 0) {
			
					WinMenu ();
			}
		}
	}
	#endregion

	#region OnCollisionEnter
	public void OnCollisionEnter(Collision other){

		if (other.gameObject.tag == "Enemy") {

			Reset ();
			lives--;
			LivesText.text = lives.ToString();
			doodgeluid.Play ();

			if (lives == 0){


				GameoverMenu();

			}

		}



	}


	#endregion

	#region Update
	// Update is called once per frame
	void Update () {

		// This checkes whether the options canvas just became activated
		bool GameoptionOnThisFrame = false;

		// if escape is pressed gameoptions menu is displayed
		if (Input.GetKeyDown (KeyCode.Escape) && !GameOptionsCanvas.activeSelf && playing){
			
			GameOptions();
			EndPlaying();

			GameoptionOnThisFrame=true;

		}

		if(PlayerPrefs.GetInt("HScore") < score)
			PlayerPrefs.SetInt("HScore", score);
		
		Highscore.text = PlayerPrefs.GetInt("HScore").ToString();
		Score.text = score.ToString();

		// If escape is pressed and Gameoptions active and is not just turned on it will turn it off
		if(Input.GetKeyDown (KeyCode.Escape) && GameOptionsCanvas.activeSelf && !GameoptionOnThisFrame){

			ContinuePlaying();

		}


			   
	}
	#endregion

	#region FixedUpdate
	void FixedUpdate(){


		// When the game is being played this script is runned
		if (playing) {


			// moving the player in the direction of the camera and setting the angular velocity to zero
			rigidbody.velocity = PlayerMovement ()+ new Vector3 (0f, rigidbody.velocity.y, 0f);
			rigidbody.angularVelocity = new Vector3 (0f, 0f, 0f);

			if (rigidbody.velocity.y>7f){

				rigidbody.velocity=new Vector3(0f,7f,0f);

			}
		}

		else {

			rigidbody.velocity = new Vector3(0f,0f,0f);
		}



	}
	#endregion

	#region LateUpdate
	// Update is calles every frame after all physics calculations are done
	void LateUpdate () {

		transform.rotation = Quaternion.Euler (0f, 0f, 0f);


		// When the game is being played this script is runned
		if (playing) {
			FirstPersonCamera.transform.rotation = CameraRotation ();
		}

	}
	#endregion

}
#endregion