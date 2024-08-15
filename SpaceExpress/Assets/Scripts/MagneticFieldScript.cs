using UnityEngine;

public class MagneticFieldScript : MonoBehaviour
{
    [SerializeField] Rigidbody trainRb;
    public Transform rail;
    public float attractionForce = 10f;
    public float railWidth = 0.5f;

    private void Awake()
    {

       // trainRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();

    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(trainRb.linearVelocity);


        if (other.gameObject.CompareTag(("Wheels")))
        {

            if (trainRb != null)
            {
                Vector3 directionToRail = rail.position - other.transform.position;
                directionToRail.y = 0;

                if (directionToRail.magnitude < railWidth)
                {
                    Debug.Log("Im here 1");

                    trainRb.position = new Vector3(rail.position.x, trainRb.position.y, rail.position.z);
                    trainRb.linearVelocity = Vector3.zero;

                    Quaternion targetRotation = Quaternion.Euler(rail.eulerAngles.x * -1,
                        trainRb.rotation.eulerAngles.y, trainRb.rotation.eulerAngles.z);
                    trainRb.rotation = targetRotation;
                }
                else
                {
                    Debug.Log("Im here 2");
                    Quaternion targetRotation = Quaternion.Euler(rail.eulerAngles.x * -1,
                        trainRb.rotation.eulerAngles.y, trainRb.rotation.eulerAngles.z);
                    trainRb.rotation = Quaternion.Slerp(trainRb.rotation, targetRotation, Time.deltaTime * 5f);
                    directionToRail.x = 140;
                     trainRb.AddForce(directionToRail.normalized * attractionForce);
                }
            }
            
        }
       
    }
}