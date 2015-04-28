#pragma strict

var speed : float = 6.0;
var jumpSpeed : float = 8.0;
var gravity : float = 20.0;

private var moveDirection : Vector3 = Vector3.zero;
private var hitHead : boolean = false;

public function MoveUpdate () 
{
	var controller : CharacterController = GetComponent.<CharacterController>();
	
	if(controller.isGrounded)
	{
		hitHead = false;
		moveDirection = Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		moveDirection.Normalize();
		moveDirection *= speed;
		
		if(Input.GetButton("Jump"))
		{
			moveDirection.y = jumpSpeed;
		}
	}
	else
	{
		if ((controller.collisionFlags & CollisionFlags.Above) != 0)
		{
			if(!hitHead)
			{
				moveDirection.y = 0;
			}
			hitHead = true;
		}
	}

	moveDirection.y -= gravity * Time.deltaTime;
	controller.Move(moveDirection * Time.deltaTime);
}