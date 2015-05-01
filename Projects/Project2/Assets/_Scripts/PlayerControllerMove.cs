using UnityEngine;
using System.Collections;

public class PlayerControllerMove : MonoBehaviour 
{
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
		characterController = gameObject.GetComponent<CharacterController> ();
	}
	
	public void UpdateMoveController () 
	{
		CheckMovement ();
		CheckInput ();
		UpdateMovement ();
	}
	
	void CheckMovement()
	{
		vertVel = moveVec.y;
		moveVec = Vector3.zero;
		Vector2 inputVec = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		
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
	}
	
	void UpdateMovement()
	{
		moveVec = transform.TransformDirection (moveVec);
		if(moveVec.magnitude > 1f)
		{
			moveVec = Vector3.Normalize(moveVec);
		}
		moveVec *= moveSpeed;
		moveVec = new Vector3(moveVec.x, vertVel, moveVec.z);
		ApplyGravity ();
		CheckCollision ();
		characterController.Move (moveVec * Time.deltaTime);
	}
	
	void CheckCollision()
	{
		if(!characterController.isGrounded)
		{
			if((characterController.collisionFlags & CollisionFlags.Above) != 0)
			{
				if(!hitHead)
				{
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
