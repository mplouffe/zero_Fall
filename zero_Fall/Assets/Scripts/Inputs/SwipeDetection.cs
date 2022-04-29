using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField]
    private float m_minimumDistance = 0.2f;

    [SerializeField]
    private float m_maximumTime = 1f;

    private PlayerController m_playerController;

    private Vector2 m_startPosition;
    private Vector2 m_endPosition;
    
    private float m_startTime;
    private float m_endTime;

    private void Awake()
    {
        m_playerController = PlayerController.Instance;
    }

    private void OnEnable()
    {
        m_playerController.OnStartTouch += SwipeStart;
        m_playerController.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        m_playerController.OnStartTouch -= SwipeStart;
        m_playerController.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        m_startPosition = position;
        m_startTime = time;
        Debug.Log(m_startPosition);
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        m_endPosition = position;
        m_endTime = time;
        Debug.Log(m_endPosition);
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(m_startPosition, m_endPosition) >= m_minimumDistance &&
            (m_endTime - m_startTime) <= m_maximumTime)
        {
            Debug.DrawLine(m_startPosition, m_endPosition, Color.red, 5f);
        }
    }
}
