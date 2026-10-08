using UnityEngine;

public class ZoneSwitchController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;
    public Vector3 targetZonePosition = new Vector3(5f, 0f, 5f);
    public float zoneRadius = 2.0f;

    public Camera mainVRCamera;
    public Camera secondaryCamera;
    public Light primaryLight;
    public Light secondaryLight;

    private Rigidbody rb;
    private bool isGrounded = true;
    private bool hasSwitched = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetStates(mainActive: true);
    }

    void Update()
{
    // Touch controls for Mobile: Tap the screen to Jump
    if ((Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && isGrounded)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    // Tilt controls using Phone Accelerometer (Tilt phone to move)
    Vector3 tilt = new Vector3(Input.acceleration.x, 0, Input.acceleration.y);
    transform.Translate(tilt * moveSpeed * Time.deltaTime, Space.World);

    // Standard Keyboard Fallback (for testing in Unity Editor)
    float moveX = Input.GetAxis("Horizontal");
    float moveZ = Input.GetAxis("Vertical");
    Vector3 move = new Vector3(moveX, 0, moveZ);
    transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

    // Zone Detection Logic
    float distance = Vector3.Distance(transform.position, targetZonePosition);
    if (distance <= zoneRadius && !hasSwitched)
    {
        SetStates(mainActive: false);
        hasSwitched = true;
    }
    else if (distance > zoneRadius && hasSwitched)
    {
        SetStates(mainActive: true);
        hasSwitched = false;
    }
}

    void SetStates(bool mainActive)
    {
        if (mainVRCamera) mainVRCamera.gameObject.SetActive(mainActive);
        if (secondaryCamera) secondaryCamera.gameObject.SetActive(!mainActive);
        if (primaryLight) primaryLight.enabled = mainActive;
        if (secondaryLight) secondaryLight.enabled = !mainActive;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
}