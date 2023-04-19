using UnityEngine;
using UnityEngine.Events;

public class HingeJointDevideListener : MonoBehaviour
{
    //angle threshold to trigger if we reached limit
    public float angleBetweenThreshold = 1f;

    public UnityEvent[] Events;
    private HingeJoint hinge;
    float devideAmount;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();

        float difference = Mathf.Abs(hinge.limits.min - hinge.limits.max);
        devideAmount = difference / (Events.Length  * 2);

    }

    private void FixedUpdate()
    {

        float currentAngle = hinge.limits.min + devideAmount;
        int eventIndex = 0;
        while (currentAngle < hinge.limits.max)
        {
            float angleDifference = Mathf.Abs(  Mathf.Min(hinge.angle, currentAngle) - Mathf.Max(hinge.angle, currentAngle));

            if (angleDifference < angleBetweenThreshold)
                Events[eventIndex].Invoke();

            currentAngle += devideAmount * 2;
            eventIndex++;
        }
    }
}
