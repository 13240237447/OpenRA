using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrosshairGui : MonoBehaviour {

	public Camera playerCamera; //drop camera of player here
	public Transform playerAim; //drop aim-Transform of player here
	public Image crosshair;

	// Use this for initialization
	void Start () {
	
		Cursor.visible = false; //hide mouse cursor
	
	}
	
	void OnGUI () {

		crosshair.GetComponent<RectTransform>().anchorMin = playerCamera.WorldToViewportPoint(playerAim.position);
		crosshair.GetComponent<RectTransform>().anchorMax = playerCamera.WorldToViewportPoint(playerAim.position);

	}
	
}
