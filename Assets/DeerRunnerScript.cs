using UnityEngine;

public class DeerRunner : MonoBehaviour
{
    public float speed = 4f;

    void Update()
    {
        float movement = 0f;

        // Up Arrow = forward
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement = 1f;
        }

        // Down Arrow = backward
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movement = -1f;
        }

        transform.Translate(Vector3.forward * movement * speed * Time.deltaTime);
    }
}