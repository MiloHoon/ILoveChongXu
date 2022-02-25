using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Range(1, 10)]
    public float smoothFactor;

    private Transform playerTransform;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        //Store Current Camera's Position In Variable "temporaryPosition"
        Vector3 temporaryPosition = transform.position;
        //Set Camera's Position.X & Position.Y To Be Equal To The Player's Position.X & Position.Y
        temporaryPosition.x = playerTransform.position.x;
        temporaryPosition.y = playerTransform.position.y;
        //Makes The Camera Smoother
        Vector3 smoothPosition = Vector3.Lerp(transform.position, temporaryPosition, smoothFactor * Time.fixedDeltaTime);
        //Set Back Camera's "smoothPosition" To The Camera's Current Position
        transform.position = smoothPosition;
    }
}