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

    [Header("Movement Settings")]
    [SerializeField] private float moveForce = 10f;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        myRb.angularDamping = 0;
    }

    void FixedUpdate()
    {
        RotateObject();
        MoveObject();
    }

    private void RotateObject()
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

    private void MoveObject()
    {
        float h = Input.GetAxis("Horizontal"); // A (-1) ... D (1)
        float v = Input.GetAxis("Vertical");   // S (-1) ... W (1)

        Vector3 forceDirection = new Vector3(h, 0, v).normalized;

        // Pøidej sílu ve smìru pohybu
        myRb.AddForce(forceDirection * moveForce, ForceMode.Force);
    }
}
