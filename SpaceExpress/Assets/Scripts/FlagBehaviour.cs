using UnityEngine;

public class FlagBehaviour : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyFlag();
        }
    }

    void DestroyFlag()
    {
        Destroy(gameObject);
    }
}