using UnityEngine;
using System.Collections;

public class CharacterAnimator : MonoBehaviour 
{
	Animator anim;
	int jumpHash = Animator.StringToHash("Jump");
	int moveHash = Animator.StringToHash("Speed");
	int landHash = Animator.StringToHash("Grounded");
	int enterHash = Animator.StringToHash("EnterBattle");
	int exitHash = Animator.StringToHash("ExitBattle");
	int slashHash = Animator.StringToHash("Slashing");
	int throwHash = Animator.StringToHash("Throwing");
	public GameObject mychar;
	public float rotationSpeed;
	bool inbattle;

	void Start () 
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if(Application.loadedLevelName != "MainMenu")
		{
			float move = Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical"));
			anim.SetFloat(moveHash, move);

			if(PlayerManager.player.playerState == PlayerManager.PlayerState.Walking)
			{
				Vector3 inp = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

				if(inp.magnitude > 0.1)
				{
					mychar.transform.rotation = Quaternion.Slerp(mychar.transform.rotation, Quaternion.LookRotation(inp), Time.deltaTime * rotationSpeed);
				}
			}

			if(Input.GetKeyDown(KeyCode.Space))
			{
				if(PlayerControllerMove.instance.characterController.isGrounded)
				{
					if(PlayerManager.player.playerState == PlayerManager.PlayerState.Walking)
					{
						anim.SetTrigger(jumpHash);
					}
				}
			}
			if(PlayerControllerMove.instance.characterController.isGrounded)
			{
				anim.SetBool(landHash, true);
			}
			else
			{
				anim.SetBool(landHash, false);
			}
		}

		if(PlayerManager.player.playerState == PlayerManager.PlayerState.Fighting)
		{
			anim.SetBool(slashHash, PlayerManager.player.slashing);
			anim.SetBool(throwHash, PlayerManager.player.throwing);

			if(!inbattle)
			{
				anim.SetTrigger(enterHash);
				inbattle = true;
			}
		}
		else
		{
			if((inbattle))
			{
				anim.SetTrigger(exitHash);
				inbattle = false;
			}
		}
	}
}
