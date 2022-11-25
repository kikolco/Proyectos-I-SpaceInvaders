using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserScript : MonoBehaviour
{
    [SerializeField] private float laserSpeed = 5.0f;
    private Rigidbody2D _laserRigidbody;

    void Start()
    {
        _laserRigidbody = GetComponent<Rigidbody2D>();
    }

    // Cada frame llama a la función de mover el láser
    void Update()
    {
        MoveLaser();
    }

    public void MoveLaser()
    {
        _laserRigidbody.velocity = new Vector2(0, laserSpeed);// cambia el vector velocidad(X, Y) por (0 y la velocidad variable)
    }

    void OnTriggerEnter2D(Collider2D other)//Cuando colisione con cualquier otro Collider "Trigger"
    {
        if (other.tag == "Enemy")//solo se ejecuta si está chocando contra un enemigo
        {
            Destroy(this.gameObject);//destruye el rayo láser
        }
    }
}
