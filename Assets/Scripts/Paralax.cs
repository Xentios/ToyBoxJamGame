using UnityEngine;

public class Paralax : MonoBehaviour
{
    public Transform[] layers;  // Assign your parallax layers in the Unity Editor
    public float[] speeds;      // Speed at which each layer moves

    private Transform cameraTransform;
    private Vector3 previousCameraPosition;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
    }

    void Update()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            // Calculate the parallax position for each layer
            float parallax = (previousCameraPosition.x - cameraTransform.position.x) * speeds[i];

            // Calculate the target position for the layer
            float targetX = layers[i].position.x + parallax;

            // Create a target position vector
            Vector3 targetPosition = new Vector3(targetX, layers[i].position.y, layers[i].position.z);

            // Interpolate towards the target position for a smooth effect
            layers[i].position = Vector3.Lerp(layers[i].position, targetPosition, Time.deltaTime);
        }

        // Update the previous camera position
        previousCameraPosition = cameraTransform.position;
    }
}
