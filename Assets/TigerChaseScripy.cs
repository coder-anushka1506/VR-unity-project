using UnityEngine;

public class TigerChase : MonoBehaviour
{
    public Transform deer;
    public float speed = 3f;
    public float stopDistance = 4f;

    void Update()
    {
        if (deer == null) return;

        float distance = Vector3.Distance(transform.position, deer.position);

        if (distance > stopDistance)
        {
            Vector3 direction = deer.position - transform.position;
            direction.y = 0f;

            transform.position += direction.normalized * speed * Time.deltaTime;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    5f * Time.deltaTime
                );
            }
        }
    }
}