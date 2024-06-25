using UnityEngine;

public class SimpleEyeTracking : MonoBehaviour
{
    void Start()
    {
        // Check if Oculus headset is present
        if (!OVRManager.isHmdPresent)
        {
            Debug.LogError("Oculus headset not detected.");
            return;
        }

        // Check if eye tracking is supported and enabled
        if (!OVRPlugin.eyeTrackingSupported)
        {
            Debug.LogError("Eye tracking is not supported on this device.");
            return;
        }

        if (!OVRPlugin.eyeTrackingEnabled)
        {
            Debug.LogError("Eye tracking is not enabled.");
            return;
        }

        Debug.Log("Eye tracking is supported and enabled.");
    }

    void Update()
    {
        // Attempt to get the eye tracking data
        OVRPlugin.EyeGazesState eyeGazesState = new OVRPlugin.EyeGazesState();
        OVRPlugin.GetEyeGazesState(OVRPlugin.Step.Render, -1, ref eyeGazesState);

        // Convert OVRPlugin.Quatf to UnityEngine.Quaternion
        Quaternion leftEyeOrientation = new Quaternion(
            eyeGazesState.EyeGazes[0].Pose.Orientation.x,
            eyeGazesState.EyeGazes[0].Pose.Orientation.y,
            eyeGazesState.EyeGazes[0].Pose.Orientation.z,
            eyeGazesState.EyeGazes[0].Pose.Orientation.w
        );

        Quaternion rightEyeOrientation = new Quaternion(
            eyeGazesState.EyeGazes[1].Pose.Orientation.x,
            eyeGazesState.EyeGazes[1].Pose.Orientation.y,
            eyeGazesState.EyeGazes[1].Pose.Orientation.z,
            eyeGazesState.EyeGazes[1].Pose.Orientation.w
        );

        // Log the gaze direction for the left and right eye
        Vector3 leftEyeDirection = leftEyeOrientation * Vector3.forward;
        Vector3 rightEyeDirection = rightEyeOrientation * Vector3.forward;

        Debug.Log($"Left Eye Gaze Direction: {leftEyeDirection}");
        Debug.Log($"Right Eye Gaze Direction: {rightEyeDirection}");
    }
}
