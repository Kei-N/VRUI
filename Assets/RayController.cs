using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RayController : MonoBehaviour {

	public GameObject  diveCamera;
	public GameObject buttonCollider;
	public GameObject ui;
	public Image buttonGauge;
	public int endPositionX = -4;
	public GameObject stage;
	public GameObject easyImageCollider;
	public GameObject normalImageCollider;
	public GameObject hardImageCollider;
	bool changeHard;
	bool changeNormal;
	bool changeEasy;
	float gaugeTime;
	Vector3 firstButtonGaugePosition;
	GameObject[] difficultyImages;

	void Start () {
		difficultyImages = GameObject.FindGameObjectsWithTag ("DifficultyImage");
		firstButtonGaugePosition = buttonGauge.rectTransform.localPosition;
	}

	void Update () {
		Ray ray = new Ray (diveCamera.transform.position, diveCamera.transform.forward);
		RaycastHit hit;



		if (Physics.Raycast (ray, out hit)) {
			Debug.DrawLine (ray.origin, hit.point, Color.black);

			if (hit.collider.gameObject.tag == "DifficultyCollider") {
				for (int i = 0; i < difficultyImages.Length; i++) { 
					difficultyImages [i].GetComponent<Image> ().color = Color.white;
				}
				hit.collider.gameObject.transform.parent.GetComponent<Image> ().color = Color.red;

				if (hit.collider.gameObject == hardImageCollider) {
					changeHard = true;
				} else {
					changeHard = false;
				}

				if (hit.collider.gameObject == normalImageCollider) {
					changeNormal = true;
				} else {
					changeNormal = false;
				}

				if (hit.collider.gameObject == easyImageCollider) {
					changeEasy = true;
				} else {
					changeEasy = false;
				}
			}

			if (hit.collider.gameObject == buttonCollider) {
				gaugeTime += Time.deltaTime * 0.01f;
				buttonGauge.rectTransform.localPosition = Vector3.Lerp (buttonGauge.rectTransform.localPosition, new Vector3 (0, 0, 1), gaugeTime);
			} else {
				gaugeTime = 0;
				buttonGauge.rectTransform.localPosition = firstButtonGaugePosition;
			}
			if (buttonGauge.rectTransform.localPosition.x > endPositionX) {
				ui.SetActive (false);
				if (changeHard) {
					stage.GetComponent<Renderer> ().material.color = Color.red;
				}
				if (changeNormal) {
					stage.GetComponent<Renderer> ().material.color = Color.yellow;
				}
				if (changeEasy) {
					stage.GetComponent<Renderer> ().material.color = Color.blue;
				}
			}
		}
	}
}