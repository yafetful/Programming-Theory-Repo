using UnityEngine;

public class ZombieCharacterControl : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 1;
    [SerializeField] private Animator m_animator = null;
    [SerializeField] private Rigidbody m_rigidBody = null;
    private float m_currentV = 0.5f;

    private void Awake()
    {
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }
    }

    private void FixedUpdate()
    {
        TankUpdate();
    }

    private void TankUpdate()
    {
        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
        m_animator.SetFloat("MoveSpeed", m_currentV);
    }

}