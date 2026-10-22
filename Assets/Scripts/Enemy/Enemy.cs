using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    // temporary for testing
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 5;

    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (Vector2)target.position - rigidbody.position;

            if (direction.sqrMagnitude > 0.01f)
            {
                rigidbody.linearVelocity = direction.normalized * speed;
            }
            else
            {
                rigidbody.linearVelocity = Vector2.zero;
            }
        }
        else
        {
            rigidbody.linearVelocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(10);

            Destroy(gameObject);
        }
    }
}
