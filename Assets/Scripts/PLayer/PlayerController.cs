using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Person {

    public Animator playerAnimation;
    public CharacterController playerCC;
	public GameObject cameraHolder;
	public ParticleSystem burpParticle;
	public PhotonView photon;

	private PlayerInputs inputs;
    private float inputV;
    private float inputH;
    private bool run;
    private bool jump;
    private bool crouch;
    private float newRotation;

    void OnEnable() { 
		newRotation = transform.rotation.y;
		InitializeController ();
		SetPlayerCamera ();
    }

	void SetPlayerCamera () {
		GameSystemsController.Instance.mainGameCamera.transform.SetParent (cameraHolder.transform);
	}

	void InitializeController() {
        inputs = gameObject.AddComponent<PlayerInputs>();
        inputs.InitializePlayerInputs(PlayerStats.playerID);
        inputs.VerticalAxisEvent += VerticalInputs;
        inputs.HorizontalAxisEvent += HorizontalInputs;
        inputs.RunEvent += RunInput;
        inputs.JumpEvent += JumpInput;
        inputs.CrouchEvent += CrouchInput;
		inputs.BurpEvent += Burp;
    }


	void FixedUpdate () {
		if (photon.isMine) {
			MovePlayer ();
			RotatePlayer ();
		}
    }

	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			//We own this player: send the others our data 
			Debug.Log ("My Input");
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		} else {
			//Network player, receive data 
			Debug.Log ("Other player input");
			transform.position = (Vector3)stream.ReceiveNext();
			transform.rotation = (Quaternion)stream.ReceiveNext();
		}
	}

	void MovePlayer () {
		if (jump) {
			jump = false;
			//moveDirection.y = PlayerStats.jumpSpeed;
			playerCC.Move(new Vector3(0f, PlayerStats.jumpSpeed, 0f));
		}

		float speed = PlayerStats.speed * inputV;
		if (run) {
			speed *= 2;
		}

		Vector3 moveDirection = new Vector3 (0f, 0f, speed);
		moveDirection = transform.TransformDirection (moveDirection);
		playerCC.SimpleMove (moveDirection);
	}

	void RotatePlayer () {
		newRotation += inputH * 3;
		transform.eulerAngles = new Vector3 (0f, newRotation, 0f);
	}


    void VerticalInputs(float args) {
        inputV = args;
        playerAnimation.SetFloat("InputV", args);
    }
    void HorizontalInputs(float args) {
        inputH = args;
        playerAnimation.SetFloat("InputH", args);
    }
    void RunInput(bool args) {
        if (run == args)
            return;
        run = args;
        playerAnimation.SetBool("Run", args);
    }
    void JumpInput(bool args) {
        if (playerCC && !playerCC.isGrounded)
            return;
        if (!args) {
            playerAnimation.SetBool("Jump", args);
            return;
        }
        jump = true;
        playerAnimation.SetBool("Jump", args);
    }
    void CrouchInput(bool args) {
        if (crouch == args)
            return;
    }
	void Burp () {
		if (burpParticle && !burpParticle.isPlaying)
			burpParticle.Play ();
	}
}
