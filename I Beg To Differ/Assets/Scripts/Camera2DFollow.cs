using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

		public GameObject lowerBound;
		public GameObject upperBound;
		public float threshold = 5f;
		public float defaultheight = 0f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        private float speed = 10f;

        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {
            /*
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            transform.position = targetPosition;
            */

			handleVertical ();
        }

		private void handleVertical (){
			if (this.transform.position.y < lowerBound.transform.position.y) {
				this.transform.position = new Vector3 (this.transform.position.x,
				                                       lowerBound.transform.position.y, this.transform.position.z);
			} else if (this.transform.position.y > upperBound.transform.position.y) {
				this.transform.position = new Vector3 (this.transform.position.x,
				                                       upperBound.transform.position.y, this.transform.position.z);

			}

		}
        
		private void FixedUpdate()
        {
            // only update lookahead pos if accelerating or changed direction
            
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = new Vector3(target.position.x, transform.position.y, -10f) + m_LookAheadPos + Vector3.forward * m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;

            m_LastTargetPosition = target.position;
            
        }
    }
}
