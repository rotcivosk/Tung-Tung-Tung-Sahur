using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Transform attackPoint;

    private void Start()
    {
        attackPoint = GameObject.FindGameObjectWithTag("AttackPoint").transform;
    }

    private void Update()
    {
        if (attackPoint == null) return;

        Vector3 direction = (attackPoint.position - transform.position).normalized;

        // Move towards the attack point
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

        // Rotate to face the attack point
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            GameManager.instance.AddPoint();
        }
        else if (other.CompareTag("Player") || other.CompareTag("AttackPoint"))
        {
            if (!GameManager.instance) return;

            
            GameManager.instance.SendMessage("GameOver");
        }
    }
}
