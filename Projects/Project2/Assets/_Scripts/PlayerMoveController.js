#pragma strict

var speed : float = 6.0;
var jumpSpeed : float = 8.0;
var gravity : float = 20.0;
var friction : float = 1.0;

private var moveDirection : Vector3 = Vector3.zero;
private var inputDirection : Vector2 = Vector2.zero;
private var hitHead : boolean = false;
private var pController : CharacterController;

private function InputUpdate()
{
	pController = GetComponent.<CharacterController>();
	inputDirection = Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	inputDirection.Normalize();
}

public function MoveUpdate () 
{
	InputUpdate();
	
	if(pController.isGrounded)
	{
		hitHead = false;
		moveDirection = Vector3(inputDirection.x, 0, inputDirection.y);
		moveDirection *= speed;
		
		if(Input.GetButton("Jump"))
		{
			moveDirection.y = jumpSpeed;
		}
	}
	else
	{
		if ((pController.collisionFlags & CollisionFlags.Above) != 0)
		{
			if(!hitHead)
			{
				moveDirection.y = 0;
			}
			hitHead = true;
		}
	}

	moveDirection.y -= gravity * Time.deltaTime;
	pController.Move(moveDirection * Time.deltaTime);
}