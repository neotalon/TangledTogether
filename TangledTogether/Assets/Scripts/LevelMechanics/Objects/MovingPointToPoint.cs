using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct CheckPoints{
    public Vector3 position;

    public CheckPoints(Vector3 position)
	{
        this.position = position;
	}
}

public class MovingPointToPoint : MonoBehaviour
{
    public List<Vector3> checkPoints;
    public Vector3 offsetPos;
    public float speed;
    public bool move;
    private Vector3 startPos;
    private Vector3 nextPos;
    private Vector3 currentPos;

    void Awake()
    {
        startPos = transform.position;
        nextPos = startPos + checkPoints[0];
        currentPos = startPos;
    }

    // Update is called once per frame
    void Update()
    {
		if (move)
		{
            if(currentPos == startPos)
			{
                gameObject.transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
                if(transform.position == nextPos)
				{
                    currentPos = nextPos;
                    move = false;
				}
			}
			else if(currentPos == nextPos)
			{
                gameObject.transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
                if (transform.position == startPos)
                {
                    currentPos = startPos;
                    move = false;
                }
            }
		}
    }
}
