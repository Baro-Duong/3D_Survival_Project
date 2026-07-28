using UnityEngine;
using UnityEngine.UI;

// Attached to the Main Camera in MenuScene. Cycles through a list of fixed-position waypoints: at each
// one the camera stays put and pans (yaws) across panAngle in a random direction, then the screen fades
// to black, the camera cuts (teleports, still hidden) to the next waypoint, and fades back in.
public class MenuCameraRig : MonoBehaviour
{
    private enum CamState { Panning, FadingOut, FadingIn }

    [Header("Waypoints")]
    public Transform[] waypoints;

    [Header("Pan")]
    public float panAngle = 45f;
    public float panDuration = 8f;

    [Header("Fade")]
    public Image fadeOverlay;
    public float fadeDuration = 1f;

    private CamState state = CamState.Panning;
    private float stateTimer = 0f;
    private int currentIndex = 0;

    private Quaternion panStartRot;
    private Quaternion panEndRot;

    // Places the camera at the first waypoint and starts panning
    private void Start()
    {
        MoveToWaypoint(0);
    }

    private void Update()
    {
        stateTimer += Time.deltaTime;

        switch (state)
        {
            case CamState.Panning:
                float panT = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(stateTimer / panDuration));
                transform.rotation = Quaternion.Slerp(panStartRot, panEndRot, panT);

                if (stateTimer >= panDuration)
                {
                    state = CamState.FadingOut;
                    stateTimer = 0f;
                }
                break;

            case CamState.FadingOut:
                SetOverlayAlpha(Mathf.Clamp01(stateTimer / fadeDuration));

                if (stateTimer >= fadeDuration)
                {
                    currentIndex = (currentIndex + 1) % waypoints.Length;
                    MoveToWaypoint(currentIndex);
                    state = CamState.FadingIn;
                    stateTimer = 0f;
                }
                break;

            case CamState.FadingIn:
                SetOverlayAlpha(1f - Mathf.Clamp01(stateTimer / fadeDuration));

                if (stateTimer >= fadeDuration)
                {
                    state = CamState.Panning;
                    stateTimer = 0f;
                }
                break;
        }
    }

    // Teleports the camera to the given waypoint's position and rolls a random pan direction across panAngle
    private void MoveToWaypoint(int index)
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Transform wp = waypoints[index];
        transform.position = wp.position;

        float half = panAngle / 2f;
        Quaternion leftRot = wp.rotation * Quaternion.Euler(0f, -half, 0f);
        Quaternion rightRot = wp.rotation * Quaternion.Euler(0f, half, 0f);

        bool leftToRight = Random.value < 0.5f;
        panStartRot = leftToRight ? leftRot : rightRot;
        panEndRot = leftToRight ? rightRot : leftRot;

        transform.rotation = panStartRot;
    }

    // Sets the fade overlay's opacity (0 = fully visible scene, 1 = fully black)
    private void SetOverlayAlpha(float alpha)
    {
        if (fadeOverlay == null) return;
        Color c = fadeOverlay.color;
        c.a = alpha;
        fadeOverlay.color = c;
    }
}
