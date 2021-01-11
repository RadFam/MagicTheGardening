using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PlayerController
{
    public class SmallMapPlayerMoveController : MonoBehaviour
    {

		NavMeshAgent myNavMesh;
		Vector3 target;
		bool hasMoveTarget = false;

		[SerializeField]
		float movingSpeed;

        // Use this for initialization
        void Start()
        {
			myNavMesh = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {

        }

		public void SetMovingSpeed(float speed)
		{
			movingSpeed = speed;
			myNavMesh.speed = movingSpeed;
		}

		public void MoveToAction(Vector3 dest)
		{
			target = dest;
			myNavMesh.destination = dest;
			myNavMesh.isStopped = false;
			hasMoveTarget = true;
		}

		public void StopMoveAction()
		{
			myNavMesh.isStopped = true;
			target = gameObject.transform.position;
			hasMoveTarget = false;
		}

		public void PauseMoveAction()
		{
			if (hasMoveTarget)
			{
				myNavMesh.isStopped = true;
			}
		}

		public void RestoreMoveAction()
		{
			if (hasMoveTarget)
			{
				myNavMesh.isStopped = false;
			}
		}
    }
}