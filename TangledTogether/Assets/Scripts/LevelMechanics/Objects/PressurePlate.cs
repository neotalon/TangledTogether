using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject moveObject;
    public float moveDist;
    public float speedDown;
    public float speedUp;
    public float weightNeeded;
    private Vector3 startPos;
    private Vector3 newPos;
    [SerializeField] private float currentWeight;
    [SerializeField] private bool activate;
	public float test;

    // Start is called before the first frame update
    void Awake()
    {
        startPos = moveObject.transform.position;
        newPos = new Vector3(startPos.x, startPos.y + moveDist, startPos.z);
    }

	// Update is called once per frame
	void FixedUpdate()
    {
		if (currentWeight >= weightNeeded)
		{
			moveObject.transform.position = Vector3.MoveTowards(moveObject.transform.position, newPos, speedDown * Time.deltaTime);
			if (moveObject.transform.position == newPos)
			{
				activate = true;
			}
		}
		else
		{
			moveObject.transform.position = Vector3.MoveTowards(moveObject.transform.position, startPos, speedUp * Time.deltaTime);
			activate = false;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.relativeVelocity.y <= test)
			currentWeight += collision.rigidbody.mass;
	}

	private void OnCollisionExit(Collision collision)
	{
		//TODO: Continue here
		currentWeight -= collision.rigidbody.mass;
	}
}
