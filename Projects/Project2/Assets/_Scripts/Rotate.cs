using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour 
{

	void Update() {
		transform.Rotate(Time.deltaTime * 8f, 0, 0);
		transform.Rotate(0, Time.deltaTime * 8f, 0, Space.World);
	}
}
