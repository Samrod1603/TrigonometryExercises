using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarExperiments : MonoBehaviour
{

    [SerializeField] float r;
    [SerializeField] float t; 
    [SerializeField] private Transform bolita;

    [Header("Velocity")]
    [SerializeField] private float radialSpeed;
    [SerializeField] private float AngularSpeed;

    [Header("Camera")]
    [SerializeField] private Camera camara; 

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        CheckBounds(camara, bolita);

        t += AngularSpeed; // Time.deltaTime; 
        r += radialSpeed;  // Time.deltaTime;
        bolita.position = PolarToCartesian(r, t);
        
        Debug.DrawLine(Vector3.zero, bolita.position); 
    }

    private Vector3 PolarToCartesian(float radius, float angle)
    {
        Vector3 unitDir = new Vector3
       (
          radius * Mathf.Cos(angle * Mathf.Deg2Rad),
          radius * Mathf.Sin(angle * Mathf.Deg2Rad)
       );
        return unitDir; 
    }


    private void CheckBounds(Camera camara, Transform bolita)
    {
        if (Mathf.Abs(bolita.position.x) > camara.orthographicSize)
        {
            Debug.Log("hit x");
            radialSpeed = -radialSpeed;
            AngularSpeed = -AngularSpeed;  
            
        }
        if (Mathf.Abs(bolita.position.y) > camara.orthographicSize)
        {
            Debug.Log("hit y");
            radialSpeed = -radialSpeed;
            AngularSpeed = -AngularSpeed;
        }
    }
}
