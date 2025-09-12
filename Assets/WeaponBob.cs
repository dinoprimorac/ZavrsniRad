using UnityEngine;

public class WeaponBob : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerRoot;        // e.g., your Player
    [SerializeField] private CharacterController cc;      // or Rigidbody (see below)

    [Header("Bobbing")]
    [SerializeField] private float frequency = 7.5f;      // how fast it bobs
    [SerializeField] private float amplitudeX = 0.03f;    // left-right sway
    [SerializeField] private float amplitudeY = 0.02f;    // small up-down
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float aimMultiplier = 0.3f;  // scale when aiming
    [SerializeField] private float smooth = 12f;

    [Header("State flags (optional external control)")]
    public bool isSprinting = false;
    public bool isAiming = false;

    private Vector3 _restLocalPos;
    private float _t;

    void Reset()
    {
        if (!playerRoot) playerRoot = transform.root;
        if (!cc) cc = playerRoot ? playerRoot.GetComponent<CharacterController>() : null;
    }

    void Awake() { _restLocalPos = transform.localPosition; }

    void Update()
    {
        // Horizontal speed magnitude (ignore Y)
        float speed = 0f;
        if (cc)
        {
            Vector3 v = cc.velocity; v.y = 0f;
            speed = v.magnitude;
        }
        else if (playerRoot && playerRoot.TryGetComponent<Rigidbody>(out var rb))
        {
            Vector3 v = rb.linearVelocity; v.y = 0f;
            speed = v.magnitude;
        }

        // Normalize to [0..1] “moving” factor (tweak 2.0f if needed)
        float moveFactor = Mathf.Clamp01(speed / 2.0f);
        float mult = (isSprinting ? sprintMultiplier : 1f) * (isAiming ? aimMultiplier : 1f);

        // Advance time proportional to movement
        _t += moveFactor * frequency * Time.deltaTime;

        // Bob pattern: x = sin(t) (L/R), y = |cos(2t)| (subtle rise/fall)
        float x = Mathf.Sin(_t) * amplitudeX * moveFactor * mult;
        float y = Mathf.Abs(Mathf.Cos(_t * 2f)) * amplitudeY * moveFactor * mult;

        Vector3 target = _restLocalPos + new Vector3(x, y, 0f);
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, smooth * Time.deltaTime);
    }

    // Call this if you re-equip or want to snap back immediately
    public void ResetPose()
    {
        _t = 0f;
        transform.localPosition = _restLocalPos;
    }
}
