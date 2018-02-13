using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;
    public float yPosBoundry = 0; //custom

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;

    private float nextTimeToSeach = 0;

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
        float xMoveDelta = (target.position - m_LastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget) {
            m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        } else {
            m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

        newPos = new Vector3(newPos.x, Mathf.Clamp(newPos.y, yPosBoundry, Mathf.Infinity), newPos.z); //new custom line for clamping camera y pos to set range

        transform.position = newPos;

        m_LastTargetPosition = target.position;
    }
}



