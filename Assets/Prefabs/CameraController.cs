using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector2 moveDir = Vector2.one;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.25f;

    [SerializeField] private Vector3 CameraOffset = new Vector3(2f, 2f, -10f);
    [SerializeField] private Transform target;

    bool playerDead = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            var targetSecond = GameObject.Find("King");
            if (targetSecond != null)
            {
                target = targetSecond.transform;
                playerDead = true;
                Time.timeScale = 2f;
            }
        }
        if (!playerDead)
        {
            moveDir = GameObject.FindWithTag("Player").GetComponent<PlayerScript>().moveDir;
        }
        else
            moveDir = Vector2.zero;
        Vector3 offset = moveDir;
        offset.Scale(CameraOffset);
        offset.z = -10f;
        Vector3 targetPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
