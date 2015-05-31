using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public ArrayList enemyList = new ArrayList();
	public int xp = 20;
	public int coins = 3;
	public int health = 2;
	public int dmg = 1;
	public int def = 0;
	public float spawn_max = 3.0f;
	public float spawn_min = 1.0f;

	public float alertDist = 5.0f;

	public CharacterController characterController;

	public enum EnemyState{Patrol, Alerted, Fighting, Dead, Running, Dashing, Cooldown};
	public EnemyState enemyState = EnemyState.Patrol;

	public Vector3 moveVec{ get; set; }
	public float vertVel{ get; set; }
	public float deadZone = 0.1f;
	private bool hitHead = false;
	public float gravity = 21f;
	public float terminalVelocity = 20f;
	public float jumpSpeed = 6f;
	public float dashSpeed = 10f;
	public float walkSpeed = 3f;
	private Vector2 dashVec = Vector2.zero;
	public float dashTime = 0.5f;
	public float cooldownTime = 1f;
	public float walkTime = 0.3f;
	public float stopTime = 1f;

	private Vector2 AIInput = Vector2.zero;

	void Awake()
	{
		//get the character controler on this object
		characterController = gameObject.GetComponent<CharacterController> ();
	}

	void Start()
	{
		RandomInput();
	}

	void RandomInput()
	{
		AIInput = Random.insideUnitCircle * 100;
		AIInput = Vector3.Normalize(AIInput);
		AIInput /= 3;
		Invoke("StopAI", walkTime);
	}

	void StopAI()
	{
		AIInput = Vector2.zero;
		Invoke("RandomInput", stopTime);
	}

	void Update()
	{
		CheckMovement ();
		CheckInput ();
		UpdateMovement ();
	}

	//Generate a list of size 1 to 5
	public void BuildEnemyList()
	{
		int rand = Mathf.FloorToInt(Random.Range(spawn_min, spawn_max));

		for(int i = 0; i < rand; i++)
		{
			enemyList.Add(GameObject.Instantiate(this));
		}
	}

	void CheckMovement()
	{
		vertVel = moveVec.y;
		moveVec = Vector3.zero;
		//Get input from user
		Vector2 inputVec = Vector2.zero;
		if(enemyState == EnemyState.Patrol)
		{
			inputVec = new Vector2 (AIInput.x, AIInput.y);
		}

		if(enemyState == EnemyState.Alerted)
		{
			if(characterController.isGrounded)
			{
				Vector3 dVec = PlayerManager.player.transform.position - this.transform.position;
				dashVec = new Vector2(dVec.x, dVec.z);
				enemyState = EnemyState.Dashing;
			}
		}

		if(enemyState == EnemyState.Dashing)
		{
			enemyState = EnemyState.Running;
			Invoke("StopDash", dashTime);
		}

		if(enemyState == EnemyState.Running)
		{
			inputVec = dashVec;
		}
		
		//Make sure that very small input don't result in movement
		if(inputVec.magnitude > deadZone)
		{
			moveVec += new Vector3(inputVec.x, 0, inputVec.y);
		}
	}

	void StopDash()
	{
		enemyState = EnemyState.Cooldown;
		Invoke("StartPatrol", cooldownTime);
	}

	void StartPatrol()
	{
		enemyState = EnemyState.Patrol;
	}
	
	void CheckInput()
	{
		if(Vector3.Distance(PlayerManager.player.transform.position, this.transform.position) < alertDist)
		{
			if(enemyState == EnemyState.Patrol)
			{
				Jump();
				enemyState = EnemyState.Alerted;
			}
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
		if(enemyState == EnemyState.Running)
		{
			moveVec *= dashSpeed;
		}
		else if(enemyState == EnemyState.Cooldown)
		{
			moveVec = Vector3.zero;
		}
		else
		{
			moveVec *= walkSpeed;
		}
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
