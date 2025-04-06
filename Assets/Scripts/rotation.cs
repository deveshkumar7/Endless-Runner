using UnityEngine;

public class rotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of rotation

    // Update is called once per frame
    void Update()
    {
        // Rotate the sprite around the Z-axis continuously
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
