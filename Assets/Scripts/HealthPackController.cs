using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    
    private Rigidbody2D _powerUpRigidbody;

    void Start()
    {
        _powerUpRigidbody = GetComponent<Rigidbody2D>();
    }

    // Cada frame llama a la función de mover el láser
    void Update()
    {
        MoveLaser();
    }

    public void MoveLaser()
    {
        _powerUpRigidbody.velocity = new Vector2(0, -moveSpeed);// cambia el vector velocidad(X, Y) por (0 y la velocidad variable)
    }

    void OnTriggerEnter2D(Collider2D other)//Cuando colisione con cualquier otro Collider "Trigger"
    {
        if (other.tag == "Player")//solo se ejecuta si está chocando contra un enemigo
        {
            Destroy(this.gameObject);//destruye el rayo láser
        }
    }
}
