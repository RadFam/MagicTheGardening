using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameElement
{
    public class AnimatedResultScript : MonoBehaviour
    {
		bool canAnimate;
		Sprite mySprite;
		GameObject mySpriteObject;

		float speedRot;
		float maxDeltaRot;
		float speedDown;
		float maxDeltaDown;
		float signRot;
		float signDown;
		float deltaRot;
		float deltaDown;

		float stepRot;
		float stepDown;

		Vector3 mySpriteInitPos;
		Quaternion mySpriteInitRot;
		Vector3 mySpriteCurrPos;
        // Use this for initialization
        void Awake()
        {
			signDown = -1.0f;
			signRot = 1.0f;
			deltaRot = 0.0f;
			deltaDown = 0.0f;
			maxDeltaRot = 45.0f;
			maxDeltaDown = 1.0f;
			speedRot = 50.0f;
			speedDown = 1.0f;
			stepRot = 0.0f;
			stepDown = 0.0f;
			//mySpriteInitPos = new Vector3(0.0f, 0.0f, 0.0f);
        }

		public void OnEnable()
		{
			if (canAnimate)
			{
				mySpriteObject = transform.GetChild(0).gameObject;

				mySpriteObject.SetActive(true);
				mySpriteObject.transform.localEulerAngles = new Vector3(-40.0f, 45.0f, 0.0f);
				mySpriteObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

				mySpriteInitPos = mySpriteObject.transform.position;
				mySpriteInitRot = mySpriteObject.transform.rotation;

				stepRot = 0.0f;
				stepDown = 0.0f;
				mySpriteCurrPos = new Vector3(mySpriteObject.transform.position.x, mySpriteObject.transform.position.y + signDown * maxDeltaDown, mySpriteObject.transform.position.z);
			}
		}

		public void OnDisable()
		{
			if (canAnimate)
			{
				mySpriteObject.transform.position = mySpriteInitPos;
				mySpriteObject.transform.rotation = mySpriteInitRot;
				mySpriteObject.SetActive(false);
			}
		}

		public void SetResultSprite(Sprite spr)
		{
			mySpriteObject = transform.GetChild(0).gameObject;
			mySprite = spr;
			mySpriteObject.SetActive(true);
			mySpriteObject.GetComponent<SpriteRenderer>().sprite = mySprite;
			mySpriteInitPos = mySpriteObject.transform.position;
			mySpriteInitRot = mySpriteObject.transform.rotation;

			stepRot = 0.0f;
			stepDown = 0.0f;
			mySpriteCurrPos = new Vector3(mySpriteObject.transform.position.x, mySpriteObject.transform.position.y + signDown * maxDeltaDown, mySpriteObject.transform.position.z);
			Debug.Log("mySpriteCurrPos: " + mySpriteCurrPos);

			canAnimate = true;
		}

		public void RemoveResult()
		{
			canAnimate = false;

			mySprite = null;
			mySpriteObject.GetComponent<SpriteRenderer>().sprite = mySprite;
			mySpriteObject.SetActive(false);

			gameObject.SetActive(false);
		}

        // Update is called once per frame
        void Update()
        {
			// Animation will be here
			if (canAnimate)
			{
				if (Mathf.Abs(deltaRot) >= maxDeltaRot)
				{
					signRot *= -1.0f;
				}
				if (Mathf.Abs(deltaDown) >= maxDeltaDown)
				{
					signDown *= -1.0f;
					mySpriteCurrPos = new Vector3(mySpriteObject.transform.position.x, mySpriteObject.transform.position.y + 2 * signDown * maxDeltaDown, mySpriteObject.transform.position.z);
					deltaDown = 0;
					Debug.Log("mySpriteCurrPos: " + mySpriteCurrPos);
				}

				stepRot = signRot * speedRot * Time.deltaTime;
				stepDown = speedDown * Time.deltaTime;

				mySpriteObject.transform.RotateAround(mySpriteObject.transform.position, transform.up, stepRot);
				mySpriteObject.transform.position = Vector3.MoveTowards(mySpriteObject.transform.position, mySpriteCurrPos, stepDown);
				Debug.Log("mySpriteObject.transform.position: " + mySpriteObject.transform.position);

				deltaRot += stepRot;
				deltaDown += stepDown;
			}
        }
    }

}