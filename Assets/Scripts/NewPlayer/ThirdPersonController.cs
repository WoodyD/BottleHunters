using UnityEngine;
using System.Collections;

public enum CharacterState {
	Idle = 0,
	Walking = 1,
	Running = 2,
	Jumping = 3,
}

public class ThirdPersonController : MonoBehaviour {

	private Animator playerAnimation;
	private Joystick joystickMove;
	public CharacterState _characterState;

	// The speed when walking
	public float walkSpeed = 2.0f;
	// when pressing "Fire3" button (cmd) we start running
	public float runSpeed = 6.0f;

	public float inAirControlAcceleration = 3.0f;

	// How high do we jump when pressing jump and letting go immediately
	public float jumpHeight = 0.5f;

	// The gravity for the character
	public float gravity = 20.0f;
	// The gravity in controlled descent mode
	public float speedSmoothing = 10.0f;
	public float rotateSpeed = 500.0f;

	public bool canJump = false;

	private float jumpRepeatTime = 0.05f;
	private float jumpTimeout = 0.15f;
	private float groundedTimeout = 0.25f;

	// The camera doesnt start following the target immediately but waits for a split second to avoid too much waving around.
	private float lockCameraTimer = 0.0f;

	// The current move direction in x-z
	private Vector3 moveDirection = Vector3.zero;
	// The current vertical speed
	private float verticalSpeed = 0.0f;
	// The current x-z move speed
	private float moveSpeed = 0.0f;

	// The last collision flags returned from controller.Move
	private CollisionFlags collisionFlags;

	// Are we jumping? (Initiated with jump button and not grounded yet)
	private bool jumping = false;
	private bool jumpingReachedApex = false;

	// Are we moving backwards (This locks the camera to not do a 180 degree spin)
	private bool movingBack = false;
	// Is the user pressing any keys?
	private bool isMoving = false;
	// When did the user start walking (Used for going into trot after a while)
	private float walkTimeStart = 0.0f;
	// Last time the jump button was clicked down
	private float lastJumpButtonTime = -10.0f;
	// Last time we performed a jump
	private float lastJumpTime = -1.0f;
	// the height we jumped from (Used to determine for how long to apply extra jump power after jumping.)
	//private float lastJumpStartHeight = 0.0f;
	private Vector3 inAirVelocity = Vector3.zero;

	private float lastGroundedTime = 0.0f;
	public bool isControllable = true;
	private bool isJoystickEnable;
	private bool joystickRun;

	void Awake() {
		moveDirection = transform.TransformDirection(Vector3.forward);
		playerAnimation = GetComponent<Animator>();
		joystickMove = FindObjectOfType<Joystick>();
		isJoystickEnable = joystickMove && joystickMove.gameObject.activeInHierarchy;
	}

	private Vector3 lastPos;
	float backwardMoveTimer = 3f;

	void UpdateSmoothedMovementDirection() {
		Transform cameraTransform = Camera.main.transform;
		bool grounded = IsGrounded();

		// Forward vector relative to the camera along the x-z plane	
		Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;

		// Right vector relative to the camera
		// Always orthogonal to the forward vector
		Vector3 right = new Vector3(forward.z, 0, -forward.x);

		float v = GetVerticalInput();
		float h = GetHorizontalInput();

		// Are we moving backwards or looking backwards
		if (v < -0.2f) {
			movingBack = true;
		} else {
			movingBack = false;
		}
		//if (v < -0.2f && backwardMoveTimer < 0f) {
		//	backwardMoveTimer = 3f;
		//	movingBack = true;
		//} else {
		//	backwardMoveTimer -= Time.deltaTime;
		//	movingBack = false;
		//}
		
		
		
		Debug.Log("[ThirdPersonController] moving backward: " + movingBack + " timer: " + backwardMoveTimer);
		bool wasMoving = isMoving;
		isMoving = Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f;

		// Target direction relative to the camera
		Vector3 targetDirection = h * right + v * forward;

		// Grounded controls
		if (grounded) {
			// Lock camera for short period when transitioning moving & standing still
			lockCameraTimer += Time.deltaTime;
			if (isMoving != wasMoving)
				lockCameraTimer = 0.0f;

			if (targetDirection != Vector3.zero) {
				if (!movingBack) {
					moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
					moveDirection = moveDirection.normalized;
				} 
				//else{
				//	moveDirection += 180f * Vector3.forward;
				//	moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
				//	moveDirection = moveDirection.normalized;
				//}
			}

			// Smooth the speed based on the current target direction
			float curSmooth = speedSmoothing * Time.deltaTime;

			// Choose target speed
			//* We want to support analog input but make sure you cant walk faster diagonally than just forward or sideways
			float targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0f);

			_characterState = CharacterState.Idle;

			// Pick speed modifier
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || joystickRun) {
				targetSpeed *= runSpeed;
				_characterState = CharacterState.Running;
			} else {
				targetSpeed *= walkSpeed;
				_characterState = CharacterState.Walking;
			}

			moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, curSmooth);

			// Reset walk time start when we slow down
			if (moveSpeed < walkSpeed * 0.3f)
				walkTimeStart = Time.time;
		}
		// In air controls
		else {
			// Lock camera while in air
			if (jumping)
				lockCameraTimer = 0.0f;

			if (isMoving)
				inAirVelocity += targetDirection.normalized * Time.deltaTime * inAirControlAcceleration;
		}

	}

	private float GetVerticalInput() {
		float verticalInput;
		if (isJoystickEnable && Mathf.Abs(joystickMove.Vertical) > 0.1f) {
			verticalInput = joystickMove.Vertical;
			joystickRun = true;
		} else {
			verticalInput = Input.GetAxisRaw("Vertical");
			joystickRun = false;
		}
		return verticalInput;

	}
	private float GetHorizontalInput() {
		float horizontalInput;
		if (isJoystickEnable && Mathf.Abs(joystickMove.Horizontal) > 0.1f) {
			horizontalInput = joystickMove.Horizontal;
		} else {
			horizontalInput = Input.GetAxisRaw("Horizontal");
		}
		return horizontalInput;
	}


	void ApplyJumping() {
		// Prevent jumping too fast after each other
		if (lastJumpTime + jumpRepeatTime > Time.time)
			return;

		if (IsGrounded()) {
			// Jump
			// - Only when pressing the button down
			// - With a timeout so you can press the button slightly before landing		
			if (canJump && Time.time < lastJumpButtonTime + jumpTimeout) {
				verticalSpeed = CalculateJumpVerticalSpeed(jumpHeight);
				SendMessage("DidJump", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	void ApplyGravity() {
		if (isControllable) // don't move player at all if not controllable.
		{
			// Apply gravity
			//bool jumpButton = Input.GetButton("Jump");

			// When we reach the apex of the jump we send out a message
			if (jumping && !jumpingReachedApex && verticalSpeed <= 0.0f) {
				jumpingReachedApex = true;
				SendMessage("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
			}

			if (IsGrounded())
				verticalSpeed = 0.0f;
			else
				verticalSpeed -= gravity * Time.deltaTime;
		}
	}

	float CalculateJumpVerticalSpeed(float targetJumpHeight) {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * targetJumpHeight * gravity);
	}

	void DidJump() {
		jumping = true;
		jumpingReachedApex = false;
		lastJumpTime = Time.time;
		//lastJumpStartHeight = transform.position.y;
		lastJumpButtonTime = -10;

		_characterState = CharacterState.Jumping;
	}

	Vector3 velocity = Vector3.zero;

	void Update() {
		if (isControllable) {
			if (Input.GetButtonDown("Jump")) {
				lastJumpButtonTime = Time.time;
			}

			UpdateSmoothedMovementDirection();

			// Apply gravity
			// - extra power jump modifies gravity
			// - controlledDescent mode modifies gravity
			ApplyGravity();

			// Apply jumping logic
			ApplyJumping();

			// Calculate actual motion
			Vector3 movement = moveDirection * moveSpeed + new Vector3(0, verticalSpeed, 0) + inAirVelocity;
			movement *= Time.deltaTime;

			// Move the controller
			CharacterController controller = GetComponent<CharacterController>();
			collisionFlags = controller.Move(movement);
		}
		velocity = (transform.position - lastPos) * 25;

		if (playerAnimation) {
			float inputMove = Mathf.Clamp(velocity.sqrMagnitude, 0, 1);
			playerAnimation.SetBool("Jump", false);
			playerAnimation.SetBool("Run", false);
			playerAnimation.SetFloat("InputV", inputMove);

			if (_characterState == CharacterState.Jumping) {
				if (IsGrounded()) {
					//playerAnimation.SetBool("Jump", true);
				}
			} else if (_characterState == CharacterState.Running) {
				playerAnimation.SetBool("Run", true);
			}
		}

		// Set rotation to the move direction
		if (IsGrounded()) {
			transform.rotation = Quaternion.LookRotation(moveDirection);
		} else {
			/* This causes choppy behaviour when colliding with SIDES
             * Vector3 xzMove = velocity;
            xzMove.y = 0;
            if (xzMove.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(xzMove);
            }*/
		}

		// We are in jump mode but just became grounded
		if (IsGrounded()) {
			lastGroundedTime = Time.time;
			inAirVelocity = Vector3.zero;
			if (jumping) {
				jumping = false;
				SendMessage("DidLand", SendMessageOptions.DontRequireReceiver);
			}
		}

		lastPos = transform.position;
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		//	Debug.DrawRay(hit.point, hit.normal);
		if (hit.moveDirection.y > 0.01f)
			return;
	}

	public float GetSpeed() {
		return moveSpeed;
	}

	public bool IsJumping() {
		return jumping;
	}

	public bool IsGrounded() {
		return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
	}

	public Vector3 GetDirection() {
		return moveDirection;
	}

	public bool IsMovingBackwards() {
		return movingBack;
	}

	public float GetLockCameraTimer() {
		return lockCameraTimer;
	}

	public bool IsMoving() {
		return Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f;
	}

}