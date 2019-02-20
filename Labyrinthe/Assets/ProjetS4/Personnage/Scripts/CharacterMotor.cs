using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour {

	/*
	 *Mémo animation Condition
	 * 1 = Walk
	 * 2 = Run
	 * 3 = Right Rotation
	 * 4 = Left Rotation
	 * 5 = Reculer
	*/

	//Animations du perso
	Animator animations;

	//Vitesse de déplacement
	public float walkSpeed;
	public float runSpeed;
	public float turnSpeed;

	//Input
	public string inputFront;
	public string inputBack;
	public string inputLeft;
	public string inputRight;

	public Vector3 jumpSpeed; //Hauteur du saut
	CapsuleCollider playerCollider;



	void Start () {
		animations = GetComponent<Animator>(); 
		playerCollider = gameObject.GetComponent<CapsuleCollider>();
	}

	bool isGrounded()
	{
		return Physics.CheckCapsule (playerCollider.bounds.center, new Vector3 (playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.1f, playerCollider.bounds.center.z), 0.6f);
	}

	void Update () {

		//Si le joueur avance
		if (Input.GetKey(inputFront) && !Input.GetKey(KeyCode.LeftShift)) 
		{
			//Joue l'animation de marche
			animations.SetInteger("condition", 1);
			transform.Translate(0, 0, walkSpeed * Time.deltaTime);
		}

		//Si le joueur arrête d'avancer
		if (Input.GetKeyUp(inputFront)) 
		{
			//Le personnage arrête de marcher
			animations.SetInteger("condition", 0);
			transform.Translate(0, 0, 0);
		}

		//Si le joueur cours
		if (Input.GetKey(inputFront) && Input.GetKey(KeyCode.LeftShift)) 
		{
			//Le personnage cours
			animations.SetInteger ("condition", 2);
			transform.Translate(0, 0, runSpeed * Time.deltaTime);
		}

		//Si le joueur cours plus
		if (!Input.GetKey(inputFront) && Input.GetKey(KeyCode.LeftShift)) 
		{
			//Le personnage cours
			animations.SetInteger ("condition", 0);
		}


		//Si le joueur recule
		if (Input.GetKey(inputBack) && !Input.GetKey(inputFront) && !Input.GetKey(inputRight) && !Input.GetKey(inputLeft)) 
		{
			//Le personnage recule
			animations.SetInteger ("condition", 5);
			transform.Translate(0, 0, -(walkSpeed / 2) * Time.deltaTime);

		}

		//Si le personnage arrête de reculer
		if (Input.GetKeyUp(inputBack) && !Input.GetKey(inputFront)) 
		{
			//Le personnage recule
			animations.SetInteger ("condition", 0);
		}

		//Rotation à gauche
		if (Input.GetKey (inputLeft) && !Input.GetKey (inputFront)) {
			//Animation du personnage(rotation)
			//animations.SetInteger ("condition", 4);
			transform.Rotate (0, -turnSpeed * Time.deltaTime, 0);
		} 
		else if (Input.GetKey(inputLeft))
			{
				transform.Rotate (0, -turnSpeed * Time.deltaTime, 0);
			}

		//Si on arrête de tourner à gauche
		if (Input.GetKeyUp(inputLeft)) 
		{
			//Le personnage arrête de marcher
			//sanimations.SetInteger("condition", 0);
		}

		//Rotation à droite
		if (Input.GetKey (inputRight) && !Input.GetKey (inputFront)) {
			//animations.SetInteger ("condition", 3);
			transform.Rotate (0, turnSpeed * Time.deltaTime, 0);
		} 
		else if (Input.GetKey(inputRight))
			{
				transform.Rotate (0, turnSpeed * Time.deltaTime, 0);
			}

		//Si on arrête de tourner à droite
		if (Input.GetKeyUp(inputRight)) 
		{
			//Le personnage arrête de pivoter
			//animations.SetInteger("condition", 0);
		}
	}
}