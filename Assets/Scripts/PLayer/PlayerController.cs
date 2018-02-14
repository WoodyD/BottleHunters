using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Person {

    public Animator playerAnimation;
    public Rigidbody playerRB;

    private PlayerInputs inputs;
    private float inputV;
    private float inputH;
    private bool run;
	private float newRotation;

    void Awake()
    {
		if (!playerAnimation)
			playerAnimation = GetComponent<Animator>();
		if (!playerRB)
			playerRB = GetComponent<Rigidbody>();
		newRotation = transform.rotation.y;

        inputs = gameObject.AddComponent<PlayerInputs>();
        inputs.InitializePlayerInputs(PlayerStats.playerID);
        inputs.VerticalAxisEvent += VerticalInputs;
        inputs.HorizontalAxisEvent += HorizontalInputs;
        inputs.RunEvent += RunInput;
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(0f, 0f, inputV);
        moveDirection = transform.TransformDirection(PlayerStats.speed * moveDirection);
        playerRB.velocity = run ? 2f * moveDirection : moveDirection;

        newRotation += inputH * PlayerStats.speed;
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
        run = args;
		playerAnimation.SetBool("Run", args);
    }

}
