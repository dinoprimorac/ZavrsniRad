using UnityEngine;

public class WeaponBob: MonoBehaviour
{
    [Header("Player ref")]
    [SerializeField] private CharacterController characterController;

    [Header("Arc Motion")]
    [Tooltip("Oscillations per second (0→π→0). 1 = full left↔right takes 2s.")]
    [SerializeField] private float cyclesPerSecond = 1.5f;
    [Tooltip("Arc radii; set equal for a perfect semicircle.")]
    [SerializeField] private float radiusX = 0.05f;
    [SerializeField] private float radiusY = 0.05f;
    [Tooltip("Top arc (y ≥ 0). Disable for bottom arc (y ≤ 0).")]
    [SerializeField] private bool useTopArc = true;

    [Header("Responsiveness")]
    [Tooltip("Speed at which sway reaches full radius.")]
    [SerializeField] private float speedForFullSway = 3.0f;
    [SerializeField] private float smooth = 12f;

    [Header("Modifiers")]
    public bool isSprinting = false;
    public bool isAiming = false;
    [SerializeField] private float sprintMultiplier = 1.3f;
    [SerializeField] private float aimMultiplier = 0.35f;

    private Vector3 restPos;
    private float phase; 

    void Awake()
    {
        restPos = transform.localPosition;
    }

    private void Start()
    {
        characterController = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        if (!characterController) return;

        Vector3 v = characterController.velocity; v.y = 0f;
        float speed = v.magnitude;

        float move = Mathf.Clamp01(speed / Mathf.Max(0.01f, speedForFullSway));
        float mult = (isSprinting ? sprintMultiplier : 1f) * (isAiming ? aimMultiplier : 1f);
        float mf = move * mult;

        if (mf > 0.001f) phase += cyclesPerSecond * Time.deltaTime;

        float t  = Mathf.PingPong(phase, 1f);
        float th = Mathf.Lerp(0f, Mathf.PI, t); 
        float x  = Mathf.Cos(th) * radiusX * mf;  
        float y  = Mathf.Sin(th) * radiusY * mf;  
        if (!useTopArc) y = -y;

        Vector3 target = restPos + new Vector3(x, y, 0f);

        transform.localPosition = Vector3.Lerp(transform.localPosition, target, smooth * Time.deltaTime);
        if (mf < 0.001f)
            transform.localPosition = Vector3.Lerp(transform.localPosition, restPos, smooth * Time.deltaTime);
    }

    public void ResetPose()
    {
        phase = 0f;
        transform.localPosition = restPos;
    }
}
