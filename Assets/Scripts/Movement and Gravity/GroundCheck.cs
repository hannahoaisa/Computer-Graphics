using UnityEngine;

[ExecuteInEditMode]
public class GroundCheck : MonoBehaviour
{
    public Gravity gravity;
    [Tooltip("Maximum distance from the ground.")]
    public float distanceThreshold = .15f;

    [Tooltip("Whether this transform is grounded now.")]
    public bool isGrounded = true;
    /// <summary>
    /// Called when the ground is touched again.
    /// </summary>
    public event System.Action Grounded;

    const float OriginOffset = .05f;
    Vector3 RaycastOrigin => transform.position + gravity.normal * OriginOffset;
    float RaycastDistance => distanceThreshold + OriginOffset;


    void LateUpdate()
    {
        // Check if we are grounded now.
        bool isGroundedNow = Physics.Raycast(RaycastOrigin, -gravity.normal, distanceThreshold);

        // Call event if we were in the air and we are now touching the ground.
        if (isGroundedNow && !isGrounded)
        {
            Grounded?.Invoke();
        }

        // Update isGrounded.
        isGrounded = isGroundedNow;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a line in the Editor to show whether we are touching the ground.
        Debug.DrawLine(RaycastOrigin, RaycastOrigin + Vector3.down * RaycastDistance, isGrounded ? Color.white : Color.red);
    }
}