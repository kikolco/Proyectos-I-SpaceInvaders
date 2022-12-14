using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OscilatingEnemyController : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionPrefab = null;
    [SerializeField] private GameObject EnemyLaserPrefab = null;
    [SerializeField] private GameObject PowerUpPrefab = null;
    [SerializeField] private float fireTimer = 3.0f;
    private float fireTime;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float verticalMoveSpeed = 2.5f;
    [SerializeField] private float horizontalMoveSpeed = 1.0f;
    public HealthBarController HealthBar;
    private float health;
    private bool continueDown = true;
    private bool Direction = true;

    [SerializeField] private Rigidbody2D _enemyRigidbody;

    void Start()
    {
        fireTime = fireTimer;
        health = maxHealth;

        _enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        fireTime -= Time.deltaTime;
        if (fireTime <= 0.0f)
            FireLaser();

        MoveEnemy();
        Death();

        HealthBar.SetHealthBar(health, maxHealth);
    }

    void FireLaser()
    {
        if (continueDown == false)
        {
            Instantiate(EnemyLaserPrefab, transform.position, Quaternion.identity);
            fireTime = fireTimer;
        }
    }

    public void MoveEnemy()
    {
        if (continueDown == true)
            _enemyRigidbody.velocity = new Vector2(0, -verticalMoveSpeed);// cambia el vector velocidad(X, Y) por (0 y la velocidad variable)
        else if (Direction == true)
            _enemyRigidbody.velocity = new Vector2(horizontalMoveSpeed, 0);
        else if (Direction == false)
            _enemyRigidbody.velocity = new Vector2(-horizontalMoveSpeed, 0);
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
            ScoreManager.instance.AddPoint();//llama a la funci??n p??blica que a??ade nuevos puntos
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            float powerUpChance = Random.Range(0f, 1f);
            if (powerUpChance <= 0.12)
            {
                Instantiate(PowerUpPrefab, transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);//destruye al enemigo
        }
    }

    void OnTriggerEnter2D(Collider2D other)//Cuando colisione con cualquier otro Collider "Trigger"
    {
        if (other.tag == "PlayerLaser")//solo se ejecuta si est?? chocando contra un l??ser del jugador
        {
            health--;
        }

        if (other.tag == "EnemyStop")//solo se ejecuta si est?? chocando contra un l??ser del jugador
        {
            continueDown = false;
        }

        if (other.tag == "ChangeDirection")//solo se ejecuta si est?? chocando contra un l??ser del jugador
        {
            Direction = !Direction;
        }

        if (other.tag == "Player")//solo se ejecuta si est?? chocando contra un l??ser del jugador
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
