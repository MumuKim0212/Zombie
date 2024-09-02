using UnityEngine;
using DG.Tweening;

public class FallingFloor : MonoBehaviour
{
    public float shakeStrength = 0.5f;
    public float shakeDuration = 2f;
    public float fallDistance = 10f;
    public float fallDuration = 1f;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartShakeAndFall();
        }
    }

    private void StartShakeAndFall()
    {
        transform.DOShakePosition(shakeDuration, shakeStrength)
            .OnComplete(() =>
            {
                transform.DOMoveY(originalPosition.y - fallDistance, fallDuration);
            });
    }
}