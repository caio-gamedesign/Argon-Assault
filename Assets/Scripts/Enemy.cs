using UnityEngine;

public class Enemy : MonoBehaviour
{
    SphereCollider sphereCollider;
    bool isAlive = true;

    [SerializeField] int scorePerHit = 12;

    ScoreText scoreText;

    [SerializeField] GameObject deathFX;
    [SerializeField] int healthPoints = 100;
    [SerializeField] int maxHits = 3;

    private void Start()
    {
        SetupSphereCollider();
        scoreText = FindObjectOfType<ScoreText>();
    }

    private void SetupSphereCollider()
    {
        sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider == null)
        {
            sphereCollider = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
        }

        sphereCollider.isTrigger = false;
        sphereCollider.radius = 0.9f;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (isAlive)
        {
            Hit();
        }
    }

    private void Hit()
    {
        scoreText.ScoreHit(scorePerHit);
        maxHits--;
        if(maxHits <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;
        Instantiate(deathFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
