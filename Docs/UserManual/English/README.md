# MMD2Unreal User Manual

This manual is for friends who want to continue the MMD community ecosystem while also using more modern software tools.

You do not need any programming knowledge; you only need to understand MMD and be willing to try Unreal Engine.

It's simple! Just follow the steps and click along the way.

> **Note:** The Chinese edition in this directory is the manual master; English and 日本語 are corresponding translations.

## You can also find RedialC here!

More works, resources, and updates:

- [BowlRoll](https://bowlroll.net/user/216962/files)
- [Bilibili](https://space.bilibili.com/11831050)
- [DeviantArt](https://www.deviantart.com/redialc)
- [AplayBox](https://www.aplaybox.com/u/999029208)

## Supported Environment

- Unreal Engine 5.8 (if possible, please download and install it from the Epic Games Launcher)
- Windows x86 computers, and Intel/M-series Mac computers

- Not supported: Windows ARM computers, Huawei HarmonyOS computers, and other devices that cannot run Unreal Engine natively.

## Contents

Read the chapters in order to go from zero to a finished scene:

| Chapter | What it covers | When to read |
|---|---|---|
| [01 Download & Install](01-installation.md) | Download the plugin from the releases page, place it into your project, enable it | Before first use |
| [02 Importing a Model](02-import-model.md) | Bring MMD character or stage models into Unreal | When you want to import a model |
| [03 Model Pose and Physics Tuning](03-model-pose-and-physics.md) | Check the imported model's pose and adjust hair, skirts, and other physics | After importing a model when pose or physics needs tuning |
| [04 M2U Material Adjustment](04-m2u-material-adjustment.md) | Adjust the appearance of an imported MMD model | When you want to change materials after importing a model |
| [05 Importing Character Motion](05-import-motion.md) | Put dances and performances onto your model | When you want the character to move |
| [06 Importing a Camera](06-import-camera.md) | Bring your MMD camera work over | When you want to use MMD camera data |
| [07 Creating Motion with Rig](07-create-motion-with-rig.md) | Use Rig in Sequencer to freely pose and keyframe in a next-generation workflow | When creating motion from scratch |
| [08 Editing Existing Motion with Rig](08-edit-existing-motion-with-rig.md) | Add layered control bindings with a specially designed Rig to correct existing motion | When fine-tuning an existing motion |
| [09 Exporting Character Motion VMD](09-export-motion.md) | Save tuned character motion and expressions back into a VMD file | When you want to take the motion back to MMD |
| [10 Exporting a Camera](10-export-camera.md) | Save your tuned camera work back into a VMD file for MMD | When you want to take the camera back to MMD |
| [11 Other Common Problems](11-other-faq.md) | Keep general questions that do not belong to one specific operation | Whenever another problem appears |

## The Whole Process in One Minute

1. **Install**: download from the releases page, drop it into your project's Plugins folder, enable it when opening the project.
2. **Drag the model**: drag a `.pmx` file straight into the Content Browser at the bottom of Unreal, click Import, and the model appears. Character models and stage models are supported.
3. (Optional) **Tune pose and physics**: if **Enable Physics** was checked during import, verify the pose, then choose collision assets and tune KawaiiPhysics in `ABP_Post_`.
4. (Optional) **Adjust materials**: when you want to change the model's appearance, open its material instance and adjust the exposed material properties.
5. **Drag the motion**: drag a dance `.vmd` file in as well, then choose the MMD model you just imported.
6. **Drag the camera**: drag a camera `.vmd` file in, and you get a camera sequence that can play inside Unreal.
7. (Optional) **Animate with Rig**: use a `CR_` / `CRMMD_` Control Rig in Sequencer to create motion from scratch, or layer `CRMMD_` corrections over existing motion.
8. (Optional) **Take it back**: after tuning the character motion or camera in Unreal, export it as a `.vmd` file and continue using it in MMD.

The whole import process is as simple as dragging files into a folder: **if you can drag and drop, you can use this plugin.**

The steps after import are designed to feel as close as possible to the native Unreal workflow, with **no extra standalone window to open.**

## One Thing to Know Before You Start

- Every step in this manual involves only three actions: dragging files, ticking options, and clicking buttons.
- After importing, some oddly named small items will appear in your project (names starting with `IKR_`, `ABP_Post_`, `RM_`, and so on). They are assets prepared automatically by the plugin; the `RM_` asset stores the PMX model comments. They handle 99% of the traditional workflow, and any adjustments are very simple.
- Whenever an option confuses you, leaving it at its default value is always the safe choice.
