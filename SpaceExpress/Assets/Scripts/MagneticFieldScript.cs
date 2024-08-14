using UnityEngine;

public class MagneticFieldScript : MonoBehaviour
{
    [SerializeField] Rigidbody trainRb;
    public Transform rail;
    public float attractionForce = 10f;
    public float railWidth = 0.5f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(("Wheels")))
        {
            if (trainRb != null)
            {
                Vector3 directionToRail = rail.position - other.transform.position;
                directionToRail.y = 0;

                if (directionToRail.magnitude < railWidth)
                {
                    trainRb.position = new Vector3(rail.position.x, trainRb.position.y, rail.position.z);
                    trainRb.linearVelocity = Vector3.zero;

                    Quaternion targetRotation = Quaternion.Euler(rail.eulerAngles.x * -1,
                        trainRb.rotation.eulerAngles.y, trainRb.rotation.eulerAngles.z);
                    trainRb.rotation = targetRotation;
                }
                else
                {
                    Quaternion targetRotation = Quaternion.Euler(rail.eulerAngles.x * -1,
                        trainRb.rotation.eulerAngles.y, trainRb.rotation.eulerAngles.z);
                    trainRb.rotation = Quaternion.Slerp(trainRb.rotation, targetRotation, Time.deltaTime * 5f);

                    trainRb.AddForce(directionToRail.normalized * attractionForce);
                }
            }
        }
    }
}