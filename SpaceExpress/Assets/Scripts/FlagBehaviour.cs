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

    void LevelPassed() // Should change to what should actully happen when you hit the flag
    {
        Destroy(this.gameObject);
       // Insert Win Logic
    }
}