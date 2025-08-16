using UnityEngine;

/// <summary>
/// Simple left-right sway/bob for a weapon holder.
/// Attach this to your empty holder GameObject (the parent of the weapon).
/// </summary>
public class WeaponLeftRightSwey : MonoBehaviour
{
    [Header("Sway")]
    [Tooltip("How far left/right to move (in local units/meters).")]
    public float amplitude = 0.03f;

    [Tooltip("How fast the sway oscillates (cycles per second).")]
    public float frequency = 6f;

    [Tooltip("Local axis to sway along (default = X/right).")]
    public Vector3 localAxis = Vector3.right;

    [Header("When to sway")]
    [Tooltip("If true, sway only happens while the player is moving.")]
    public bool onlyWhileMoving = true;

    [Tooltip("Movement speed threshold to consider 'moving'.")]
    public float moveThreshold = 0.05f;

    [Tooltip("How quickly the holder returns to its rest position when stopping.")]
    public float returnSpeed = 12f;

    [Tooltip("Scales sway amount by movement speed a bit for nicer feel.")]
    public bool scaleBySpeed = true;

    // Cached
    Vector3 _restLocalPos;
    Transform _root;
    Vector3 _lastRootPos;

    CharacterController _cc;
    Rigidbody _rb;

    void Awake()
    {
        _restLocalPos = transform.localPosition;
        _root = transform.root != null ? transform.root : transform;
        _lastRootPos = _root.position;

        // Try to auto-detect common movement components on parents
        _cc = _root.GetComponentInChildren<CharacterController>();
        if (_cc == null) _cc = _root.GetComponent<CharacterController>();
        _rb = _root.GetComponentInChildren<Rigidbody>();
        if (_rb == null) _rb = _root.GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        transform.localPosition = _restLocalPos;
    }

    void OnDisable()
    {
        transform.localPosition = _restLocalPos;
    }

    void Update()
    {
        float speed = EstimateSpeed();

        bool isMoving = speed > moveThreshold || !onlyWhileMoving;
        float t = Time.time * frequency;

        if (isMoving)
        {
            float amount = amplitude;
            if (scaleBySpeed)
            {
                // Lightly scale with speed so faster movement = slightly bigger sway
                float s = Mathf.Clamp01((speed - moveThreshold) * 0.75f); // tweak factor
                amount *= Mathf.Lerp(0.7f, 1.2f, s);
            }

            Vector3 offset = (transform.TransformDirection(localAxis.normalized) - transform.position).normalized; 
            // The above ensures axis is in LOCAL space:
            offset = localAxis.normalized * (Mathf.Sin(t) * amount);

            transform.localPosition = _restLocalPos + offset;
        }
        else
        {
            // Smoothly return to rest when not moving
            transform.localPosition = Vector3.Lerp(transform.localPosition, _restLocalPos, returnSpeed * Time.deltaTime);
        }
    }

    float EstimateSpeed()
    {
        // Prefer CharacterController velocity if present
        if (_cc != null)
        {
            Vector3 v = _cc.velocity;
            v.y = 0f;
            return v.magnitude;
        }

        // Next, Rigidbody linear velocity if present
        if (_rb != null)
        {
            Vector3 v = _rb.linearVelocity;
            v.y = 0f;
            return v.magnitude;
        }

        // Fallback: root world-position delta
        Vector3 p = _root.position;
        float speed = (_lastRootPos - p).magnitude / Mathf.Max(Time.deltaTime, 0.0001f);
        _lastRootPos = p;
        return speed;
    }
}
