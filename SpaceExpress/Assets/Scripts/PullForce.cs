using UnityEngine;

public class PullForce : MonoBehaviour
{
    [SerializeField] float pullRadius = 250f;
    [SerializeField] float pullForce = 100f;
    [SerializeField] Collider trainCollider;

    void FixedUpdate()
    {
        ApplyPullForce();
    }

    void ApplyPullForce()
    {
        if (trainCollider != null)
        {
            if (Vector3.Distance(transform.position, trainCollider.transform.position) <= pullRadius)
            {
                Rigidbody trainRigidbody = trainCollider.GetComponent<Rigidbody>();
                if (trainRigidbody != null)
                {
                    Vector3 direction = transform.position - trainCollider.transform.position;
                    trainRigidbody.AddForce(direction.normalized * (pullForce * Time.fixedDeltaTime));
                }
            }
        }
    }
    
    void OnDrawGizmosSelected() // Pull radius visualization
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }
}