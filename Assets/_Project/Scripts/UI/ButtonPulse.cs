using UnityEngine;

// Attach to any UI element (e.g. ToolLibraryBTN) to make it periodically scale up and back down,
// drawing the player's eye to it so it doesn't look like a static, non-interactable icon.
public class ButtonPulse : MonoBehaviour
{
    private enum PulseState { Idle, Pulsing }

    [Header("Pulse")]
    public float scaleAmount = 1.15f;   // peak scale multiplier at the top of the pulse
    public float pulseDuration = 0.6f;  // time for one grow+shrink cycle
    public float idleDuration = 2f;     // pause between pulses

    private PulseState state = PulseState.Idle;
    private float stateTimer = 0f;
    private Vector3 baseScale;

    private void Start()
    {
        baseScale = transform.localScale;
    }

    private void Update()
    {
        stateTimer += Time.deltaTime;

        switch (state)
        {
            case PulseState.Idle:
                if (stateTimer >= idleDuration)
                {
                    state = PulseState.Pulsing;
                    stateTimer = 0f;
                }
                break;

            case PulseState.Pulsing:
                float t = Mathf.Clamp01(stateTimer / pulseDuration);
                float scaleT = Mathf.Sin(t * Mathf.PI); // 0 -> 1 -> 0, smooth grow then shrink
                transform.localScale = baseScale * Mathf.Lerp(1f, scaleAmount, scaleT);

                if (stateTimer >= pulseDuration)
                {
                    transform.localScale = baseScale;
                    state = PulseState.Idle;
                    stateTimer = 0f;
                }
                break;
        }
    }
}
