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
    PlayerScript player;

    public void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
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
                Time.timeScale = 5f;
            }
        }
        if (!playerDead)
        {
            moveDir = player.moveDir;
        }
        else
            moveDir = Vector2.zero;
        Vector3 offset = moveDir;
        offset.Scale(CameraOffset);
        offset.z = -10f;
        Vector3 targetPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.position += new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;
    }
}
