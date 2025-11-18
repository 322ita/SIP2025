using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float sens = 100f;
    public Transform playerT;
    public Transform cameraHead;
    float mouseX=0f, mouseY=0f;
    public float smooth = 0.25f;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        sens = PlayerPrefs.GetFloat("Sens", 10f);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraHead.position;
        mouseX += Input.GetAxisRaw("Mouse X") * sens;
        mouseY -= Input.GetAxisRaw("Mouse Y") * sens;

        mouseY = Mathf.Clamp(mouseY, -90f, 90f);
        transform.localRotation = Quaternion.Euler(mouseY, mouseX, transform.localEulerAngles.z);
        playerT.localRotation = Quaternion.Euler(0, mouseX, 0);

    }
    public void tiltCamera(float t)
    {
        Quaternion currentAngle = Quaternion.Euler(transform.localRotation.eulerAngles.x,
            transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);

        Quaternion lerpedAngle = Quaternion.Euler(transform.localRotation.eulerAngles.x,
            transform.localRotation.eulerAngles.y, t);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, lerpedAngle, smooth * Time.deltaTime);
    }
}
