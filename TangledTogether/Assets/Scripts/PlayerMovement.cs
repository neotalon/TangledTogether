using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Transform cam;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    public float speed;
    public float gravity = -9.82f;
    public float jumpHeight = 3f;
    public float turnSmoothTime;

    private bool isGrounded;
    private float turnSmoothVelocity;
    private Vector3 velocity;
	private Vector3 direction;

	[Header("Input")]
	public KeyCode left = KeyCode.A;
	public KeyCode right = KeyCode.D;
	public KeyCode up = KeyCode.W;
	public KeyCode down = KeyCode.S;
	public KeyCode jump = KeyCode.Space;

	private void Update()
	{
		if (Input.GetKey(left))
			direction += Vector3.left;
		if (Input.GetKey(right))
			direction += Vector3.right;
		if (Input.GetKey(up))
			direction += Vector3.forward;
		if (Input.GetKey(down))
			direction += Vector3.back;
		direction = direction.normalized;

		Jump();

		if (direction.magnitude > 0.1f && isGrounded)
		{
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);
			rb.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);
			//transform.position += direction * speed * Time.deltaTime;
			direction = Vector3.zero;
		}
		else if (direction.magnitude <= 0.1f && rb.velocity.x > 0.1f && rb.velocity.z > 0.1f)
		{
			rb.AddForce(-direction * speed * 10 * Time.deltaTime, ForceMode.Impulse);
			Debug.Log("move");
		}
		else
		{
			direction = Vector3.zero;
		}
	}

	void Jump()
	{
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		if(isGrounded && Input.GetKeyDown(jump))
		{
			rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
		}
		else if(!isGrounded)
		{
			rb.AddForce(gravity * Vector3.up);
		}
	}


	//   void Update()
	//   {
	//       float horizonal = Input.GetAxisRaw("Horizontal");
	//       float vertical = Input.GetAxisRaw("Vertical");
	//       Vector3 direction = new Vector3(horizonal, 0f, vertical).normalized;

	//       if (obiRope.CalculateLength() <= ropeLength)
	//       {
	//           if (direction.magnitude >= 0.1f)
	//           {
	//               Move(direction);
	//           }
	//       }
	//       else
	//	{
	//           float otherPlayerDirection = Vector3.Angle(gameObject.transform.right, otherPlayer.transform.position);
	//           float otherPlayerDistance = Vector3.Distance(gameObject.transform.position, otherPlayer.transform.position);
	//           //Debug.Log(otherPlayerDirection);

	//       }

	//       JumpAndFall();
	//   }

	//   void Move(Vector3 direction)
	//{
	//       float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
	//       float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
	//       transform.rotation = Quaternion.Euler(0f, angle, 0f);

	//       Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
	//       Debug.Log(moveDir.normalized * speed * Time.deltaTime);
	//       //rb.velocity = moveDir.normalized * speed * Time.deltaTime;
	//       characterController.Move(moveDir.normalized * speed * Time.deltaTime);
	//   }

	//void JumpAndFall()
	//{
	//	isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

	//	if (isGrounded && velocity.y < 0)
	//	{
	//		velocity.y = -2f;
	//	}

	//	if (isGrounded && Input.GetKeyDown(jump))
	//	{
	//		velocity.y = Mathf.Sqrt(jumpHeight * gravity * -2);
	//	}

	//	velocity.y += gravity * Time.deltaTime;
	//	characterController.Move(velocity * Time.deltaTime);
	//}
}
