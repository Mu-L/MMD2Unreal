# 02 Importing a Model

This chapter explains how to bring an MMD `.pmx` model into Unreal. After importing, you will have a character you can drag into a scene, one that can make facial expressions, with hair and skirt that sway naturally.

> **Note:** This manual was translated by AI from the Chinese original, which is the authoritative version. Some expressions may not be fully accurate.

## Before You Start: Check the Model Folder

A `.pmx` file never works alone; its colors and clothing patterns live in texture files sitting next to it. So please confirm:

- The model and its matching textures are still together in the original folder.
- If you only received a lone `.pmx` file, ask the creator for the complete package first.

## Import Steps

1. Find the **Content Browser** at the bottom of Unreal (the area showing asset thumbnails). It is a good idea to create a new empty folder for this model first.
2. Drag the `.pmx` file directly into the Content Browser.
3. An import settings window appears. For your first time you do not need to change anything: just click the **Import** button in the lower right.
4. Wait for the progress messages to finish. Larger models take longer, which is normal.
5. Once done, the folder now holds everything related to the model.
6. Remember to save promptly. If you do not save manually, the imported assets will disappear after you close the Unreal Editor.
![Save all](../DocPic/Saveall.jpg)

## What Each Option Means

The options are grouped by purpose:
![Import window](../DocPic/PMX_import.jpg)

### Asset Type

- **Character**: a person model. Choose this almost always.
- **Scene**: stages, rooms, furniture — models that do not move by themselves.

### Textures & Appearance

- **Import Textures**: brings in the color/pattern files the model uses. Keep it ticked, otherwise the model turns gray-white.
- **Create Materials**: automatically generates parent and child material assets for the model in a way that follows Unreal Engine conventions.

### Expressions

- **Import Expressions**: blinking, smiling, mouth shapes and other face changes come in too, including combined expressions made of several smaller ones.
- Keep it ticked for the full experience; turning it off speeds up import if you only need the body for now.

<small>**Name-compatibility note:** you may notice that some expression names carry an odd `UE5EA_patch_` prefix. It is the existing name-compatibility prefix: special-character names such as `∧`, `▲`, and `□` are converted to underscores in Control Rig, so the importer adds this prefix to avoid collisions. The prefixed expressions still correspond to the original PMX expressions, and the naming mapping has not changed.</small>

### Posing Helpers

- **Create IK Asset** creates `IKR_<MeshName>` and uses the plugin's bundled minimal UE5 Manny content to generate two retargeters: `RTG_<MeshName>` for Manny → MMD and `RTG_<MeshName>_ToManny` for MMD → Manny. Both RTGs also include basic mouth-shape and blink mappings so common MMD expressions can travel together with ARKit-style expression curves.
- **Create Rig Binding** creates `CR_` / `CRMMD_` Control Rigs for posing and animation work in Sequencer.
- **Arm Constraint Correction** reduces upper-arm, forearm, and hand penetration into the torso, neck, head, legs, or the opposite arm. It is enabled by default for Character import and disabled by default for Scene import. Leave it at the default for now; after importing motion, [05-2 Arm Constraint Correction](05-2-arm-constraint-correction.md) explains Debug Draw and detailed tuning.
- If these assets are unfamiliar, **leave the defaults enabled**. They are helper assets for later retargeting and manual animation work and do not interfere with normal import.

### Physics

- **Enable Physics**: makes hair, skirts and accessories sway naturally with motion. On by default.
- **World Physics Collision**: controls whether swaying parts are pushed away by walls and floors in your scene — for example, hair pushed aside when the character stands close to a wall. **Enabled by default for Character import and disabled by default for Scene import.**
- For multi-chain structures such as skirts, the importer uses the PMX model's cross-chain Joints where possible so neighboring skirt panels keep their overall relationship. Regular ring-shaped skirts may also receive safe missing horizontal links automatically. Normal users do not need to build these links by hand; tune the generated KawaiiPhysics nodes in the same way as other dynamic chains.

## What You Get Afterwards

Open the folder you imported into and you will see:

| What you see | What it is |
|---|---|
| A character you can drag into the scene | The model itself; double-click to inspect |
| A `Materials` folder | The model's materials |
| A `Textures` folder | The model's textures |
| An `RM_<MeshName>` file | A text asset that stores the PMX model comments; double-click to read them |
| Files starting with `IKR_` | The current PMX model's IK Rig, describing the retarget chains, IK solvers, and Goals |
| `RTG_<MeshName>` / `RTG_<MeshName>_ToManny` | Retargeters for Manny → MMD and MMD → Manny respectively; they also transfer the basic mouth-shape and blink curves |
| Files starting with `CR_` or `CRMMD_` | Modern Rig controllers for manually adjusting poses |
| Files starting with `ABP_Post_` | Animation Blueprints that apply MMD IK, additional transforms, Arm Constraint Correction (including body and leg avoidance), and physics in real time; open them as described in Chapter 03 when physics is enabled |

Materials and textures are neatly stored in the `Materials` and `Textures` subfolders.

For models containing SDEF, the same `ABP_Post_` also includes the native SDEF pose-correction node automatically. In normal use, do not add, delete, or move it manually.

`RM_<MeshName>` is a text asset generated from the PMX model comments, not an extra model file. It preserves the Japanese and English comments from the PMX file when available. Double-click it to read the comments; reimporting the model updates the text as well.

A PMXD Mesh Deformer solution existed during development, but has been temporarily removed due to real-time preview performance concerns and numerous compatibility issues. Imports no longer generate PMXD files; the model’s `RM_` notes also describe the skinning limitations.

The current release already includes the minimal Manny content required for bidirectional retargeting, so with **Create IK Asset** enabled a normal import creates `IKR_<MeshName>` and both RTG directions without requiring Manny assets from the project. If the bundled plugin content is missing or damaged, the import warning explains why. For reverse conversion use the `_ToManny` asset instead of swapping only the preview meshes in the forward RTG. Existing models can rebuild both directions by selecting only the IK asset overwrite option during reimport.

### Bidirectional RTG Examples

![MMD to Manny RTG](../DocPic/RTG_MMDout.jpg)

`RTG_<MeshName>_ToManny` is for MMD → Manny and has its own operation stack.

![Manny to MMD RTG](../DocPic/RTG_toMMD.jpg)

`RTG_<MeshName>` is for Manny → MMD and configures MMD-specific body and IK handling.

### Basic expressions transferred by the two RTGs

Both directions automatically handle the five common MMD vowel mouth shapes and a normal blink:

| MMD expression | Matching ARKit-style expression |
|---|---|
| `あ` | open mouth `jawOpen` |
| `い` | horizontal mouth stretch `mouthStretch` |
| `う` | puckered lips `mouthPucker` |
| `え` | tightened mouth corners `mouthDimple` |
| `お` | rounded mouth `mouthFunnel` |
| `まばたき` | blink `eyeBlink` |

`RTG_<MeshName>` carries these Manny / ARKit-style expressions onto the MMD model. `RTG_<MeshName>_ToManny` carries the MMD expressions back to Manny / ARKit-style curves. Expressions that have left and right sides, such as eye blinks, are assigned to both sides automatically where needed.

This is an **approximate visual mapping for convenient cross-system use**, not a phoneme-accurate speech conversion. Mouth shapes vary a lot between characters, so you can still adjust the expression curves on the target character if a particular shape does not look right.

For a model imported before this feature was added, reimport the PMX and select only the **IK Rig / IK Retargeter** overwrite option to rebuild both RTGs. You do not need to overwrite the model itself just to get these mappings.

## Reimporting an Existing PMX

Dragging a `.pmx` with the same name back into the original Unreal folder still shows a risk warning instead of overwriting immediately.

If you want to preserve the old version, the safest choice is still to **rename the model and import it into a new folder**. If you intentionally want the new PMX to update the existing model, choose **Reimport PMX Model** in the warning window. A second **PMX Reimport Options** window then lets you choose exactly which existing assets may be overwritten in this run.

**Overwrite Skeletal Mesh + synchronize Skeleton** is one coupled core option and cannot be split. It overwrites the Skeletal Mesh while synchronizing compatible skeleton data into the existing Skeleton; the existing Skeleton asset and path remain unchanged. Additive bones and reference-pose changes can be synchronized in place, while removing, renaming, or reparenting an existing bone is refused before any write. It is selected by default, so accepting the dialog normally still updates the model, skeleton, expressions, and related core data. Materials and textures can be selected independently.

PhysicsAsset, Post Process AnimBP, IK Rig / IK Retargeter, and Control Rigs can also be selected independently. Turning off the core option does not force those companion options off: any selected companion assets are rebuilt against the actual model and Skeleton currently in use, while the plugin does not actively rebuild, rename, compile, save, or delete unselected existing assets. This makes it possible to rebuild both RTG directions, Control Rigs, or other companion assets without overwriting the model itself.

If an older model contains SDEF but has not yet been migrated to the current native SDEF representation, select both **Overwrite Skeletal Mesh + synchronize Skeleton** and **Overwrite existing Post Process AnimBP** for the first migration. Selecting only one cannot complete the migration; a core-only attempt is refused, and the plugin does not fall back to the old SDEF deformation path.

Keep these points in mind:

- Reimport is an overwrite operation and cannot be undone with one click. Back up important models first.
- Manual edits made to generated Rigs, materials, or other related assets may be rebuilt or overwritten when their matching overwrite option is selected.
- If the new PMX removes materials, textures, or other companion content that existed before, the old Unreal assets are not deleted automatically yet. Remove them manually only after confirming they are no longer needed.

## Common Problems

### Dragging a model in does nothing

Check in order:

1. Are the plugin and KawaiiPhysics both enabled under **Edit > Plugins**? The editor also needs to be restarted after installation.
2. Did you drag the `.pmx` file itself rather than its archive?
3. Try again in a different Content Browser folder.

If the first check fails, return to [Download & Install](01-installation.md).

### The model imports but appears gray-white

Its textures did not come along. The most common cause is that the textures were separated from the `.pmx` file. Put the complete asset folder back together, delete the gray model, re-import, and make sure **Import Textures** is ticked.

### The model looks slightly different from MMD

Models containing SDEF use native UE skinning with automatically generated pose corrections. QDEF currently imports as BDEF4 linear skinning using its four-bone weights. It does not reproduce dual-quaternion deformation, so twisting and bending may shrink or flatten the mesh compared with MMD. Models mixing SDEF and QDEF can be imported. SDEF pose corrections currently cover imported LOD0 only; manually generated reduced LODs have no matching corrections and may differ more. Import fails if the pose-correction data cannot be generated completely. Unusual bones or special constraints can also produce differences from MMD.

### MMD-specific gloss and outlines are not fully reproduced

MMD's Sphere, Toon, and Edge material effects currently use basic material handling, so some gloss and outline looks may differ from MMD.

### A same-name conflict appears

Items with the same name already exist. M2U does not overwrite them immediately; it first shows the risk warning.

- To preserve the current version, abandon the import and rename the model or use a fresh folder.
- To intentionally update the existing model, make sure any edits you care about are backed up, click **Reimport PMX Model**, then choose the assets that may be overwritten in the second window.

## Next Step

The model is in. First check its pose and physics: [03 Model Pose and Physics Tuning](03-model-pose-and-physics.md).
