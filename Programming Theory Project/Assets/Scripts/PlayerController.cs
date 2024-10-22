using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isShooting;
    private float MoveSpeed;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = new Vector3(0, 0, 25);
        StartCoroutine(MoveToPosition(new Vector3(0, 0, 40), 3.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true;
            animator.SetBool("isShooting", isShooting);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
            animator.SetBool("isShooting", isShooting);
        }
    }

    private IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsed = 0;
        MoveSpeed = 2.0f;
        animator.SetFloat("Speed", MoveSpeed);
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPosition, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
        MoveSpeed = 0.5f;
        animator.SetFloat("Speed", MoveSpeed);
    }
}
