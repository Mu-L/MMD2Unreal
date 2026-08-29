# 03 Model Pose and Physics Tuning

This chapter covers two checks after importing a model: verify its initial pose, then tune the physics of hair, skirts, sleeves, and accessories.

## When this chapter applies

The KawaiiPhysics nodes and collision assets described here are created only when **Enable Physics** is checked in [Importing a Model](02-import-model.md). After import, you can usually find these assets in the Content Browser:

| Asset | Purpose |
| --- | --- |
| `ABP_Post_<ModelName>` | Post-process Animation Blueprint that applies MMD IK/additional transforms and KawaiiPhysics after the input animation. |
| `Phy/PA_<ModelName>_ALL` | Physics Asset containing all usable MMD colliders. This is the default asset used by KawaiiPhysics. |
| `Phy/PA_<ModelName>_G00`, `_G01`, … | Physics Assets split by MMD collision group, for inspection or selecting one group manually. The generated numbers depend on the PMX model. |
| `IKR_<ModelName>` | IK Rig for retargeting or other IK workflows. It is not the same thing as the IK/constraint node inside `ABP_Post`. |

If the physics assets are missing, check that **Enable Physics** was enabled, that the KawaiiPhysics plugin is enabled, and that the PMX contains valid rigid bodies and dynamic bone chains. If the model was imported without physics, import the PMX again with **Enable Physics** enabled. Use a new content folder if the project already contains same-named assets.

## What the ABP does

`ABP_Post_<ModelName>` is the model's post-process Animation Blueprint. It does not replace the base VMD or Sequencer animation. It adds a final processing layer to the pose:

```text
Input animation → MMD IK/additional transforms → KawaiiPhysics → Output pose
```

VMD, Control Rig, and other Sequencer animation remain the main source of the pose. The ABP adds the MMD constraints and physics after that pose has been evaluated. During preview or playback, dynamic bones such as hair and skirts are recalculated from the current pose and the model's movement.

When **Enable Physics** is disabled, the importer may still create `ABP_Post` for MMD IK and additional transforms, but it does not create the KawaiiPhysics nodes or the per-group physics assets used in this chapter. Those assets cannot be tuned through this workflow in that case.

## What the IK nodes do

The MMD IK/constraint node in the ABP reads the PMX IK chains, target bones, link bones, and angle limits, then solves them after the base animation. This helps preserve relationships defined by the model author for feet, knees, arms, and other chains. The node also applies MMD additional rotation or translation.

With the automated algorithm, in most cases you do not need to separately adjust whether each IK channel is enabled as you would in MMD. However, note that the corresponding IK values on the IK node are the switches for the corresponding MMD IK channels.

In almost all cases, you do not need to edit the IK nodes in the ABP:

- Every model has different bone names, IK chains, and angle limits, so the nodes are generated from the current PMX.
- Changing the chain, target bone, or execution order can make knees bend backward, feet drift, arms flip, or the result differ from MMD.
- Collision problems should normally be solved with the Physics Asset and KawaiiPhysics settings, not by forcing bones through an IK edit.

Only create a copy of the ABP and experiment if you understand the PMX constraints and intentionally need a custom animation pipeline. For normal use, keep the generated IK/constraint node and its connections unchanged.

## Improve the physics with KawaiiPhysics

### 1. Observe the default result first

Open `ABP_Post_<ModelName>` and switch to **AnimGraph**. When physics is enabled, the graph contains generated KawaiiPhysics nodes, usually inside the region marked `PMX Kawaii Physics (MMD2Unreal Generated)`.

Open a Level Sequence containing the model and play a short section. Check whether hair passes through the head or shoulders, skirts pass through the legs, sleeves or accessories shake too much, or dynamic bones suddenly stretch, explode, or stop when the model moves quickly.

First decide whether the issue is a collider problem or a physics-parameter problem. Make one small change and preview again.

### 2. Select an MMD collision group in the KawaiiPhysics node
![KawaiiPhysics node](../DocPic/Abp_View.jpg)

This is the place where you choose the MMD collision group:

1. Open `ABP_Post_<ModelName>` and switch to **AnimGraph**.
2. Select the KawaiiPhysics node you want to tune.
3. In the **Details** panel, expand **Limits**.
4. In **Physics Asset**, choose `PA_<ModelName>_ALL` or `PA_<ModelName>_Gxx`.
5. Compile the ABP, save it, and preview the Level Sequence again.

The `xx` in `Gxx` is the PMX rigid body's MMD collision-group number, from `00` through `15`:

| MMD group | Asset |
| --- | --- |
| Group 0 | `PA_<ModelName>_G00` |
| Group 1 | `PA_<ModelName>_G01` |
| Group 2 | `PA_<ModelName>_G02` |
| … | … |
| Group 15 | `PA_<ModelName>_G15` |

![Collision group](../DocPic/collision_change.jpg)
Use `_ALL` by default. It contains all valid MMD colliders, and the importer preserves the PMX two-way non-collision masks in the Physics Asset. Switch to `_Gxx` only when you need to isolate a group, compare groups, or stop an unrelated collider from affecting one dynamic chain.

This property selects the source of the colliders; it does not select which hair or skirt chain is simulated. The simulated bones are still determined by the node's `Root Bone` and `Additional Root Bones`. An ABP can contain several KawaiiPhysics nodes, and each node can use a different collision asset when necessary.

### 3. Inspect and adjust the collision shapes

Double-click `PA_<ModelName>_ALL` or a `_Gxx` asset to open the Physics Asset Editor:

1. Confirm that the preview mesh is the current MMD model.
2. Show the collision bodies and check whether spheres, capsules, or boxes for the body, head, shoulders, and legs are in the right locations.
3. If a collider is clearly offset, check the model import, skeleton pose, and scale first. Do not compensate for a global offset by simply making the KawaiiPhysics `Radius` larger.
4. If needed, adjust the selected Body's position, rotation, or size, then save the Physics Asset.
5. Return to the ABP and Level Sequence and preview again. Change one area at a time.

MMD collision rigid bodies are imported as Physics Asset shapes. `_ALL` and `_Gxx` are different collision assets generated from the same PMX data. Reimporting the model can regenerate them, so record important custom adjustments.

### 4. Tune the main KawaiiPhysics parameters

In the KawaiiPhysics node's Details panel, expand **Physics Settings**:

| Parameter | Tuning direction |
| --- | --- |
| `Damping` | Lower values allow more sway; higher values make the motion duller and heavier. Increase it when motion keeps shaking; decrease it when hair barely moves. |
| `Stiffness` | Higher values return the chain to its animated pose more strongly; lower values allow freer sway. Increase it when a skirt is thrown too far; decrease it for softer motion. |
| `Radius` / `Collision Radius` | The collision sphere carried by each simulated bone. Too large can cause explosions or excessive pushing; too small can allow penetration. |
| `Limit Angle` | Limits the per-step physics rotation and can suppress flips and jitter. `0` means unlimited. |
| `World Damping Location` and `World Damping Rotation` | Control how much the model's movement and rotation are transferred into sway. Increase them when fast movement creates excessive trailing; lower them when you want stronger drag. |

Change one parameter by a small amount, then preview again. Bone lengths, rigid-body sizes, and motion ranges differ between models, so values from one model should not be copied blindly to another.

### 5. A practical order for skirt penetration

Penetration usually comes from colliders being too small, the wrong collision group being selected, or large gaps between simulated bones. Try this order:

1. Restore or select `PA_<ModelName>_ALL` and confirm that the skirt chain can see the body and leg colliders.
2. Slightly enlarge the leg, hip, or body colliders in the Physics Asset Editor without pushing them far outside the mesh.
3. Slightly increase the KawaiiPhysics `Radius`.
4. If there are gaps between adjacent skirt bones, increase **Bones → Bone Subdivision → Bone Subdivision Count** to improve collision sampling along long bone segments. This increases computation.
5. If penetration continues, check whether the skirt chain should use another `Gxx` asset and whether the PMX non-collision mask disables that pair.

If the skirt is pushed away from the body, reduce `Radius` or the collider size first, then check whether the selected group contains too many body colliders.

## What not to edit directly

- Do not delete or reorder the MMD IK/constraint nodes to fix penetration.
- Do not add the same colliders both to the Physics Asset and to the KawaiiPhysics node's `Spherical Limits`, `Capsule Limits`, or `Box Limits`; duplicate collision can result.
- Do not change several chains, collision assets, and physics parameters at once.
- After reimporting, confirm that each KawaiiPhysics node still points to the intended `_ALL` or `_Gxx` asset.

For collision shapes, parameters, bone subdivision, curves, and external forces, see the [official KawaiiPhysics collision setup guide](https://pafuhana1213.github.io/KawaiiPhysics-Portal/en/docs/features/collision-setup) and the [official parameter reference](https://pafuhana1213.github.io/KawaiiPhysics-Portal/en/docs/parameters/physics).

After the model pose and physics look right, continue with [04 M2U Material Adjustment](04-m2u-material-adjustment.md).
