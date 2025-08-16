using UnityEngine;

/// <summary>
/// Left-right sway + slight up-down bob for a weapon holder.
/// Attach to your empty holder GameObject (parent of the weapon).
/// </summary>
public class WeaponLeftRightSwey : MonoBehaviour
{
    [Header("Horizontal (Left–Right)")]
    [Tooltip("How far to move left/right (local units).")]
    public float horizontalAmplitude = 0.03f;
    [Tooltip("How fast the left/right sway cycles (Hz).")]
    public float horizontalFrequency = 6f;
    [Tooltip("Local axis to sway along (usually X).")]
    public Vector3 horizontalAxis = Vector3.right;

    [Header("Vertical (Up–Down)")]
    [Tooltip("How far to move up/down (keep tiny).")]
    public float verticalAmplitude = 0.01f; // 'just a little bit'
    [Tooltip("How fast the up/down bob cycles (Hz).")]
    public float verticalFrequency = 12f;
    [Tooltip("Phase offset (radians) so vertical peaks don't align with horizontal.")]
    [Range(0f, 6.283185f)] public float verticalPhase = 1.5707963f; // ~90°

    [Header("When to animate")]
    [Tooltip("If true, animate only while the player is moving.")]
    public bool onlyWhileMoving = true;
    [Tooltip("Speed threshold considered 'moving'.")]
    public float moveThreshold = 0.05f;
    [Tooltip("How quickly it returns to rest when stopping.")]
    public float returnSpeed = 12f;
    [Tooltip("Lightly scale animation with movement speed.")]
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

        _cc = _root.GetComponentInChildren<CharacterController>();
        if (_cc == null) _cc = _root.GetComponent<CharacterController>();

        _rb = _root.GetComponentInChildren<Rigidbody>();
        if (_rb == null) _rb = _root.GetComponent<Rigidbody>();
    }

    void OnEnable()  { transform.localPosition = _restLocalPos; }
    void OnDisable() { transform.localPosition = _restLocalPos; }

    void Update()
    {
        float speed = EstimateSpeed();
        bool isMoving = !onlyWhileMoving || speed > moveThreshold;

        if (isMoving)
        {
            float t = Time.time;

            // Gentle speed-based scaling (optional)
            float scale = 1f;
            if (scaleBySpeed)
            {
                float s = Mathf.Clamp01((speed - moveThreshold) * 0.75f);
                scale = Mathf.Lerp(0.7f, 1.2f, s);
            }

            // Normalize local axes (fallbacks if user leaves them at zero)
            Vector3 hAxis = (horizontalAxis.sqrMagnitude > 0f ? horizontalAxis : Vector3.right).normalized;
            Vector3 vAxis = Vector3.up; // fixed up/down axis (local)

            // Compute offsets in LOCAL space
            float h = Mathf.Sin(t * horizontalFrequency) * horizontalAmplitude * scale;
            float v = Mathf.Sin(t * verticalFrequency + verticalPhase) * verticalAmplitude * scale;

            Vector3 offset = hAxis * h + vAxis * v;
            transform.localPosition = _restLocalPos + offset;
        }
        else
        {
            // Smoothly return to rest when not moving
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                _restLocalPos,
                returnSpeed * Time.deltaTime
            );
        }
    }

    float EstimateSpeed()
    {
        if (_cc != null)
        {
            Vector3 v = _cc.velocity; v.y = 0f;
            return v.magnitude;
        }

        if (_rb != null)
        {
            Vector3 v = _rb.linearVelocity; v.y = 0f;
            return v.magnitude;
        }

        Vector3 p = _root.position;
        float speed = (p - _lastRootPos).magnitude / Mathf.Max(Time.deltaTime, 0.0001f);
        _lastRootPos = p;
        return speed;
        }
}
