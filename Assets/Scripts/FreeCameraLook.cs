using UnityEngine;

public class FreeCameraLook : MonoBehaviour
{
    public float rotationSpeed = 0.2f;

    private Vector2 lastPos;
    private bool dragging = false;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                lastPos = t.position;
                dragging = true;
            }
            else if (t.phase == TouchPhase.Moved && dragging)
            {
                Vector2 delta = t.position - lastPos;

                transform.Rotate(Vector3.up, delta.x * rotationSpeed, Space.World);
                transform.Rotate(Vector3.right, -delta.y * rotationSpeed, Space.Self);

                lastPos = t.position;
            }
            else if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
            {
                dragging = false;
            }
        }
    }
}