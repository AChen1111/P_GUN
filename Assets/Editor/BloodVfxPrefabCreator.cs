using System.IO;
using UnityEditor;
using UnityEngine;

public static class BloodVfxPrefabCreator
{
    private const string VfxFolderPath = "Assets/Prefab/VFX";
    private const string PrefabPath = VfxFolderPath + "/BloodBurst.prefab";
    private const string ResourcesFolderPath = "Assets/Resources/VFX";
    private const string ResourcesPrefabPath = ResourcesFolderPath + "/BloodBurst.prefab";
    private const string FallbackMaterialPath = VfxFolderPath + "/BloodParticle.mat";
    private const string ParticleTexturePath = VfxFolderPath + "/BloodParticleDot.png";

    [InitializeOnLoadMethod]
    private static void AutoCreateDefaultPrefab()
    {
        EditorApplication.delayCall += () =>
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode) return;

            if (AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath) == null)
            {
                CreateBloodBurstPrefab(false);
                return;
            }

            RepairBloodBurstPrefab(PrefabPath);
            EnsureResourcesPrefab();
            RepairBloodBurstPrefab(ResourcesPrefabPath);
        };
    }

    [MenuItem("Tools/P_GUN/VFX/Create Blood Burst Prefab")]
    public static void CreateBloodBurstPrefabFromMenu()
    {
        CreateBloodBurstPrefab(true);
    }

    [MenuItem("Tools/P_GUN/VFX/Spawn Blood Burst Preview")]
    public static void SpawnBloodBurstPreview()
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
        if (prefab == null)
        {
            CreateBloodBurstPrefab(false);
            prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
        }

        if (prefab == null)
        {
            Debug.LogError("BloodBurst prefab create failed.");
            return;
        }

        var instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        if (instance == null) return;

        instance.transform.position = Vector3.zero;
        var bloodVfx = instance.GetComponent<BloodVfx>();
        if (bloodVfx != null)
        {
            bloodVfx.Play(Vector3.zero, Vector2.right);
        }

        Selection.activeGameObject = instance;
        EditorGUIUtility.PingObject(instance);
    }

    private static void CreateBloodBurstPrefab(bool selectAfterCreate)
    {
        EnsureProjectFolder("Assets", "Prefab");
        EnsureProjectFolder("Assets/Prefab", "VFX");

        DeleteAssetIfExists(PrefabPath);
        DeleteAssetIfExists(ResourcesPrefabPath);

        var material = GetParticleMaterial();
        var root = new GameObject("BloodBurst", typeof(BloodVfx));

        var spray = CreateParticleSystem(root.transform, "BloodSpray");
        ConfigureSpray(spray, material);

        var drops = CreateParticleSystem(root.transform, "BloodDrops");
        ConfigureDrops(drops, material);

        var mist = CreateParticleSystem(root.transform, "BloodMist");
        ConfigureMist(mist, material);

        var bloodVfx = root.GetComponent<BloodVfx>();
        AssignParticleSystems(bloodVfx, spray, drops, mist);

        var prefab = PrefabUtility.SaveAsPrefabAsset(root, PrefabPath, out var success);
        Object.DestroyImmediate(root);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        if (!success || prefab == null)
        {
            Debug.LogError($"Create BloodBurst prefab failed: {PrefabPath}");
            return;
        }

        Debug.Log($"Created BloodBurst prefab: {PrefabPath}");
        EnsureResourcesPrefab();

        if (!selectAfterCreate) return;

        Selection.activeObject = prefab;
        EditorGUIUtility.PingObject(prefab);
    }

    private static void RepairBloodBurstPrefab(string prefabPath)
    {
        if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) == null) return;

        var root = PrefabUtility.LoadPrefabContents(prefabPath);
        var material = GetParticleMaterial();
        var changed = false;

        foreach (var ps in root.GetComponentsInChildren<ParticleSystem>(true))
        {
            var velocity = ps.velocityOverLifetime;
            if (velocity.enabled && velocity.z.mode != ParticleSystemCurveMode.TwoConstants)
            {
                velocity.z = new ParticleSystem.MinMaxCurve(0f, 0f);
                changed = true;
            }

            var renderer = ps.GetComponent<ParticleSystemRenderer>();
            if (renderer != null && renderer.sharedMaterial != material)
            {
                renderer.sharedMaterial = material;
                changed = true;
            }
        }

        if (changed)
        {
            PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
            AssetDatabase.SaveAssets();
            Debug.Log($"Repaired BloodBurst prefab: {prefabPath}");
        }

        PrefabUtility.UnloadPrefabContents(root);
    }

    private static void EnsureResourcesPrefab()
    {
        if (AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath) == null) return;

        EnsureProjectFolder("Assets", "Resources");
        EnsureProjectFolder("Assets/Resources", "VFX");
        DeleteAssetIfExists(ResourcesPrefabPath);

        if (AssetDatabase.CopyAsset(PrefabPath, ResourcesPrefabPath))
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"Created runtime Resources BloodBurst prefab: {ResourcesPrefabPath}");
        }
    }

    private static void DeleteAssetIfExists(string assetPath)
    {
        if (AssetDatabase.LoadAssetAtPath<Object>(assetPath) == null) return;

        AssetDatabase.DeleteAsset(assetPath);
    }

    private static ParticleSystem CreateParticleSystem(Transform parent, string name)
    {
        var go = new GameObject(name, typeof(ParticleSystem));
        go.transform.SetParent(parent, false);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
        return go.GetComponent<ParticleSystem>();
    }

    private static void ConfigureSpray(ParticleSystem ps, Material material)
    {
        var main = ps.main;
        main.duration = 0.46f;
        main.loop = false;
        main.playOnAwake = false;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.startLifetime = new ParticleSystem.MinMaxCurve(0.14f, 0.36f);
        main.startSpeed = 0f;
        main.startSize = new ParticleSystem.MinMaxCurve(0.026f, 0.072f);
        main.startColor = new ParticleSystem.MinMaxGradient(new Color(0.42f, 0.00f, 0.00f, 1f), new Color(0.95f, 0.03f, 0.01f, 1f));
        main.gravityModifier = new ParticleSystem.MinMaxCurve(0.10f, 0.32f);
        main.maxParticles = 48;

        var emission = ps.emission;
        emission.enabled = true;
        emission.rateOverTime = 0f;
        emission.SetBursts(new[] { new ParticleSystem.Burst(0f, (short)14, (short)22) });

        var shape = ps.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Circle;
        shape.radius = 0.026f;

        var velocity = ps.velocityOverLifetime;
        velocity.enabled = true;
        velocity.space = ParticleSystemSimulationSpace.Local;
        velocity.x = new ParticleSystem.MinMaxCurve(2.4f, 5.2f);
        velocity.y = new ParticleSystem.MinMaxCurve(-1.1f, 1.1f);
        velocity.z = new ParticleSystem.MinMaxCurve(0f, 0f);

        ConfigureSizeOverLifetime(ps, 1f, 0.65f, 0.08f);
        ConfigureColorOverLifetime(ps, new Color(0.88f, 0.00f, 0.00f, 1f), new Color(0.42f, 0.00f, 0.00f, 0.82f), new Color(0.12f, 0.00f, 0.00f, 0f));
        ConfigureRenderer(ps, material, 82);
    }

    private static void ConfigureDrops(ParticleSystem ps, Material material)
    {
        var main = ps.main;
        main.duration = 0.62f;
        main.loop = false;
        main.playOnAwake = false;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.startLifetime = new ParticleSystem.MinMaxCurve(0.28f, 0.60f);
        main.startSpeed = 0f;
        main.startSize = new ParticleSystem.MinMaxCurve(0.014f, 0.042f);
        main.startColor = new ParticleSystem.MinMaxGradient(new Color(0.34f, 0.00f, 0.00f, 1f), new Color(0.80f, 0.00f, 0.00f, 1f));
        main.gravityModifier = new ParticleSystem.MinMaxCurve(0.30f, 0.58f);
        main.maxParticles = 32;

        var emission = ps.emission;
        emission.enabled = true;
        emission.rateOverTime = 0f;
        emission.SetBursts(new[] { new ParticleSystem.Burst(0f, (short)7, (short)12) });

        var shape = ps.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Circle;
        shape.radius = 0.020f;

        var velocity = ps.velocityOverLifetime;
        velocity.enabled = true;
        velocity.space = ParticleSystemSimulationSpace.Local;
        velocity.x = new ParticleSystem.MinMaxCurve(1.0f, 3.0f);
        velocity.y = new ParticleSystem.MinMaxCurve(-1.2f, 1.2f);
        velocity.z = new ParticleSystem.MinMaxCurve(0f, 0f);

        ConfigureSizeOverLifetime(ps, 1f, 0.9f, 0.35f);
        ConfigureColorOverLifetime(ps, new Color(0.62f, 0.00f, 0.00f, 1f), new Color(0.28f, 0.00f, 0.00f, 0.9f), new Color(0.08f, 0.00f, 0.00f, 0f));
        ConfigureRenderer(ps, material, 81);
    }

    private static void ConfigureMist(ParticleSystem ps, Material material)
    {
        var main = ps.main;
        main.duration = 0.38f;
        main.loop = false;
        main.playOnAwake = false;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.startLifetime = new ParticleSystem.MinMaxCurve(0.16f, 0.32f);
        main.startSpeed = 0f;
        main.startSize = new ParticleSystem.MinMaxCurve(0.040f, 0.095f);
        main.startColor = new ParticleSystem.MinMaxGradient(new Color(0.62f, 0.00f, 0.00f, 0.45f), new Color(0.95f, 0.02f, 0.00f, 0.62f));
        main.gravityModifier = 0f;
        main.maxParticles = 24;

        var emission = ps.emission;
        emission.enabled = true;
        emission.rateOverTime = 0f;
        emission.SetBursts(new[] { new ParticleSystem.Burst(0f, (short)4, (short)8) });

        var shape = ps.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Circle;
        shape.radius = 0.034f;

        var velocity = ps.velocityOverLifetime;
        velocity.enabled = true;
        velocity.space = ParticleSystemSimulationSpace.Local;
        velocity.x = new ParticleSystem.MinMaxCurve(0.4f, 1.1f);
        velocity.y = new ParticleSystem.MinMaxCurve(-0.7f, 0.7f);
        velocity.z = new ParticleSystem.MinMaxCurve(0f, 0f);

        ConfigureSizeOverLifetime(ps, 0.55f, 1f, 1.35f);
        ConfigureColorOverLifetime(ps, new Color(0.95f, 0.02f, 0.00f, 0.42f), new Color(0.48f, 0.00f, 0.00f, 0.22f), new Color(0.12f, 0.00f, 0.00f, 0f));
        ConfigureRenderer(ps, material, 80);
    }

    private static void ConfigureSizeOverLifetime(ParticleSystem ps, float start, float middle, float end)
    {
        var size = ps.sizeOverLifetime;
        size.enabled = true;
        var curve = new AnimationCurve(
            new Keyframe(0f, start),
            new Keyframe(0.45f, middle),
            new Keyframe(1f, end)
        );
        size.size = new ParticleSystem.MinMaxCurve(1f, curve);
    }

    private static void ConfigureColorOverLifetime(ParticleSystem ps, Color start, Color middle, Color end)
    {
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

    private static void ConfigureRenderer(ParticleSystem ps, Material material, int sortingOrder)
    {
        var renderer = ps.GetComponent<ParticleSystemRenderer>();
        renderer.renderMode = ParticleSystemRenderMode.Billboard;
        renderer.alignment = ParticleSystemRenderSpace.View;
        renderer.sortingLayerName = "Default";
        renderer.sortingOrder = sortingOrder;
        renderer.minParticleSize = 0.01f;
        renderer.maxParticleSize = 0.35f;
        renderer.sharedMaterial = material;
    }

    private static void AssignParticleSystems(BloodVfx bloodVfx, params ParticleSystem[] systems)
    {
        var serializedObject = new SerializedObject(bloodVfx);

        var particleSystems = serializedObject.FindProperty("particleSystems");
        particleSystems.arraySize = systems.Length;
        for (var i = 0; i < systems.Length; i++)
        {
            particleSystems.GetArrayElementAtIndex(i).objectReferenceValue = systems[i];
        }

        serializedObject.FindProperty("fallbackLifetime").floatValue = 1.2f;
        serializedObject.FindProperty("deactivateOnComplete").boolValue = true;
        serializedObject.ApplyModifiedPropertiesWithoutUndo();
    }

    private static Material GetParticleMaterial()
    {
        var fallback = AssetDatabase.LoadAssetAtPath<Material>(FallbackMaterialPath);
        var texture = GetParticleTexture();

        if (fallback != null)
        {
            fallback.mainTexture = texture;
            fallback.color = Color.white;
            EditorUtility.SetDirty(fallback);
            return fallback;
        }

        var shader = Shader.Find("Universal Render Pipeline/Particles/Unlit");
        if (shader == null)
        {
            shader = Shader.Find("Sprites/Default");
        }

        if (shader == null)
        {
            shader = Shader.Find("Particles/Standard Unlit");
        }

        if (shader == null)
        {
            shader = Shader.Find("Unlit/Transparent");
        }

        fallback = new Material(shader)
        {
            name = "BloodParticle",
            color = Color.white
        };
        fallback.mainTexture = texture;

        AssetDatabase.CreateAsset(fallback, FallbackMaterialPath);
        return fallback;
    }

    private static Texture2D GetParticleTexture()
    {
        var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(ParticleTexturePath);
        if (texture != null) return texture;

        var generated = new Texture2D(32, 32, TextureFormat.RGBA32, false)
        {
            name = "BloodParticleDot",
            filterMode = FilterMode.Bilinear,
            wrapMode = TextureWrapMode.Clamp
        };

        const float center = 15.5f;
        const float radius = 15.5f;
        for (var y = 0; y < generated.height; y++)
        {
            for (var x = 0; x < generated.width; x++)
            {
                var dx = (x - center) / radius;
                var dy = (y - center) / radius;
                var dist = Mathf.Sqrt(dx * dx + dy * dy);
                var alpha = Mathf.Clamp01(1f - dist);
                alpha = Mathf.SmoothStep(0f, 1f, alpha);
                generated.SetPixel(x, y, new Color(1f, 1f, 1f, alpha));
            }
        }

        generated.Apply();

        var fullPath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, ParticleTexturePath);
        File.WriteAllBytes(fullPath, generated.EncodeToPNG());
        Object.DestroyImmediate(generated);

        AssetDatabase.ImportAsset(ParticleTexturePath);
        var importer = AssetImporter.GetAtPath(ParticleTexturePath) as TextureImporter;
        if (importer != null)
        {
            importer.alphaIsTransparency = true;
            importer.mipmapEnabled = false;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.SaveAndReimport();
        }

        return AssetDatabase.LoadAssetAtPath<Texture2D>(ParticleTexturePath);
    }

    private static void EnsureProjectFolder(string parent, string folderName)
    {
        var path = $"{parent}/{folderName}";
        if (AssetDatabase.IsValidFolder(path)) return;

        AssetDatabase.CreateFolder(parent, folderName);
    }
}
