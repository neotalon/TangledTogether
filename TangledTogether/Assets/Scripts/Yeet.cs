using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yeet : MonoBehaviour
{
	private PlayerInput playerInput;
	private GameObject otherPlayer;

	public float viewRadius;
	[Range(0, 360)]
	public float viewAngle;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	private void Awake()
	{
		playerInput = gameObject.GetComponent<PlayerInputHolder>().playerInput;
	}

	void Update()
	{
		YeetOtherPlayer();
	}

	void YeetOtherPlayer()
	{
		if (Input.GetKey(playerInput.yeet))
		{
			Debug.Log("Prepare to yeet");
			CheckOtherPlayerPos();
		}
		else if (Input.GetKeyUp(playerInput.yeet))
		{
			Debug.Log("Yeet denied");
		}
	}

	void CheckOtherPlayerPos()
	{
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
		if(targetsInViewRadius.Length != 0)
		{
			otherPlayer = targetsInViewRadius[0].gameObject;
			Transform target = otherPlayer.transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{
				float dstToTarget = Vector3.Distance(transform.position, target.position);

				if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
				{
					Debug.Log("Y E E T");
				}
			}
		}
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}
