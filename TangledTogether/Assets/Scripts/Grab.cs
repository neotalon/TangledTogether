using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private PlayerInput playerInput;
    public float grabRange;

	private Rigidbody rb;
    private FixedJoint fixedJoint;
	private RaycastHit hit;
	private Ray castpoint;
	private bool grabbedObject;

	private void Awake()
	{
		playerInput = gameObject.GetComponent<PlayerInputHolder>().playerInput;
		rb = gameObject.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
    {
        GrabObject();
    }

    void GrabObject()
	{
		if (Input.GetKeyDown(playerInput.grab))
		{
            Debug.Log("Grab");
			Debug.DrawRay(transform.position, Vector3.forward, Color.magenta);
			if (!grabbedObject)
			{
				castpoint = new Ray(transform.position, Vector3.forward);
				if (Physics.Raycast(castpoint, out hit, grabRange))
				{
					fixedJoint = hit.collider.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
					fixedJoint.connectedBody = rb;
					grabbedObject = true;
				}
			}
		}
        else if (Input.GetKeyUp(playerInput.grab))
		{
            Debug.Log("Stop grab");
			Destroy(fixedJoint);
			grabbedObject = false;
		}
	}
}
