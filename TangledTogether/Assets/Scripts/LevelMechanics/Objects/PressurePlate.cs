using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject moveObject;
    public float moveDist;
    public float speedDown;
    public float speedUp;
    public float neededWeight;

	public bool movePartial;

    private Vector3 startPos;
    private Vector3 newPos;
	private Vector3 tempPos;
    private float currentWeight;
    private bool activate;

    // Start is called before the first frame update
    void Awake()
    {
        startPos = moveObject.transform.position;
        newPos = new Vector3(startPos.x, startPos.y + moveDist, startPos.z);
    }

	// Update is called once per frame
	void FixedUpdate()
    {
		MoveBlockPartial();
		MoveBlock();
	}

	void MoveBlockPartial()
	{
		if (movePartial)
		{
			tempPos = new Vector3(startPos.x, startPos.y + moveDist * (currentWeight / neededWeight), startPos.z);
			moveObject.transform.position = Vector3.MoveTowards(moveObject.transform.position, tempPos, speedDown * Time.deltaTime);
			if (moveObject.transform.position == newPos)
			{
				activate = true;
			}
			else
			{
				activate = false;
			}
		}
	}

	void MoveBlock()
	{
		if (!movePartial)
		{
			if (currentWeight >= neededWeight)
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
	}

	private void OnTriggerEnter(Collider collision)
	{
		if(collision.GetComponent<Rigidbody>() != null)
			currentWeight += collision.gameObject.GetComponent<Rigidbody>().mass;
	}

	private void OnTriggerExit(Collider collision)
	{
		if (collision.GetComponent<Rigidbody>() != null)
			currentWeight -= collision.gameObject.GetComponent<Rigidbody>().mass;
	}
}
