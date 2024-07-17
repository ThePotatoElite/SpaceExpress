using UnityEngine;

public class Wheels : MonoBehaviour
{
    [SerializeField] GameObject cart;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Rail"))
        {
            // Cart.transform.Rotate(other.transform.rotation.eulerAngles);
            Debug.Log($"{cart.name} Should be rotating");
        }
        else { Debug.Log("What is happening?"); }
    }
}