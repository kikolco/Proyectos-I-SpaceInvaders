using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuicideEnemyController : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionPrefab = null;
    [SerializeField] private GameObject PowerUpPrefab = null;
    [SerializeField] private float changeDir = 1.0f;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private Slider healthBar;

    public HealthBarController HealthBar;
    private float health;
    
    [SerializeField] private Rigidbody2D _suicideEnemyRigidbody;

    void Start()
    {
        health = maxHealth;
        HealthBar.SetHealthBar(health, maxHealth);

        _suicideEnemyRigidbody = GetComponent<Rigidbody2D>();
        timerEnded();
    }

    // Cada frame llama a la función de mover el láser
    void Update()
    {
        changeDir -= Time.deltaTime;
        if (changeDir <= 0.0f)
            timerEnded();

        MoveEnemy();
        Death();
    }

    void timerEnded()
    {
        _suicideEnemyRigidbody.velocity = new Vector2(Random.Range(-10f, 10f), -moveSpeed);
        changeDir = 3.0f;
    }

    public void MoveEnemy()
    {
        _suicideEnemyRigidbody.velocity = new Vector2(0, -moveSpeed);// cambia el vector velocidad(X, Y) por (0 y la velocidad variable)
    }

    private void Death()
    {
        if (health <= 0)
        {
            ScoreManager.instance.AddPoint();//llama a la función pública que añade nuevos puntos
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            float powerUpChance = Random.Range(0f, 1f);
            if (powerUpChance <= 0.8)
            {
                Instantiate(PowerUpPrefab, transform.position, Quaternion.identity);
            }

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
