using UnityEngine;

public class Wheels : MonoBehaviour
{
    [SerializeField] GameObject Cart;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Rail")
        {
          //  Cart.transform.Rotate(other.transform.rotation.eulerAngles);
            Debug.Log($"{Cart.name} Should be rotating");
        }
        else { Debug.Log("What is happening"); }
    }

}
