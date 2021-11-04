using UnityEngine;

public class Target : MonoBehaviour
{
    public ParticleSystem explosionFX;
    public int scoreToAdd;

    private GameManager gameManager;
    private Rigidbody targetRb;
    private float minSpeed = 12f, maxSpeed = 16f, torqueSpeed = 10f, xRange = 4f, yPosition = -2f;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        transform.position = RandomPosition();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            gameManager.UpdateScore(scoreToAdd);
            Instantiate(explosionFX, transform.position, explosionFX.transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        Destroy(gameObject);
        if (gameObject.CompareTag("Good") && gameManager.isGameActive)
        {
            gameManager.DecreaseLives(-1);
        }
    } 

    private Vector3 RandomPosition() => new Vector3(Random.Range(-xRange, xRange), yPosition, 0f);

    private Vector3 RandomForce() => Vector3.up * Random.Range(minSpeed, maxSpeed);

    private float RandomTorque() => Random.Range(-torqueSpeed, torqueSpeed);
}
