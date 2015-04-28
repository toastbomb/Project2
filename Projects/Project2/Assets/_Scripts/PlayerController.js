#pragma strict

var speed : float = 6.0;
var jumpSpeed : float = 8.0;
var gravity : float = 20.0;

private var moveDirection : Vector3 = Vector3.zero;

function Update () 
{
	var controller : CharacterController = GetComponent.<CharacterController>();
	
	if(controller.isGrounded)
	{
		moveDirection = Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		moveDirection.Normalize();
		moveDirection *= speed;
		
		if(Input.GetButtonDown("Jump"))
		{
			moveDirection.y = jumpSpeed;
		}
	}
	
	moveDirection.y -= gravity * Time.deltaTime;
	controller.Move(moveDirection * Time.deltaTime);
}