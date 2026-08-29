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

### Posing Helpers

- **Create IK Asset** and **Create Rig Binding**: these generate helper files for posing and adjusting. If they mean nothing to you, **just leave them at their defaults**; they never get in the way.

### Physics

- **Enable Physics**: makes hair, skirts and accessories sway naturally with motion. On by default.
- **World Physics Collision**: controls whether swaying parts are pushed away by walls and floors in your scene — for example, hair pushed aside when the character stands close to a wall. On by default.

## What You Get Afterwards

Open the folder you imported into and you will see:

| What you see | What it is |
|---|---|
| A character you can drag into the scene | The model itself; double-click to inspect |
| A `Materials` folder | The model's materials |
| A `Textures` folder | The model's textures |
| An `RM_<MeshName>` file | A text asset that stores the PMX model comments; double-click to read them |
| Files starting with `IKR_` | Unreal assets used for retargeting animation data |
| Files starting with `CR_` or `CRMMD_` | Modern Rig controllers for manually adjusting poses |
| Files starting with `ABP_Post_` | Animation Blueprints that apply MMD IK, additional transforms, and physics in real time; open them as described in Chapter 03 when physics is enabled |

Materials and textures are neatly stored in the `Materials` and `Textures` subfolders.

`RM_<MeshName>` is a text asset generated from the PMX model comments, not an extra model file. It preserves the Japanese and English comments from the PMX file when available. Double-click it to read the comments; reimporting the model updates the text as well.

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

A few models use a special fitting method that is currently approximated, so some hems or hair tips may differ slightly. This does not affect overall use.

### MMD-specific gloss and outlines are not fully reproduced

MMD's Sphere, Toon, and Edge material effects currently use basic material handling, so some gloss and outline looks may differ from MMD.

### A same-name conflict stops the import

Identically named items already exist in the folder. To protect existing content, overwriting is refused. Rename the model or import it into a fresh folder.

## Next Step

The model is in. First check its pose and physics: [03 Model Pose and Physics Tuning](03-model-pose-and-physics.md).
