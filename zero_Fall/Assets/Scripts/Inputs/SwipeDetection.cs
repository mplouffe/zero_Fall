using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField]
    private float m_minimumDistance = 0.2f;

    [SerializeField]
    private float m_maximumTime = 1f;

    [SerializeField, Range(0f, 1f)]
    private float m_directionThreshold = 0.9f;

    [SerializeField]
    private GameObject m_trail;

    private PlayerController m_playerController;

    private Vector2 m_startPosition;
    private Vector2 m_endPosition;
    
    private float m_startTime;
    private float m_endTime;

    private bool m_swiping;

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

    private void Update()
    {
        if (m_swiping)
        {
            Debug.Log("Swiping");
            m_trail.transform.position = m_playerController.TouchPosition();
        }
    }

    private void SwipeStart(Vector2 position, float time)
    {
        m_startPosition = position;
        m_startTime = time;
        m_swiping = true;
        m_trail.SetActive(true);
        m_trail.transform.position = position;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        m_trail.SetActive(false);
        m_swiping = false;
        m_endPosition = position;
        m_endTime = time;
        DetectSwipe();
    }

    private IEnumerator Trail()
    {
        while(true)
        {
            m_trail.transform.position = m_playerController.TouchPosition();
        }
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(m_startPosition, m_endPosition) >= m_minimumDistance &&
            (m_endTime - m_startTime) <= m_maximumTime)
        {
            Debug.DrawLine(m_startPosition, m_endPosition, Color.red, 5f);
            Vector3 direction = m_endPosition - m_startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > m_directionThreshold)
        {
            Debug.Log("Swipe UP");
        }

        if (Vector2.Dot(Vector2.down, direction) > m_directionThreshold)
        {
            Debug.Log("Swipe DOWN");
        }

        if (Vector2.Dot(Vector2.left, direction) > m_directionThreshold)
        {
            Debug.Log("Swipe LEFT");
        }

        if (Vector2.Dot(Vector2.right, direction) > m_directionThreshold)
        {
            Debug.Log("Swipe RIGHT");
        }
    }
}
