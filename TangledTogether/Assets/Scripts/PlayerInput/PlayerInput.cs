﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInput : ScriptableObject
{
	public KeyCode left = KeyCode.A;
	public KeyCode right = KeyCode.D;
	public KeyCode up = KeyCode.W;
	public KeyCode down = KeyCode.S;
	public KeyCode jump = KeyCode.Space;
	public KeyCode grab = KeyCode.G;
	public KeyCode yeet = KeyCode.F;

	public bool disableMovement = false;
	public bool disableThrow = false;
}
