using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameBorder : MonoBehaviour{
	private Vector2 screenBounds;
	private float objectWidth;
	private float objectHeight;
	
	void Start(){
		screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
	}
	
	void BorderUpdate(){
		Vector3 vPos = transform.position;
		vPos.x = Mathf.Clamp(vPos.x, screenBounds.x + objectWidth, screenBounds.x * -1 - objectWidth);
		vPos.y = Mathf.Clamp(vPos.y, screenBounds.y + objectHeight, screenBounds.y * -1 - objectHeight);
		transform.position = vPos;
	}
}