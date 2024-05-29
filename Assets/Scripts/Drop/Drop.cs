using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private float _fallSpeed = 0f;

    public SpriteRenderer SpriteRenderer { get; private set; }

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Fall();
    }

    private void Fall()
    {
        transform.Translate(Vector3.down * (_fallSpeed / 10));
    }

    public void SetFallSpeed(float fallSpeed)
    {
        _fallSpeed = Mathf.Clamp(fallSpeed, 0f, float.MaxValue);
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}
