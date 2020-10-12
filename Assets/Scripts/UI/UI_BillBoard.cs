using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BillBoard : MonoBehaviour
{
	void Update()
	{
		transform.forward = Camera.main.transform.forward;
	}
}
