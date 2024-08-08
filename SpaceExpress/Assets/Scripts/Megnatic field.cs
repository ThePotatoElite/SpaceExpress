using UnityEngine;
using UnityEngine.UIElements;

public class MagneticFieldScript : MonoBehaviour
{
    [SerializeField] Rigidbody TrainRB;
    public Transform rail;
    public float attractionForce = 10f;
    public float railWidth = 0.5f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == ("Wheels"))
        {
            if (TrainRB != null)
            {
                Vector3 directionToRail = rail.position - other.transform.position;
                directionToRail.y = 0;  

                if (directionToRail.magnitude < railWidth)
                {
                    TrainRB.position = new Vector3(rail.position.x, TrainRB.position.y, rail.position.z);
                    TrainRB.linearVelocity = Vector3.zero;  

                    Quaternion targetRotation = Quaternion.Euler(rail.eulerAngles.x * -1, TrainRB.rotation.eulerAngles.y, TrainRB.rotation.eulerAngles.z);
                    TrainRB.rotation = targetRotation ;
                }
                else
                {
                    Quaternion targetRotation = Quaternion.Euler(rail.eulerAngles.x * -1, TrainRB.rotation.eulerAngles.y, TrainRB.rotation.eulerAngles.z);
                    TrainRB.rotation = Quaternion.Slerp(TrainRB.rotation, targetRotation, Time.deltaTime * 5f);

                    TrainRB.AddForce(directionToRail.normalized * attractionForce);
                }
            }
        }
        }
    }