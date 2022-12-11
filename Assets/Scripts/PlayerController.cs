using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private GameObject LaserPrefab = null;
    [SerializeField] private float timeBetweenShots = 1;
    [SerializeField] private GameObject ExplosionPrefab = null;
    [SerializeField] private GameObject DamageParticles = null;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Color low;
    [SerializeField] private Color high;
    [SerializeField] private int maxPlayerHealth = 10;
    [SerializeField] private int playerHealth = 10;
    private float cooldown = 0;
    private Rigidbody2D _playerRigidbody;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        SetHealthBar(playerHealth, maxPlayerHealth);
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal")){
        MovePlayer();//si dan A, D, < o > se utilizará un valor de 1 o -1 dependiendo de la dirección
        }
        else{
        StopMovement();//si no dan ninguna tecla para el movimiento 
        }

        cooldown = Mathf.Max(0, cooldown - Time.deltaTime);// cálculo del cooldown

        if (Input.GetButton("Jump"))//si dan al espacio llama a la función de disparar
        {
        Shoot();
        }

        PlayerDeath();
    }

    private void SetHealthBar(float health, float maxHealth)
    {
        healthBar.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, healthBar.normalizedValue);
    }

    private void MovePlayer()
    {
    var horizontalInput = Input.GetAxisRaw("Horizontal");
    _playerRigidbody.velocity = new Vector2(horizontalInput * playerSpeed, 0);//pone la velocidad del jugador como el input horizontal(1) multiplicado por la velocidad para que sea variable en Unity
    }

    private void StopMovement()
    {
    var horizontalInput = Input.GetAxisRaw("Horizontal");
    _playerRigidbody.velocity = new Vector2(horizontalInput * 0, 0);//pone la velocidad del jugador como 0 en X e Y para que no haya ningún movimiento
    }

    private void Shoot()
    {
        if (cooldown == 0)
        {
            cooldown = timeBetweenShots;//reseteo del cooldown
            Instantiate(LaserPrefab, transform.position, Quaternion.identity);//generar el prefab del laser en la posición del jugador y con la rotación del jugador
        }
    }

    void OnTriggerEnter2D(Collider2D other)//Cuando colisione con cualquier otro Collider "Trigger"
    {
        if (other.tag == "HealthPack")
            if (playerHealth < maxPlayerHealth)
                playerHealth++;

        if (other.tag == "EnemyLaser")//solo se ejecuta si está chocando contra un enemigo
        {
            playerHealth--;
            Instantiate(DamageParticles, transform.position, Quaternion.identity);
        }

        if (other.tag == "Enemy")
            playerHealth = 0;
    }

    private void PlayerDeath()
    {
        if (playerHealth <= 0)
        {
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);//destruye el rayo láser
        }
    }
}
