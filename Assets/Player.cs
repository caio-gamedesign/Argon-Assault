using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")] [SerializeField] float speed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlThrowPitchFactor = -30f;

    [SerializeField] float positionYawFactor = 4f;
    [SerializeField] float controlThrowRollFactor = -20f;

    private float xThrow, yThrow;

    private void Update()
    {
        ProcessPosition();
        ProcessRotation();
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

        float clampedXPos = ClampedPos(transform.localPosition.x, xThrow, speed, xRange);
        float clampedYPos = ClampedPos(transform.localPosition.y, yThrow, speed, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private float ClampedPos(float origin, float inputThrow, float speed, float range)
    {
        float offset = inputThrow * speed * Time.deltaTime;
        float position = origin + offset;

        return Mathf.Clamp(position, -range, range);
    }



}
