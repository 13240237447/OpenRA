using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AmmoGui : MonoBehaviour {

	public WeaponListController playerWeaponList; //Reference to all weapons of player
	public Text ammo; //GUI text to show ammo of player
	
	private WeaponConf selectedWeapon; //currently selected weapon

	void OnGUI () {

		selectedWeapon = playerWeaponList.GetWeaponForStats(); //set selected weapon
		
		if (!selectedWeapon.infiniteAmmunition) {
			ammo.text = "" + selectedWeapon.currentAmmunition;
		} else {
			ammo.text = "∞";
		}

	}
	
}
