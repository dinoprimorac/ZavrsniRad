using UnityEngine;
using System.Collections;

public class ProceduralRecoil : MonoBehaviour
{
    [SerializeField] private float kickBack = 0.06f;
    [SerializeField] private float kickAngle = 6f;
    [SerializeField] private float returnTime = 0.08f;  // seconds to return
    [SerializeField] private float snap = 18f;          // smoothing

    private Vector3 restPos;
    private Quaternion restRot;
    private Vector3 targetPos;
    private Quaternion targetRot;

    void Awake()
    {
        restPos = transform.localPosition;
        restRot = transform.localRotation;
        targetPos = restPos;
        targetRot = restRot;
    }

    public void FireKick()
    {
        StopAllCoroutines();
        StartCoroutine(KickRoutine());
    }

    private IEnumerator KickRoutine()
    {
        // instant kick
        targetPos = restPos + new Vector3(0, 0, -kickBack);
        targetRot = restRot * Quaternion.Euler(-kickAngle, 0, 0);

        // hold one frame so you can see it, then return
        yield return null;

        float t = 0f;
        Vector3 startPos = targetPos;
        Quaternion startRot = targetRot;

        while (t < returnTime)
        {
            t += Time.deltaTime;
            float a = Mathf.Clamp01(t / returnTime);
            targetPos = Vector3.Lerp(startPos, restPos, a);
            targetRot = Quaternion.Slerp(startRot, restRot, a);
            yield return null;
        }

        targetPos = restPos;
        targetRot = restRot;
    }

    void LateUpdate()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, snap * Time.deltaTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, snap * Time.deltaTime);
    }
}
