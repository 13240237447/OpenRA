using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

/*
	>>Description:
	
	This is the adapter for mobile input.
	Use it for Android and iOS.

*/

public class MobileInput : MonoBehaviour {

	public PlayerController playerController;
	public GameObject mobileInputCanvas;
	
	private bool isJump = false;
	private bool isShoot = false;
	private bool isMove = false;
	
	void Start () {
		
		mobileInputCanvas.SetActive (true);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//Cursor.visible = true; //show mouse cursor

		if (isJump && playerController.GetGrounded()) {
			playerController.GetComponent<Rigidbody>().velocity = new Vector3 (playerController.GetComponent<Rigidbody>().velocity.x,
																			   playerController.playerConf.speedJump,
																			   playerController.GetComponent<Rigidbody>().velocity.z);
			playerController.SetGrounded (false);
		}
		
		if (isShoot && Time.time > playerController.GetNextFire()) {
			if(playerController.GetSelectedWeapon().currentAmmunition > 0) {
				playerController.SetNextFire(Time.time + playerController.GetSelectedWeapon().fireRate);
				
				//Create projectile
				for(int i = 0; i < playerController.GetSelectedWeapon().projectile.projectileConf.amount; i++) {
					GameObject p = Object.Instantiate(playerController.GetSelectedWeapon().projectile.gameObject, playerController.transform.position, playerController.transform.rotation) as GameObject;
					p.transform.parent = GlobalVars.trash;
					p.GetComponent<ProjectileController>().Init(playerController.playerAim.position);
					if (!playerController.GetSelectedWeapon().infiniteAmmunition) {
						playerController.GetSelectedWeapon().currentAmmunition--;
					}
				}
				
				//SFX
				GlobalVars.AudioPlay(playerController.GetSelectedWeapon().sfxShot);
				
				//Execute aim animation
				playerController.playerWeaponList.WeaponShootAnimation();
			}
		}
	
	}
	
	public void Jump () {
		
		isJump = true;
		
	}

	public void JumpOff () {
		
		isJump = false;
		
	}

	public void Shoot () {
		
		isShoot = true;
		
	}

	public void ShootOff () {
		
		isShoot = false;
		
	}

	public void WeaponNext () {
		
		playerController.SetSelectedWeapon(playerController.playerWeaponList.SelectPreviousWeapon());
		//SFX
		GlobalVars.AudioPlay(playerController.GetSelectedWeapon().sfxSelected);
		
	}

	public void WeaponPrevious () {
		
		playerController.SetSelectedWeapon(playerController.playerWeaponList.SelectNextWeapon());
		//SFX
		GlobalVars.AudioPlay(playerController.GetSelectedWeapon().sfxSelected);
		
	}

	public void Move (BaseEventData data) {
		
		isMove = true;
		PointerEventData pointerData = data as PointerEventData;
		Vector3 heading = pointerData.position - pointerData.pressPosition;
		float distance = heading.magnitude;
		Vector3 direction = heading / distance; // This is now the normalized direction.
		
		//Move
		try {
			
			playerController.GetComponent<Rigidbody>().velocity = new Vector3 (direction.y * playerController.playerConf.speedVertical,
																			   playerController.GetComponent<Rigidbody>().velocity.y,
																			   -direction.x * playerController.playerConf.speedHorizontal);
																			   
		} catch (System.Exception e) {
			
			Debug.Log (e.ToString ());
			
		}
		
		//Aim
		float tilt = (distance > 100.0f) ? 100.0f : distance;
		tilt = (distance < 40.0f) ? 40.0f : distance;
		playerController.playerAim.localPosition = new Vector3 (direction.y * tilt * 0.05f, 
																0.25f, 
																-direction.x * tilt * 0.05f);
		
	}

	public void MoveOff () {
		
		if (isMove) {
			isMove = false;
			playerController.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
		
	}
}
