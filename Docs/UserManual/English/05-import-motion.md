# 05 Importing Character Motion

This chapter explains how to put an MMD dance or performance (a `.vmd` file) onto a model you have already imported.

> **Note:** This manual was translated by AI from the Chinese original, which is the authoritative version. Some expressions may not be fully accurate.

## Before You Start

- You have imported at least one `.pmx` model as described in the [model import chapter](02-import-model.md).
- You have the motion `.vmd` file you want to use.
- Motion and model should ideally be a matched pair: use the same model that danced to it in MMD for the most accurate result.
- Before importing, load the model and sample motion in MMD to check the result. Some models deform strangely with certain motions; knowing this in advance prevents you from mistaking it for an Unreal import problem.

## Import Steps

![VMD import](../DocPic/VMD_import.jpg)

1. Drag the motion `.vmd` file into the Content Browser.
2. Set up the window as explained below.
3. Click **Import** and wait for completion.

## What Each Option Means

The plugin now detects the file type automatically, so there is no need to choose an import type. If a window asks you to choose one, the VMD file may be damaged.

### Target Skeletal Mesh (which character to use)

Pick, from the dropdown, the model imported earlier with this plugin. This step is required; otherwise the motion does not know whom to dress onto.

### Character Bake Rate (30 or 60)

- Even when you choose 30 FPS, you can still output video at another frame rate; Unreal performs the frame interpolation.

- **30 FPS**: MMD itself runs at 30 frames per second. The motion you saw in MMD is 30 FPS, so this is the faithful choice.
- **60 FPS**: records more densely and creates larger files. Choose it when you also need the animation for cloth simulation.

In short: **choose 30 for authenticity, 60 for more uses.** Neither changes the speed or length of the motion. For fighting motions, 60 FPS may have the downside of making the movement feel softer.

### Separate Motion and Expression Data (off by default)

When enabled, body motion and facial expressions are split into two files:

- The main file handles only the body's dance.
- A second file ending in `_Expression` handles only blinking, mouth shapes and other expressions.

The benefit of splitting: later you can adjust expression strength alone without touching body motion, or pair one dance's body with another dance's face. If you do not need that, leave it off.

## What You Get Afterwards

An Animation Sequence asset appears in your folder. Double-click it and the preview window shows the character performing the whole motion.

Several notes:

- Physical effects like hair swaying are not recorded into this animation. So if hair stays still in the preview, it will move once played in the scene.
- If the motion contains data for parts the model does not have, those parts are skipped automatically, and a message in the lower left corner lists what was skipped. Minor skips usually do not affect watching.
- If the animation preview differs from MMD, check whether the post-process Animation Blueprint switch is enabled in the lower-right corner of the preview window.
- If you now see the **arm or hand entering the torso, neck, head, legs, or opposite arm**, that is exactly what **Arm Constraint Correction** is designed to reduce. Leave the defaults alone for now; the next chapter explains Debug Draw and the useful tuning controls in detail.

## Same Name Conflict

Dragging in a file whose name matches an existing asset pops up a conflict window:

- **Incremental Import**: keeps the old item and stores the new one under an automatic name, e.g. `Dance_1`. Choose this when you want to keep both versions of the same dance.
- **Abandon Import**: nothing changes; the import is cancelled. Choose this if you dragged by mistake.

## Common Problems

### Some body parts do not move after importing a motion

The motion contained data for parts this model does not have; those parts were skipped automatically. The message at the lower left lists the details. Minor skips barely affect viewing; large-scale stillness usually means the motion and model are not a matched pair — try the original model.

### Expressions do not change

1. Make sure **Import Expressions** was ticked when importing the model.
2. Make sure expression data came with the motion: if you enabled **Separate Motion and Expression Data**, expressions live alone in the file ending with `_Expression`; layer it on top when playing.

### Which expressions are not supported yet?

Special expressions such as material color changes or bone shifts are not supported yet. Facial expressions such as blinking and mouth shapes are the current scope.

## Next Step

The character can move now. First check the arms and hands for visible penetration into the body or legs: [05-2 Arm Constraint Correction](05-2-arm-constraint-correction.md). Once the motion looks clean, continue with [06 Importing a Camera](06-import-camera.md).
