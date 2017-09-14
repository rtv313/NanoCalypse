using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    public void fireShake(int magnitude) // 0 - small, 1 - big, 2 - long
    {
        switch (magnitude)
        {
            case 0:
                shakeDuration = 0.3f;
                shakeAmount = 0.4f;
                decreaseFactor = 1.2f;
                break;

            case 1:
                shakeDuration = 0.5f;
                shakeAmount = 0.7f;
                decreaseFactor = 1.0f;
                break;

            case 2:
                shakeDuration = 2.0f;
                shakeAmount = 0.7f;
                decreaseFactor = 1.0f;
                break;

            default:
                shakeDuration = 0.5f;
                shakeAmount = 0.7f;
                decreaseFactor = 1.0f;
                break;
        }
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = transform.position + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0.0f;
        }
    }
}
