using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteController : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionPrefab = null;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private int maxHealth = 3;
    private float health;
    
    private Rigidbody2D _meteoriteRigidbody;

    void Start()
    {
        health = maxHealth;
        _meteoriteRigidbody = GetComponent<Rigidbody2D>();
    }

    // Cada frame llama a la función de mover el láser
    void Update()
    {
        MoveLaser();
    }

    public void MoveLaser()
    {
        _meteoriteRigidbody.velocity = new Vector2(0, -moveSpeed);// cambia el vector velocidad(X, Y) por (0 y la velocidad variable)
    }

    private void Death()
    {
        if (health <= 0)
        {
            ScoreManager.instance.AddPoint();//llama a la función pública que añade nuevos puntos
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

            Destroy(this.gameObject);//destruye al enemigo
        }
    }

    void OnTriggerEnter2D(Collider2D other)//Cuando colisione con cualquier otro Collider "Trigger"
    {
        if (other.tag == "PlayerLaser")//solo se ejecuta si está chocando contra un láser del jugador
        {
            health--;
        }

        if (other.tag == "Player")//solo se ejecuta si está chocando contra un láser del jugador
        {
            Destroy(this.gameObject);//destruye al enemigo
        }
    }
}
