using UnityEngine;
using System.IO;
using System.Threading;

public class EyeLogger : MonoBehaviour
{
    private StreamWriter logger;
    private string logFilePath;

    void Start()
    {
        // Use a fixed path for the log directory
        string logDirectory = @"C:\Users\cs-reu\EyeTracking\LoggerData";
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }

        // Append the file name with the date and time
        logFilePath = Path.Combine(logDirectory, "eye_positions_log_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt");

        InitializeLoggerWithRetries();
    }

    private void InitializeLoggerWithRetries()
    {
        int maxRetries = 5;
        int retryDelay = 100;
        int retryCount = 0;

        while (retryCount < maxRetries)
        {
            try
            {
                FileStream fileStream = new FileStream(logFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                logger = new StreamWriter(fileStream);
                logger.WriteLine("Time,LeftEyeX,LeftEyeY,LeftEyeZ,RightEyeX,RightEyeY,RightEyeZ");
                InvokeRepeating("LogEyePosition", 0f, 0.1f);
                break;
            }
            catch (IOException e)
            {
                retryCount++;
                Debug.LogError($"Error initializing logger (attempt {retryCount}): {e.Message}");
                Thread.Sleep(retryDelay);
            }
        }

        if (logger == null)
        {
            Debug.LogError("Failed to initialize logger after multiple attempts.");
        }
    }

    void LogEyePosition()
    {
        if (logger == null)
            return;

        try
        {
            OVRPlugin.EyeGazesState eyeGazesState = new OVRPlugin.EyeGazesState();
            if (OVRPlugin.GetEyeGazesState(OVRPlugin.Step.Render, -1, ref eyeGazesState))
            {
                // Check if eyeGazesState contains valid data
                if (eyeGazesState.EyeGazes.Length > 1)
                {
                    // Convert OVRPlugin.Vector3f to UnityEngine.Vector3
                    Vector3 leftEyePosition = new Vector3(
                        eyeGazesState.EyeGazes[0].Pose.Position.x,
                        eyeGazesState.EyeGazes[0].Pose.Position.y,
                        eyeGazesState.EyeGazes[0].Pose.Position.z
                    );

                    Vector3 rightEyePosition = new Vector3(
                        eyeGazesState.EyeGazes[1].Pose.Position.x,
                        eyeGazesState.EyeGazes[1].Pose.Position.y,
                        eyeGazesState.EyeGazes[1].Pose.Position.z
                    );

                    logger.WriteLine($"{Time.time},{leftEyePosition.x},{leftEyePosition.y},{leftEyePosition.z},{rightEyePosition.x},{rightEyePosition.y},{rightEyePosition.z}");
                }
                else
                {
                    Debug.LogError("Invalid eye gaze data.");
                }
            }
            else
            {
                Debug.LogError("Failed to get eye gaze data.");
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"Error writing to log file: {e.Message}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Unexpected error: {ex.Message}");
        }
    }

    void OnDestroy()
    {
        try
        {
            if (logger != null)
            {
                logger.Close();
                logger = null;
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"Error closing log file: {e.Message}");
        }
    }
}
