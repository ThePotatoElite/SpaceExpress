using UnityEngine;

public class FlagBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyFlag();
        }
    }

    private void DestroyFlag()
    {
        Destroy(gameObject);
    }
}