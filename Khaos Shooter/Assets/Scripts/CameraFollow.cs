using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public float damping = 1;
    //public float lookAheadFactor = 3;
    //public float lookAheadReturnSpeed = 0.5f;
    //public float lookAheadMoveThreshold = 0.1f;
    public float yPosBoundry = 0;

    //private float m_OffsetZ;
    //private Vector2 m_LastTargetPosition;
    private Vector2 m_CurrentVelocity;
    //private Vector2 m_LookAheadPos;

    private float nextTimeToSeach = 0;

    // Use this for initialization
    private void Start()
    {
        //m_LastTargetPosition = target.position;
        //m_OffsetZ = (transform.position - target.position).z;
        transform.parent = null;
    }


    // Update is called once per frame
    private void Update()
    {
        //custom block
        if (target == null) {
            if (nextTimeToSeach >= Time.time) {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player == null) return;
                target = player.GetComponent<Transform>();
            } else {
                nextTimeToSeach++;
            }

        };
        if (target == null) return;

        // only update lookahead pos if accelerating or changed direction
        //float xMoveDelta = (target.position - m_LastTargetPosition).x;
        //float xMoveDelta = target.position.x - m_LastTargetPosition.x;

        //bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        //if (updateLookAheadTarget) {
        //    m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        //} else {
        //m_LookAheadPos = Vector2.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        //}
        //Vector2 aheadTargetPos = (Vector2)target.position + m_LookAheadPos + Vector2.right * m_OffsetZ;
        //Vector2 aheadTargetPos = (Vector2)target.position  + Vector2.right * m_OffsetZ;
        Vector2 aheadTargetPos = (Vector2)target.position;// + Vector2.right;
        Vector2 newPosv2 = Vector2.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping, Mathf.Infinity, Time.deltaTime);

        newPosv2 = new Vector2(newPosv2.x, Mathf.Clamp(newPosv2.y, yPosBoundry, Mathf.Infinity)); //clamp camera y pos to set range
        Vector3 newPos = new Vector3(newPosv2.x, newPosv2.y, -11);
        //Vector3 newPos = new Vector3(0, 1, -11);

        //Debug.Log(newPos);
        transform.position = newPos;

        //m_LastTargetPosition = target.position;
    }
}



