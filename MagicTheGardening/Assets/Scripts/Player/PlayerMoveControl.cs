using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameControllers;

namespace PlayerController
{
    public class PlayerMoveControl : MonoBehaviour
    {
        [SerializeField]
        Transform target;

        NavMeshAgent navMesh;

        void Start()
        {
            navMesh = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        public void SetStoppingDistance(float dist)
        {
            navMesh.stoppingDistance = dist;
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<PlayerInteractController>().Cancel();
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMesh.destination = destination;
            navMesh.isStopped = false;
        }

        public void Stop()
        {
            navMesh.destination = gameObject.transform.position;
            navMesh.isStopped = true;
        }

        public void Cancel()
        {
            Stop();
        }

        void UpdateAnimator()
        {
            Vector3 velocity = navMesh.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            //gameObject.GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}