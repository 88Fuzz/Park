using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    public Range neckRotationMovement = new Range(-50, 50);
    public Range neckPitchMovement = new Range(60,330);

    private Transform cameraTransform;
    private Vector3 bodyAngle;

    public void Start()
    {
        cameraTransform = Camera.main.transform;
        bodyAngle = new Vector3();
    }

    public void LateUpdate()
    {
        bodyAngle = transform.eulerAngles;
        Vector3 cameraAngle = cameraTransform.eulerAngles;
        //TODO there should be some lerping going on here with the new head positions
        bodyAngle.y += getClampedDifference(cameraAngle.y, bodyAngle.y);
        bodyAngle.x = getHalfClampedValue(cameraAngle.x);
        transform.eulerAngles = bodyAngle;
    }

    //TODO dear god these angle things are gross, figure out a better way to do them please.
    private float getHalfClampedValue(float cameraAngle)
    {
        if (cameraAngle > 180)
        {
            return Mathf.Clamp(cameraAngle, neckPitchMovement.max, 360);
        }

        return Mathf.Clamp(cameraAngle, 0, neckPitchMovement.min);
    }

    private float getClampedDifference(float cameraAngle, float bodyAngle)
    {
        float naturalAngle = cameraAngle - bodyAngle;
        float normalizedAngle = getNormalizedCameraAngle(cameraAngle, bodyAngle) - bodyAngle;

        float diffAngle;
        if (Mathf.Abs(naturalAngle) < Mathf.Abs(normalizedAngle))
            diffAngle = naturalAngle;
        else
            diffAngle = normalizedAngle;

        return Mathf.Clamp(diffAngle, neckRotationMovement.min, neckRotationMovement.max);
    }

    //TODO this should be moved to a better place for it, like a mathUtils class or something?
    private float getNormalizedCameraAngle(float cameraAngle, float bodyAngle)
    {
        if (bodyAngle < 180)
        {
            if (cameraAngle > 180)
                return cameraAngle - 360;
        }
        else if (bodyAngle > 180)
        {
            if (cameraAngle < 180)
                return 360 + cameraAngle;
        }

        return cameraAngle;
    }
    private float specialAngleClamp(float angle)
    {
        if (angle > neckRotationMovement.max && angle < 180)
            return neckRotationMovement.max;
        else if (angle >= 180 && angle < neckRotationMovement.min)
            return neckRotationMovement.min;
        return angle;
    }
}