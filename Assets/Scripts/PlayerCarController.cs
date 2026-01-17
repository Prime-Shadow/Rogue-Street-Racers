using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [Header("Wheels Collider")]

    public WheelCollider frontLeftWheeleCollider;
    public WheelCollider frontRightWheeleCollider;
    public WheelCollider rearLeftWheeleCollider;
    public WheelCollider rearRightWheeleCollider;

    [Header("Wheels Transform")]
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    [Header("Car Engine")]
    public float accelerationForce = 300f;
    public float breakingForce = 3000f;
    private float presentBreakForce = 0f;
    private float presentAcceleration = 0f;

    [Header("Car Steering")]
    public float wheelsTorque = 35f;
    private float presentTurnAngle = 0f;

    [Header("Car Sounds")]
    public AudioSource audioSource;
    public AudioClip accelerationSound;
    public AudioClip slowAccelerationSound;
    public AudioClip stopSound;

    private void Update()
    {
        MoveCar();
        CarSteering();
        //ApplyBreaks();
    }

    private void MoveCar()
    {
        // AWD
        frontLeftWheeleCollider.motorTorque = presentAcceleration;      // FWD
        frontRightWheeleCollider.motorTorque = presentAcceleration;     // FWD
        rearLeftWheeleCollider.motorTorque = presentAcceleration;       // RWD
        rearRightWheeleCollider.motorTorque = presentAcceleration;      // RWD

        //presentAcceleration = accelerationForce * Input.GetAxis("Vertical");
        presentAcceleration = accelerationForce * SimpleInput.GetAxis("Vertical");
        //Debug.Log("Vertical" + Input.GetAxis("Vertical"));
        //Debug.Log("IsGrounded:" + frontLeftWheeleCollider.isGrounded);


        if (presentAcceleration > 0)
        {
            audioSource.PlayOneShot(accelerationSound, 0.2f);
        }
        else if (presentAcceleration < 0)
        {
            audioSource.PlayOneShot(slowAccelerationSound, 0.2f);
        }
        else if (presentAcceleration == 0)
        {
            audioSource.PlayOneShot(stopSound, 0.2f);
        }

    }

    private void CarSteering()
    {
        //presentTurnAngle = wheelsTorque * Input.GetAxis("Horizontal");
        presentTurnAngle = wheelsTorque * SimpleInput.GetAxis("Horizontal");
        frontLeftWheeleCollider.steerAngle = presentTurnAngle;
        frontRightWheeleCollider.steerAngle = presentTurnAngle;

        SteeringWheels(frontLeftWheeleCollider, frontLeftWheelTransform);
        SteeringWheels(frontRightWheeleCollider, frontRightWheelTransform);
        SteeringWheels(rearLeftWheeleCollider, rearLeftWheelTransform);
        SteeringWheels(rearRightWheeleCollider, rearRightWheelTransform);
    }
    void SteeringWheels(WheelCollider WC, Transform WT)
    {
        Vector3 position;
        Quaternion rotation;

        WC.GetWorldPose(out position, out rotation);

        WT.position = position;
        WT.rotation = rotation;

    }

    public void ApplyBreaks()
    {
        //if(Input.GetKey(KeyCode.Space))
        //{
        //    presentBreakForce = breakingForce;
        //}
        //else
        //{
        //    presentBreakForce = 0f;
        //}

        //frontLeftWheeleCollider.brakeTorque = presentBreakForce;
        //frontRightWheeleCollider.brakeTorque = presentBreakForce;
        //rearLeftWheeleCollider.brakeTorque = presentBreakForce;
        //rearRightWheeleCollider.brakeTorque = presentBreakForce;

        StartCoroutine(carBreaks());
    }

    IEnumerator carBreaks()
    {
        presentBreakForce = breakingForce;

        frontLeftWheeleCollider.brakeTorque = presentBreakForce;
        frontRightWheeleCollider.brakeTorque = presentBreakForce;
        rearLeftWheeleCollider.brakeTorque = presentBreakForce;
        rearRightWheeleCollider.brakeTorque = presentBreakForce;

        yield return new WaitForSeconds(2f);

        presentBreakForce = 0f;

        frontLeftWheeleCollider.brakeTorque = presentBreakForce;
        frontRightWheeleCollider.brakeTorque = presentBreakForce;
        rearLeftWheeleCollider.brakeTorque = presentBreakForce;
        rearRightWheeleCollider.brakeTorque = presentBreakForce;


    }

}
