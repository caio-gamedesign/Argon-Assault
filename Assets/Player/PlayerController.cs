using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float controlSpeed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;

    [Header("Screen Position Based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 4f;

    [Header("Control Throw Based")]
    [SerializeField] float controlThrowPitchFactor = -30f;
    [SerializeField] float controlThrowRollFactor = -20f;

    [SerializeField] GameObject[] guns;

    [SerializeField] ParticleSystem[] particleSystems;

    private float xThrow, yThrow;
    bool isControlEnabled = true;

    void OnPlayerDeath()
    {
        isControlEnabled = false;
    }

    private void Update()
    {
        if (isControlEnabled)
        {
            ProcessPosition();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void ProcessFiring()
    {
        bool isActive = CrossPlatformInputManager.GetButton("Fire");
        SetGunsActive(isActive);
    }

    private void SetGunsActive(bool isActive)
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            var emission = ps.emission;
            emission.enabled = isActive;
        }
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlThrowPitchFactor;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;

        float rollDueToControlThrow = xThrow * controlThrowRollFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = yawDueToPosition;
        float roll = rollDueToControlThrow;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessPosition()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float clampedXPos = ClampedPos(transform.localPosition.x, xThrow, controlSpeed, xRange);
        float clampedYPos = ClampedPos(transform.localPosition.y, yThrow, controlSpeed, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private float ClampedPos(float origin, float inputThrow, float speed, float range)
    {
        float offset = inputThrow * speed * Time.deltaTime;
        float position = origin + offset;

        return Mathf.Clamp(position, -range, range);
    }
}
