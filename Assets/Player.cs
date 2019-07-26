using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [Tooltip("In ms^-1")] [SerializeField] float speed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;
    

    private void Update()
    {
        float clampedXPos = ClampedPos(transform.localPosition.x, "Horizontal", speed, xRange);
        float clampedYPos = ClampedPos(transform.localPosition.y, "Vertical", speed, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private float ClampedPos(float origin, string axis, float speed, float range)
    {
        float input = CrossPlatformInputManager.GetAxis(axis);
        float offset = input * speed * Time.deltaTime;
        float position = origin + offset;
        
        return Mathf.Clamp(position, -range, range);
    }



}
