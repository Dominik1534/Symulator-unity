using UnityEngine;

public class UsuwanieBrudu : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "�ciany")
        {
            Destroy(gameObject);
        }
    }


}
