using UnityEngine;
using System;

public enum BloodVfxColorMode
{
    Red,
    Green
}

[DisallowMultipleComponent]
public class BloodVfx : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particleSystems;
    [SerializeField] private float fallbackLifetime = 1.2f;
    [SerializeField] private bool deactivateOnComplete = true;

    private float completeTime;
    private bool isPlaying;

    public Action<BloodVfx> OnComplete { get; set; }

    private void Awake()
    {
        RefreshParticleSystems();
    }

    private void Reset()
    {
        RefreshParticleSystems();
    }

    private void Update()
    {
        if (!isPlaying || Time.time < completeTime) return;

        isPlaying = false;

        if (OnComplete != null)
        {
            OnComplete.Invoke(this);
            return;
        }

        if (deactivateOnComplete)
            gameObject.SetActive(false);
    }

    public void Play(Vector3 position, Vector2 direction)
    {
        Play(position, direction, BloodVfxColorMode.Red);
    }

    public void Play(Vector3 position, Vector2 direction, BloodVfxColorMode colorMode)
    {
        transform.position = position;
        SetDirection(direction);

        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        RefreshParticleSystems();
        ApplyColorMode(colorMode);

        var maxLifetime = fallbackLifetime;
        foreach (var ps in particleSystems)
        {
            if (ps == null) continue;

            ps.Clear(true);
            ps.Play(true);

            var main = ps.main;
            maxLifetime = Mathf.Max(maxLifetime, main.duration + main.startLifetime.constantMax);
        }

        isPlaying = true;
        completeTime = Time.time + maxLifetime;
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.001f) return;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    [ContextMenu("Refresh Particle Systems")]
    public void RefreshParticleSystems()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>(true);
    }

    public void StopImmediate()
    {
        isPlaying = false;
        RefreshParticleSystems();

        foreach (var ps in particleSystems)
        {
            if (ps == null) continue;

            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            ps.Clear(true);
        }
    }

    private void ApplyColorMode(BloodVfxColorMode colorMode)
    {
        foreach (var ps in particleSystems)
        {
            if (ps == null) continue;

            ApplyParticleColor(ps, colorMode);
        }
    }

    private static void ApplyParticleColor(ParticleSystem ps, BloodVfxColorMode colorMode)
    {
        var isMist = ps.name.Contains("Mist");
        var isDrops = ps.name.Contains("Drops");

        Color start;
        Color middle;
        Color end;

        if (colorMode == BloodVfxColorMode.Green)
        {
            start = isMist ? new Color(0.25f, 0.95f, 0.10f, 0.42f) : new Color(0.08f, 0.78f, 0.04f, 1f);
            middle = isDrops ? new Color(0.03f, 0.36f, 0.02f, 0.9f) : new Color(0.05f, 0.46f, 0.02f, isMist ? 0.22f : 0.82f);
            end = new Color(0.01f, 0.12f, 0.00f, 0f);
        }
        else
        {
            start = isMist ? new Color(0.95f, 0.02f, 0.00f, 0.42f) : new Color(0.88f, 0.00f, 0.00f, 1f);
            middle = isDrops ? new Color(0.28f, 0.00f, 0.00f, 0.9f) : new Color(0.42f, 0.00f, 0.00f, isMist ? 0.22f : 0.82f);
            end = new Color(0.12f, 0.00f, 0.00f, 0f);
        }

        var main = ps.main;
        main.startColor = new ParticleSystem.MinMaxGradient(start, middle);

        var color = ps.colorOverLifetime;
        color.enabled = true;

        var gradient = new Gradient();
        gradient.SetKeys(
            new[]
            {
                new GradientColorKey(start, 0f),
                new GradientColorKey(middle, 0.45f),
                new GradientColorKey(end, 1f)
            },
            new[]
            {
                new GradientAlphaKey(start.a, 0f),
                new GradientAlphaKey(middle.a, 0.45f),
                new GradientAlphaKey(end.a, 1f)
            }
        );

        color.color = new ParticleSystem.MinMaxGradient(gradient);
    }
}
