using UnityEngine;
using UnityEngine.UI;

// Full-screen red overlay that briefly pulses whenever the player takes damage (rabbit bite, or HP
// loss from empty thirst/hunger). Triggered by PlayerStats.TakeDamage() calling Flash(amount).
// Damage is accumulated in hpAccumulator; a pulse only fires once a whole HP has been crossed, so
// the flash rate tracks the real HP-loss rate directly (drain at 3 HP/s crosses a whole HP every
// 1/3s -> 3 pulses/s) instead of running on an arbitrary fixed timer. A single large instantaneous
// hit (e.g. a 5 HP rabbit bite) crosses 5 whole HP in one frame, collapsing into one bigger pulse
// via damageFlashIntensityPerHP rather than 5 back-to-back pulses in the same frame.
public class DamageFlash : MonoBehaviour
{
    public static DamageFlash Instance { get; set; }

    public Image flashImage;
    public GameConfig config;

    private float currentAlpha = 0f;
    private float hpAccumulator = 0f;

    // Singleton setup
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Queues damage; Update() turns it into a pulse once a whole HP has accumulated
    public void Flash(float damageAmount)
    {
        hpAccumulator += damageAmount;
    }

    private void Update()
    {
        if (hpAccumulator >= 1f)
        {
            float wholeHpLost = Mathf.Floor(hpAccumulator);
            hpAccumulator -= wholeHpLost;

            float intensityMultiplier = 1f + wholeHpLost * config.damageFlashIntensityPerHP;
            currentAlpha = Mathf.Clamp01(config.damageFlashAlpha * intensityMultiplier);
        }

        if (currentAlpha <= 0f) return;

        currentAlpha = Mathf.Max(0f, currentAlpha - config.damageFlashAlpha / config.damageFlashFadeDuration * Time.deltaTime);

        if (flashImage != null)
        {
            Color c = flashImage.color;
            c.a = currentAlpha;
            flashImage.color = c;
        }
    }
}
