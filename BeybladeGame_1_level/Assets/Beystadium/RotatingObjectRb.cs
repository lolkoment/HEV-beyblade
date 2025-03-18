using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RotatingObjectRb : MonoBehaviour
{
    private Rigidbody myRb;
    private const float SpeedFactor = 1f;

    private enum RotateAxe { AxeX, AxeY, AxeZ }
    [SerializeField] private RotateAxe rotateAxe;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float maxRotationSpeed = 50f;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        myRb.angularDamping = 0;
    }

    void FixedUpdate()
    {
        Vector3 axis = Vector3.zero;

        switch (rotateAxe)
        {
            case RotateAxe.AxeX:
                axis = Vector3.right;
                break;
            case RotateAxe.AxeY:
                axis = Vector3.up;
                break;
            case RotateAxe.AxeZ:
                axis = Vector3.forward;
                break;
        }

        myRb.AddTorque(axis * rotationSpeed * SpeedFactor, ForceMode.Force);

        if (myRb.angularVelocity.magnitude > maxRotationSpeed)
        {
            myRb.angularVelocity = myRb.angularVelocity.normalized * maxRotationSpeed;
        }
    }
}
