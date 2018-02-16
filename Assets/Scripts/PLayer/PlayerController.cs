using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Person {

    public Animator playerAnimation;
    public Rigidbody playerRB;
    public CharacterController playerCC;

    private PlayerInputs inputs;
    private float inputV;
    private float inputH;
    private bool run;
	private bool jump;
	private bool crouch;
	private float newRotation;

	void Awake()
    {
		if (!playerAnimation)
			playerAnimation = GetComponent<Animator>();
        if (!playerCC)
            playerCC = GetComponent<CharacterController>();
        
        if(!playerCC && !playerRB)
			playerRB = GetComponent<Rigidbody>();
        
		newRotation = transform.rotation.y;

		InitializeController ();
    }

	void InitializeController () {
		inputs = gameObject.AddComponent<PlayerInputs> ();
		inputs.InitializePlayerInputs (PlayerStats.playerID);
		inputs.VerticalAxisEvent += VerticalInputs;
		inputs.HorizontalAxisEvent += HorizontalInputs;
		inputs.RunEvent += RunInput;
		inputs.JumpEvent += JumpInput;
		inputs.CrouchEvent += CrouchInput;
	}

    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(0f, 0f,
			run ? 2f * PlayerStats.speed * inputV : PlayerStats.speed * inputV);
		if (jump) {
			jump = false;
			//moveDirection.y = PlayerStats.jumpSpeed;
			playerCC.Move (new Vector3 (0f, PlayerStats.jumpSpeed, 0f));
		}
		moveDirection = transform.TransformDirection (moveDirection);

        if(playerCC)
			playerCC.SimpleMove(moveDirection);
        else
            playerRB.AddForce (moveDirection, ForceMode.Impulse);

		newRotation += inputH * 3;
        transform.eulerAngles = new Vector3(0f, newRotation, 0f);
    }

    void VerticalInputs(float args){
        inputV = args;
		playerAnimation.SetFloat("InputV", args);
    }
	void HorizontalInputs(float args){
        inputH = args;
		playerAnimation.SetFloat("InputH", args);
	}
    void RunInput(bool args){
		if (run == args)
			return;
        run = args;
		playerAnimation.SetBool("Run", args);
    }
	void JumpInput (bool args) {
		if (playerCC && !playerCC.isGrounded)
			return;
		if (!args) {
			playerAnimation.SetBool ("Jump", args);
			return;
		}
		jump = true;
		playerAnimation.SetBool("Jump", args);
	}
	void CrouchInput (bool args) {
		if (crouch == args)
			return;
	}
}
