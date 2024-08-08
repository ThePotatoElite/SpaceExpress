using UnityEngine;

public class FlagBehaviour : MonoBehaviour
{
    private bool _moveDown = false;
    private float _speed = 50f;

    void Update()
    {
        if (_moveDown)
        {
            transform.Translate(Vector3.down * (_speed * Time.deltaTime)); // Doesn't work currently
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelPassed();
        }
    }

    void LevelPassed() // Should change to what actually happens when you hit the flag
    {
        _moveDown = true;
        Destroy(gameObject, 2f); // Match the time before loading the next level
        // Insert Win Logic Later
    }
}