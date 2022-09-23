using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScript : MonoBehaviour
{
    public enum MovementMode
    {
        ConstantVelocity = 0,
        Acceleration
    }

    [SerializeField]
    float speed = 10;
    Vector3 velocity;
    private Vector3 acceleration; 
    [SerializeField] private MovementMode movementMode;

    private void Start()
    {
        velocity = new Vector3(speed, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = GetWorldMousePosition();

        Vector3 target = mousePos - transform.position;

        float angle;

        if (movementMode == MovementMode.ConstantVelocity)
        {
            angle = Mathf.Atan2(target.y, target.x);
        }
        else
        {
            angle = Mathf.Atan2(velocity.y, velocity.x);
        }

        Move(target);
        LookAt(GetWorldMousePosition()); 
    }

    private void LookAt(Vector2 targetPosition)
    {
        Vector2 thisPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 forward = targetPosition - thisPosition;
        float radians = Mathf.Atan2(forward.y, forward.x) - Mathf.PI / 2;
        RotateZ(radians);
    }

    private Vector3 GetWorldMousePosition()
    {
        Camera camera = Camera.main;
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane);
        Vector4 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        return worldPos;
    }

    private void RotateZ(float radians)
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, radians * Mathf.Rad2Deg);
    }

    private void Move(Vector3 target)
    {
        if (movementMode == MovementMode.ConstantVelocity)
        {
            if (target.magnitude > 1f)
                transform.position += target.normalized * speed * Time.deltaTime;
        }
        else
        {
            Vector3 acceleration = GetWorldMousePosition() - transform.position;
            velocity += acceleration * Time.deltaTime;
            transform.position += new Vector3(velocity.x, velocity.y, 0f) * Time.deltaTime;
        }
    }
}
