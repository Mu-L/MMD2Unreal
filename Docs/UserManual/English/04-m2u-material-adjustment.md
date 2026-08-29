# 04 M2U Material Adjustment

This chapter explains how to understand and adjust M2U materials after importing an MMD model.

## First: what M2U materials are for

The M2U preset material is based on the `MF_MMD_RedialC` material function. It is a basic transitional material for MMD users: it offers familiar controls for colour, textures, roughness, normals, emissive effects, and MMD `SPA/SPH` sphere maps, so that users who are new to Unreal Engine can first migrate a model and adjust its appearance with a workflow close to Ray-MMD.

This preset is a reliable starting point. It is neither a complete recreation of Ray-MMD nor the limit of Unreal's material system. Do not treat it as a closed black box or as the only way Unreal materials can work. Once you have developed your own visual preferences and workflow, you can continue learning Unreal's material system and create your own master materials.

## Recommended order for first adjustments

You do not need to understand every material asset name at first. Open the final `Mi_<model name>_<material name>` material instance. In the Details panel, an edit is written to that material instance only after you tick the override box to the left of the parameter. Adjust in this order:

1. Check that the `Diffuse` base-colour texture is correct. It is usually assigned automatically on import.
2. Use `DiffuseColor` for an overall colour correction, such as changing the colour of clothing, hair, or skin.
3. Use `Roughness` to make the surface appear smoother or more matte.
4. If the model uses MMD SPA/SPH maps, check `Sphere` and the corresponding add/multiply strengths. These values are normally set automatically during PMX import.
5. Then try `Normal`, `Emissive`, `RimLight`, and opacity controls.
6. Click **Save** and inspect the result in the model preview or in a level.

If an option is missing from `Mi_`, first check its Parent, then confirm that the parent material exposes that parameter. Save and preview after each adjustment. Do not edit the shared `MF_MMD_RedialC` just to change one material slot: changing a shared function affects every `MS_` that uses it.

## Adjustable properties in the current preset

The following parameters are currently visible in the material instance Details panel, arranged in the same groups as the panel. Their names match the UI. Start with `Diffuse`, `DiffuseColor`, and `Roughness`. `*Setting` parameters are advanced texture-coordinate settings; leave them at their defaults unless you have a specific need.

### `0.Debug`

| Parameter | When to use it |
|---|---|
| `Z-Fighting` | Adjust only when overlapping surfaces flicker or jitter. Start close to `0` and make small changes. |

### `1.Diffuse`

| Parameter | When to use it |
|---|---|
| `Diffuse` | The base-colour texture. It is normally assigned automatically on import. |
| `DiffuseColor` | Your first choice for overall tinting, for example to change clothing, hair, or skin. White means no additional tint. |
| `DiffuseColorProfile` | Use this only when you need to refine the overall colour appearance; use `DiffuseColor` first for ordinary tinting. |
| `DiffuselSetting` | Advanced texture-coordinate settings for the base-colour texture. |

### `2.Roughness`

| Parameter | When to use it |
|---|---|
| `Roughness` | Controls whether a surface appears smoother or more matte. The base preset value is about `0.6`; adjust this before considering a roughness map. |
| `RoughnessMap` | Use when you have a suitable roughness texture. |
| `EnableRoughnessMap` | Turn on to make `RoughnessMap` take effect. It is off by default. |
| `RoughnesslSetting` | Advanced texture-coordinate settings for the roughness texture. |
| `Anisotropy` | Advanced control for anisotropic appearance. Ordinary MMD materials usually do not need adjustment here. |

### `3.Specular`

| Parameter | When to use it |
|---|---|
| `Specular` | Specular highlight strength. The base preset value is about `0.1`; leave it at the default when unsure. |
| `Metallic` | Metallic amount. Fabric, skin, and plastic usually use low values. |
| `MetallicMap` | Use when you have a suitable metallic texture. |
| `EnableMetallicMap` | Turn on to make `MetallicMap` take effect. It is off by default. |
| `MetalSetting` | Advanced texture-coordinate settings for the metallic texture. |

### `4.Emissive`

| Parameter | When to use it |
|---|---|
| `Emissive` | Emissive texture, suitable for eyes, accessories, lights, or special effects. |
| `EmissiveColor` | Adjusts emissive colour and strength. Do not rely entirely on emissive controls for an ordinary base colour. |
| `RimLight` | A stylized control for the rim-light appearance. |
| `RimLightSoftness` | Adjusts the softness of the rim light. The base preset value is about `0.25`. |

### `5.Normal`

| Parameter | When to use it |
|---|---|
| `Normal` | Normal map. Leave the default when you do not have an additional normal map. |
| `NormalIntensity` | Adjusts normal-detail strength. The base preset value is `1.0`; make small changes to avoid an overdone result. |
| `NormalSetting` | Advanced texture-coordinate settings for the normal map. |

### `6.MMD SPA/SPH`

| Parameter | When to use it |
|---|---|
| `Sphere` | The MMD SPA/SPH sphere map. It is normally assigned automatically when the PMX material uses one. |
| `SphereAddStrength` | Adjusts the contribution of an SPA (additive) sphere map. PMX import sets it automatically from the sphere-map mode. |
| `SphereMulStrength` | Adjusts the contribution of an SPH (multiply) sphere map. PMX import sets it automatically from the sphere-map mode. |

### `7.Opacity`

| Parameter | When to use it |
|---|---|
| `Opacity` | Overall opacity. The base preset value is `1.0`. |
| `OpacityGamma` | Adjusts the appearance of the opacity transition. The base preset value is `1.0`; try it only when an edge appears too hard or too soft. |
| `EnableShadow` | Controls whether the transparent material participates in shadows. It is on by default. |

## Quickly replace the base appearance of an entire model with your own master material

In this manual, “material master” means a material asset from which material instances can inherit, such as an `M_` or `MS_` asset. Once you have your own workflow, you can prepare a master material and use the model-level material instance to replace the base appearance of an entire model. This avoids opening dozens or even hundreds of individual `Mi_` assets:

1. Create or prepare your own master material, for example `M_MyMmdBase`. You can copy and develop an existing `MS_`, or use a completely different node setup that you know well.
2. In the Content Browser, open `Materials/MotherMat/MSi_<model name>` for the model.
3. In the Details panel, change **Parent** to your master material and save the material instance.
4. Check the model preview. Every `Mi_` whose parent is `MSi_<model name>` updates through the new parent, so the base appearance of the entire model can change together.

To preserve the commonly imported settings in `Mi_` as far as possible, your custom master material should preferably support at least these parameter names: `Diffuse`, `DiffuseColor`, `Sphere`, `SphereAddStrength`, and `SphereMulStrength`. If the new master material does not expose a parameter, its override value remains stored in `Mi_` but no longer affects the appearance.

This is an example of Unreal's parent-material and material-instance inheritance. The plugin includes similar conveniences not only to make import smoother, but also to help you gradually understand Unreal asset organisation, parameter inheritance, and batch replacement.

### Reimport note after replacing the master material

When a model with the same name is reimported, the current importer resets the **Parent** of `MSi_<model name>` to that model's `MS_<model name>`. If you assigned a custom parent to `MSi_`, check and set it again after reimporting. Include this in your reimport workflow.

## Material asset relationships: MS, MSi, and Mi (MI)

The naming is easier to understand after the basic workflow above. `MS_` is a master material, while `MSi_` and `Mi_` are material instances. `MF_` is a material function, not the parent of a material slot, and is not called a master material. After importing with **Create Materials** enabled, a model's materials normally use this parent-child structure:

```text
Mi_<model name>_<material name>  Final material instance: one PMX material slot
└─ MSi_<model name>              Model-level material instance shared by all Mi_ assets
   └─ MS_<model name>            Model-level master material
      └─ MF_MMD_RedialC          Shared basic MMD material function
```

- `Mi_<model name>_<material name>`: the final material instance for one PMX material slot. Open this level to adjust one item of clothing, hair, or other material.
- `MSi_<model name>`: the model-level material instance, shared between `MS_` and every final `Mi_`. Changing its **Parent** switches the master material for the entire model.
- `MS_<model name>`: the master material for the current model. It is created using `MF_MMD_RedialC` during the first import.
- `MF_MMD_RedialC`: a basic material function that can be shared by multiple models. Check its scope before changing it.

This documentation sometimes uses **MI** as a general term for Material Instance. Actual asset names use `Mi_` and `MSi_`; follow the file names shown in the Content Browser. These assets normally appear in the model import folder under `Materials` and `Materials/MotherMat`.

## When to move beyond this basic material

When you need complex character skin, cloth, glass, special lighting, layered materials, or project-level performance control, treat the M2U material as a reference and transition layer. Learn and create your own `M_` master materials and `MF_` material functions. You can keep the `Mi_` organisation, or use the model-level `MSi_` replacement point to apply your established material workflow to the whole model.

The M2U basic material is a first step into Unreal, not a definition of Unreal's material capabilities. Understanding the roles of `MS_`, `MSi_`, and `Mi_` means you have already started using Unreal's important material-inheritance and batch-replacement ideas.

## Unreal material documentation

- [UE 5.8 Materials and Rendering Documentation](https://dev.epicgames.com/documentation/en-us/unreal-engine/unreal-engine-materials)
- [Material Properties](https://dev.epicgames.com/documentation/en-us/unreal-engine/unreal-engine-material-properties)

## Notes

- Before changing an `MS_` or the shared `MF_MMD_RedialC`, confirm the scope. A shared-function change can affect multiple `MS_` assets.
- When replacing the Parent of `MSi_`, use a master material with compatible parameter names where possible; otherwise texture and colour values stored in `Mi_` may no longer drive the new material.
- When reimporting a model with the same name, confirm again that `MSi_` still points to your custom master material.

## Next step

After adjusting the materials, continue with [05 Importing Character Motion](05-import-motion.md).
