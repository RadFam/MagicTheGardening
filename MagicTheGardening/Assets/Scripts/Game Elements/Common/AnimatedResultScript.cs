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

		Vector3 mySpriteInitPos;
		Vector3 mySpriteCurrPos;
        // Use this for initialization
        void Start()
        {
			signDown = -1.0f;
			signRot = 1.0f;
			deltaRot = 0.0f;
			deltaDown = 0.0f;
			mySpriteInitPos = new Vector3(0.0f, 0.0f, 0.0f);
        }

		public void OnEnable()
		{
			if (canAnimate)
			{
				mySpriteObject.SetActive(true);
				mySpriteObject.transform.localEulerAngles = new Vector3(-40.0f, 45.0f, 0.0f);
				mySpriteObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
			}
		}

		public void OnDisable()
		{
			if (canAnimate)
			{
				mySpriteObject.SetActive(false);
			}
		}

		public void SetResultSprite(Sprite spr)
		{
			mySprite = spr;
			mySpriteObject.SetActive(true);
			mySpriteObject.GetComponent<SpriteRenderer>().sprite = mySprite;

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
				}

				mySpriteCurrPos = new Vector3(mySpriteObject.transform.localPosition.x, mySpriteObject.transform.localPosition.y+signDown * speedDown * Time.deltaTime, mySpriteObject.transform.localPosition.z);
				
				mySpriteObject.transform.RotateAround(transform.localPosition, transform.up, signRot * speedRot * Time.deltaTime);
				mySpriteObject.transform.localPosition = Vector3.MoveTowards(mySpriteObject.transform.localPosition, mySpriteCurrPos, signDown * speedDown * Time.deltaTime);

				deltaRot += signRot * speedRot * Time.deltaTime;
				deltaDown += signDown * speedDown * Time.deltaTime;
			}
        }
    }

}