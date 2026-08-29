# 06 Importing a Camera

This chapter explains how to bring your MMD camera work (a camera `.vmd` file) into Unreal. After importing, you get a directly playable camera sequence whose positions, dolly and pan moves, zooms, and cuts all match what you set in MMD.

> **Note:** This manual was translated by AI from the Chinese original, which is the authoritative version. Some expressions may not be fully accurate.

## Before You Start

- Have your camera `.vmd` file ready. It has the same extension as motion files but holds lens data. If you cannot tell them apart, do not worry: Auto Detect will.
- Ideally the scene already has a character with motion, so the playback has something to look at.

## Import Steps

![Camera import](../DocPic/camera_import.jpg)

1. Drag the camera `.vmd` file into the Content Browser.
2. Set up the window as explained below.
3. Click **Import** and wait for completion.

## What Each Option Means

### Camera Bake Rate (30 or 60)

Same logic as character motion:

- **30 FPS**: faithful to MMD's original rhythm.
- **60 FPS**: RedialC's special algorithm adds MMD-style cut-jump frame interpolation.

Unlike character motion, which is recommended at 30 FPS, camera data is recommended at 60 FPS by default. MMD camera cuts rely on adjacent keyframes, while most software does not support adjacent jump interpolation; the details are explained below.

### Camera Keyframes Only (on by default)

This option decides how reworkable your camera is:

- **On (recommended)**: only the keyframes you placed in MMD are recorded, along with the acceleration/deceleration feel between them. You can keep fine-tuning individual positions later without losing the original pacing.
- **Off**: every single frame's position is locked in. Hard to edit afterwards; rarely needed.

Unless you have a special reason, keep the default On.

## Cut Protection

MMD works often switch camera positions in an instant. At 60 FPS, the plugin recognizes these instant cuts and preserves them exactly:

- The last frame before the cut still shows the previous position.
- The very next frame instantly shows the new one.
- No strange transition frames appear in between.

All you need to know: use 60 FPS without fear, cuts will not smear.

*The same algorithm was previously implemented by RedialC in Blender. If you need the Blender version, see [download and usage](https://github.com/JDui/Blender_MMDCameraScale).*

## What You Get Afterwards

A camera sequence (Sequencer) appears in your folder. Double-click it to replay the entire camera work from start to finish; put it into the scene's playback flow and it automatically takes over the view, "holding the camera" for you through the whole show.

The sequence includes: camera positions and orientations, focal changes (zoom in/out), perspective mode, and the timing of every cut.

## Same Name Conflict

As with motion, when the conflict window pops up choose one:

- **Incremental Import**: keeps the old item; the new one is stored under an automatic name like `Camera_1`.
- **Abandon Import**: cancels this import; nothing changes.

## Common Problems

### Strange transition frames appear at camera cuts

Make sure the camera was imported at 60 FPS with **Camera Keyframes Only** left enabled — the cut protection works under that combination.

## Flow Recap

Model, motion, and camera — the trio is complete:

1. Drag a `.pmx` in → you have a character.
2. Drag a motion `.vmd` in → the character performs.
3. Drag a camera `.vmd` in → someone is holding the lens for you.

**Drag the imported model into the Sequencer created by the VMD camera and load the animation. At this point, you have completed the Unreal equivalent of loading the model, camera, and motion in MMD.**

All that remains is pressing play in Unreal and enjoying the result. If you are preparing to export a video, continue learning about Unreal Movie Render; here is the [official Epic documentation](https://dev.epicgames.com/documentation/en-us/unreal-engine/movie-render-pipeline-in-unreal-engine).
