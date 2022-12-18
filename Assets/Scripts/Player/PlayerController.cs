using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private GameObject LaserPrefab = null;
    [SerializeField] private float timeBetweenShots = 1;
    [SerializeField] private GameObject ExplosionPrefab = null;
    [SerializeField] private GameObject DamageParticles = null;
    [SerializeField] private GameObject LaserAbility = null;
    [SerializeField] private GameObject abilityText = null;
    [SerializeField] private int maxPlayerHealth = 6;
    [SerializeField] private PlayerHealthBarController healthBarController;
    [SerializeField] private GameController gameController;
    private int playerHealth;
    private float cooldown = 0;
    private float multiplierTimer = 60;
    public bool laserAbility = false;
    private float laserAbilityTmr = 40;
    private Rigidbody2D _playerRigidbody;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        playerHealth = maxPlayerHealth;
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

        if (Input.GetButton("Jump") && laserAbility == false)//si dan al espacio llama a la función de disparar
        {
        Shoot();
        }
        CheckLaserAbility();
        PlayerDeath();

        healthBarController.SetHealthBar(playerHealth, maxPlayerHealth);

        multiplierTimer -= Time.deltaTime;
        if (multiplierTimer <= 0)
        {
            ScoreManager.instance.AddMultiplier();
            multiplierTimer = 60;
        }
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

    private void CheckLaserAbility()
    {
        laserAbilityTmr -= Time.deltaTime;

        if (laserAbilityTmr <= 0)
        {
            abilityText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Q))
            {
                LaserAbility.SetActive(true);
                laserAbility = true;
                laserAbilityTmr = 40;
                //abilityText.GetComponent<Animation>().Play("ability_text");
            }
        }else
            abilityText.SetActive(false);   
    }

    void OnTriggerEnter2D(Collider2D other)//Cuando colisione con cualquier otro Collider "Trigger"
    {
        if (other.tag == "HealthPack")
            if (playerHealth < maxPlayerHealth)
                playerHealth++;

        if (other.tag == "EnemyLaser")//solo se ejecuta si está chocando contra un enemigo
        {
            playerHealth--;
            multiplierTimer = 60;
            ScoreManager.instance.RemoveMultiplier();
            Instantiate(DamageParticles, transform.position, Quaternion.identity, this.transform);
        }

        if (other.tag == "Enemy")
            playerHealth = 0;
    }

    private void PlayerDeath()
    {
        if (playerHealth <= 0)
        {
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            gameController.GameOver();
            Destroy(this.gameObject);
        }
    }
}
