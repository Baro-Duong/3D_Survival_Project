using UnityEngine;
using UnityEngine.UI;

// Full-screen red overlay that briefly pulses whenever the player takes damage (rabbit bite, or HP
// loss from empty thirst/hunger). Triggered by PlayerStats.TakeDamage() calling Flash().
public class DamageFlash : MonoBehaviour
{
    public static DamageFlash Instance { get; set; }

    public Image flashImage;
    public GameConfig config;

    private float currentAlpha = 0f;

    // Singleton setup
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Jumps the overlay to full flash alpha; Update() fades it back out over damageFlashFadeDuration
    public void Flash()
    {
        currentAlpha = config.damageFlashAlpha;
    }

    // Fades the red overlay back to transparent
    private void Update()
    {
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
