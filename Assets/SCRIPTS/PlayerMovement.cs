using UnityEngine;

/**
 * Movement of the player
 * Regroups every function or properties linked to the movement of the player.
 * @author tom_samaille
 */
public class PlayerMovement : MonoBehaviour
{
    /** 
     * Camera refers to our player linked camera.
     */
    public Camera m_Camera;
    /** 
     * Speed refers to our player movement speed.
     */
    public float m_Speed = 5f;
    /** 
     * RigidBody refers to the rigidbody component of the object.
     */
    private Rigidbody m_RigidBody;

    /**
     * Start is called before the first frame update
     * @author tom_samaille
     */
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    /**
     * FixedUpdate is called once per frame. Used for physics
     * @author tom_samaille
     */
    void FixedUpdate()
    {
        // Get Input for axis
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Calculate the forward vector
        Vector3 camForward_Dir = Vector3.Scale(m_Camera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = v * camForward_Dir + h * m_Camera.transform.right;
        if (move.magnitude > 1f)
        {
            move.Normalize();
        }
        m_RigidBody.AddForce(move * m_Speed);
    }
}
