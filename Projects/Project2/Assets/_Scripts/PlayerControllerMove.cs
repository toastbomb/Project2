using UnityEngine;
using System.Collections;

public class PlayerControllerMove : MonoBehaviour 
{
	public static PlayerControllerMove instance;

	public float gravity = 21f;
	public float terminalVelocity = 20f;
	public float jumpSpeed = 6f;
	public float moveSpeed = 10f;
	
	public Vector3 moveVec{ get; set; }
	public float vertVel{ get; set; }
	
	public CharacterController characterController;
	
	public float deadZone = 0.1f;
	private bool hitHead = false;
	
	void Awake()
	{
		//get the character controler on this object
		characterController = gameObject.GetComponent<CharacterController> ();

		if(instance == null)
		{
			DontDestroyOnLoad (gameObject);
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
	}
	
	public void UpdateMoveController () 
	{
		if(Application.loadedLevelName != "MainMenu")
		{
			CheckMovement ();
			CheckInput ();
			UpdateMovement ();
		}
	}
	
	void CheckMovement()
	{
		vertVel = moveVec.y;
		moveVec = Vector3.zero;
		//Get input from user
		Vector2 inputVec = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		//Make sure that very small input don't result in movement
		if(inputVec.magnitude > deadZone)
		{
			moveVec += new Vector3(inputVec.x, 0, inputVec.y);
		}
	}
	
	void CheckInput()
	{
		if(Input.GetButton("Jump"))
		{
			Jump();
		}
	
		//Testing saving and loading
		if (Input.GetKeyDown(KeyCode.P)) {
			GameControl.control.Save();
		}
		if (Input.GetKeyDown(KeyCode.O)) {
			GameControl.control.Load ();
		}
	}
	
	void UpdateMovement()
	{
		//Makes sure the left is left and right is right etc. in reference to the camera
		moveVec = transform.TransformDirection (moveVec);
		//Forces magnitude to be a maximum of 1
		if(moveVec.magnitude > 1f)
		{
			moveVec = Vector3.Normalize(moveVec);
		}
		moveVec *= moveSpeed;
		moveVec = new Vector3(moveVec.x, vertVel, moveVec.z);
		ApplyGravity ();
		CheckCollision ();
		//Apply our move vector to the character
		characterController.Move (moveVec * Time.deltaTime);
	}
	
	void CheckCollision()
	{
		if(!characterController.isGrounded)
		{
			//If we touch something above us
			if((characterController.collisionFlags & CollisionFlags.Above) != 0)
			{
				//So we can only hit our head once per jump
				if(!hitHead)
				{
					//Stop our vertical velocity
					moveVec = new Vector3(moveVec.x, 0, moveVec.z);
					hitHead = true;
				}
			}
		}
		else
		{
			hitHead = false;
		}
	}
	
	void ApplyGravity()
	{
		//Make gravity happen if we haven't reached terminal velocity
		if(moveVec.y > -terminalVelocity)
		{
			moveVec = new Vector3(moveVec.x, moveVec.y - gravity * Time.deltaTime, moveVec.z);
		}
		if(characterController.isGrounded && moveVec.y < -1)
		{
			moveVec = new Vector3(moveVec.x, -1, moveVec.z);
		}
	}
	
	void Jump()
	{
		if(characterController.isGrounded)
		{
			vertVel = jumpSpeed;
		}
	}
}
