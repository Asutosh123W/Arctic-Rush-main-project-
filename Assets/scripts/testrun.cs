
using UnityEngine;

public class testrun : MonoBehaviour
{
    public float speed = 5;
    public Rigidbody rb;

    private void FixedUpdate()
    {
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove);
    }

    private void Update()
    {

    }
}
