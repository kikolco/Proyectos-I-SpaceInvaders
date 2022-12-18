using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuicideEnemyController : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionPrefab = null;
    [SerializeField] private GameObject EnemyLaserPrefab = null;
    [SerializeField] private GameObject PowerUpPrefab = null;
    private float changeDir = 0.5f;
    [SerializeField] private float fireTimer = 2.0f;
    private float fireTime;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private int maxHealth = 3;

    public HealthBarController HealthBar;
    private float health;
    
    [SerializeField] private Rigidbody2D _suicideEnemyRigidbody;

    void Start()
    {
        health = maxHealth;
        fireTime = fireTimer;

        _suicideEnemyRigidbody = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    // Cada frame llama a la función de mover el láser
    void Update()
    {
        changeDir -= Time.deltaTime;
        if (changeDir <= 0.0f)
            ChangeDirection();

        if (changeDir > 0.0f)
            MoveEnemy();

        fireTime -= Time.deltaTime;
        if (fireTime <= 0.0f)
            FireLaser();

        Death();

        HealthBar.SetHealthBar(health, maxHealth);
    }

    void ChangeDirection()
    {
        _suicideEnemyRigidbody.velocity = new Vector2(Random.Range(-5f, 5f), -moveSpeed);
        changeDir = 0.5f;
    }

    void FireLaser()
    {
        Instantiate(EnemyLaserPrefab, transform.position, Quaternion.identity);
        fireTime = fireTimer;
    }

    public void MoveEnemy()
    {
        _suicideEnemyRigidbody.velocity = new Vector2(_suicideEnemyRigidbody.velocity.x, -moveSpeed);// cambia el vector velocidad(X, Y) por (0 y la velocidad variable)
    }

    private void RemoveHealth()
    {
        float removeHealthTmr = 0.25f;
        removeHealthTmr -= Time.deltaTime;

        if (removeHealthTmr <= 0)
        {
            removeHealthTmr = 0.25f;
            health--;
        }

    }

    private void Death()
    {
        if (health <= 0)
        {
            ScoreManager.instance.AddPoint();//llama a la función pública que añade nuevos puntos
            ScoreManager.instance.AddPoint();
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            float powerUpChance = Random.Range(0f, 1f);
            if (powerUpChance <= 0.24)
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
    float removeHealthTmr = 0.25f;
    void OnTriggerStay2D(Collider2D collider) 
    {
        if (collider.CompareTag("LaserAbility"))
        {
            removeHealthTmr = Mathf.Max(0, removeHealthTmr - Time.deltaTime);

            if (removeHealthTmr <= 0.0)
            {
                removeHealthTmr = 0.25f;
                health--;
            }
        }
    }
}
