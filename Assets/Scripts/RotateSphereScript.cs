using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSphereScript : MonoBehaviour
{

    public float speed = 1;

    private bool triggered = false;
    private float startY;

    public void Triggered()
    {
        startY = transform.localPosition.y;
        triggered = true;
}
	private void Update ()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime * 10);
        if (triggered)
        {
            transform.position += (transform.up * speed * Time.deltaTime);
            triggered = (transform.position.y < startY + 10);
        }
    }

}
