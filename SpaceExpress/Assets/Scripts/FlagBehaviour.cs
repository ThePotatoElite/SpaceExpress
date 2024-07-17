using UnityEngine;

public class FlagBehaviour : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelPassed();
        }
    }

    void LevelPassed() // Should change to what actually happens when you hit the flag
    {
        Destroy(gameObject);
       // Insert Win Logic Later
    }
}