# 08 Editing Existing Motion with Rig

This chapter explains how to keep an existing MMD motion and add new adjustments as a separate Control Rig layer. The recommended workflow is to use the plugin-generated `CRMMD_ModelName` in Sequencer as a **Layered Control Rig** and add the control binding on top of the original motion.

The original Animation Sequence remains the base motion, while the new arm, body, finger, or expression changes live on the upper layer. You can revise the upper controls repeatedly without baking or rewriting the original keys.

## When to Use This Chapter

- You imported a VMD motion and received an Animation Sequence.
- You only want to correct a few poses, such as raising an arm, fixing a foot passing through the floor, changing the gaze, or adjusting an expression.
- You want to preserve the original dance timing and most of its motion while adding small corrections on selected frames.

If you are starting from an empty timeline, see [07 Creating Motion with Rig](07-create-motion-with-rig.md).

## Before You Start

- You imported the VMD as described in [Importing Character Motion](05-import-motion.md), using a matching target Skeletal Mesh.
- You have a Level Sequence with the same MMD character in the level and in Sequencer.
- The Content Browser contains the `CRMMD_ModelName` Control Rig for this model. **Create Rig Binding** must have been enabled during model import.

## 1. Put the Existing Motion on the Base Layer

1. Open or create a Level Sequence and add the MMD character from the World Outliner to Sequencer.
2. On the character's **Skeletal Mesh** track, add an **Animation** track. You can also drag the imported Animation Sequence from the Content Browser onto the Skeletal Mesh track.
3. Choose the motion in the Animation section and set its start and end frames. Play it once to confirm that the original feet, hands, and expressions already move correctly.
4. This Animation track is the base. Any Control Rig correction must stay under the **same character binding**, not on another character or the camera binding.

## 2. Add `CRMMD_` as a Layered Control Rig

The preferred method is to choose the layered mode while adding the track:

1. Select the same character's **Skeletal Mesh** track and click **+ Track**.
2. Choose **Control Rig > Layered**, then select the `CRMMD_ModelName` for this model. If the menu asks for the Rig first and then shows a layered option, enable **Layered** before confirming.
![Add layered Control Rig](../DocPic/Controlrig_Add.jpg)
3. If **Layered** is not shown in the add menu, first add `CRMMD_ModelName` with **Control Rig > Control Rig Classes**. Then right-click the Control Rig track in Sequencer's left Outliner and choose **Convert To Layered**.
4. Make sure the Control Rig section overlaps the Animation section for at least the frames you want to correct. If the Control Rig section covers only the correction range, the original Animation Sequence continues to play outside that range.
5. Expand the Control Rig track and confirm that the `CRMMD_` controls are listed. The generated `CRMMD_` includes the backwards solve needed to read the current bone motion, so the existing Animation Sequence can remain as input while you add corrections.

> **How to tell that layering worked:** when you move the playhead through the existing motion, the character keeps the original dance pose. Moving a `CRMMD_` control creates a correction at that frame. If the character jumps back to its reference pose as soon as the Rig is added, the track was probably not converted to Layered, or its section does not overlap the Animation section.

## 3. Add Corrections on Top of the Original Motion

### 1. Correct the body and limbs

1. Move the playhead to the frame that needs correction and observe the base pose first.
2. Select the `CRMMD_` controls you need. Common body controls are `CTRL_Chest`, `CTRL_UpperChest`, `CTRL_Pelvis`, `CTRL_Neck`, and `CTRL_Head`.
3. For arms, use `CTRL_UpperArm_L/R`, `CTRL_LowerArm_L/R`, and `CTRL_Hand_L/R`. Rotate them in the order upper arm, lower arm, then hand, using small changes. This corrects only the necessary bones instead of recreating the complete motion.
4. When the pose looks right, press `S` or click the key button for the controls you changed. Key only the controls that need correction; do not key every control at once.
5. Move to the next correction frame and repeat. Sequencer interpolates between keys. To hold a correction, key the same correction at the beginning and end of the hold.

### 2. Correct feet, fingers, and gaze

- If a foot passes through the floor, make a small adjustment on `CTRL_LegIK_L/R` or the corresponding leg/foot control. Use `CTRL_Toe_L/R` only when the toe direction itself needs correction.
- If the hand shape is wrong, key only the necessary `CTRL_Thumb_*`, `CTRL_Index_*`, and similar finger controls. Do not fill the whole arm with unnecessary keys.
- If the gaze is wrong, use `CTRL_Eye_L` and `CTRL_Eye_R`, then key the face control channels.

### 3. Add expressions

1. Expand the `CTRL_Face_` group and locate the eye, mouth, brow, or other face sliders.
2. Adjust a slider at the frame that needs a change and click the key button next to its channel. Face controls usually use a range from 0 to 1.
3. To soften an expression, use a smaller value on neighboring frames. To return to the original expression, reduce the upper-layer correction and bring its value gradually back to 0 near the end of the correction.

## 4. Manage the Layered Tracks

### Track Order

- With one `CRMMD_` layered track, the default settings are normally enough.
- With multiple Control Rig tracks, right-click a track to adjust **Order**. The editor starts at 100; use distinct order values for multiple layers and preview the result to confirm which correction wins.
- If a later ordinary, non-layered Control Rig track overrides an earlier correction, convert it to Layered or review the Order values. Leave the base Animation section unchanged and keep corrections in layered tracks whenever possible.

### Correction Weight

- Right-click the Control Rig track and enable **Weight** to control the strength of the whole layer.
- A weight of 0 temporarily disables the layer. Animating the weight upward lets a correction fade in instead of appearing suddenly.
- Weight keys are stored in the Level Sequence. To disable an experiment, set its weight to 0 rather than deleting the entire track.

### Edit Only Part of the Motion

Trim the Control Rig section to the frames that need correction. Outside that range, the original Animation Sequence continues to play. This is useful for fixing one chorus move, one foot plant, or one expression section.

## 5. Curves and Undo

1. Expand the `CRMMD_` track, select a translation, rotation, or expression channel, and adjust its interpolation and tangents in Curve Editor.
2. If a correction is too strong, undo the latest control change or delete that control's keys in the current range. Do not delete the base Animation section.
3. To compare the original and corrected versions, temporarily disable the layered Control Rig section or set its Weight to 0. Re-enable it after checking.
4. If you want to bake the final result into a new reusable animation asset, use **Bake Animation Sequence** on the character's Skeletal Mesh track. You do not need to bake anything just to export VMD; the current Sequencer can be exported directly.

## 6. Preview, Save, and Export

1. Start playback before the correction range and confirm that the original timing is unchanged and the correction appears only on the intended frames.
2. Check for sudden arm flips, feet passing through the floor, expression jumps, and abrupt starts or ends at the correction range.
3. Save the Level Sequence. The base Animation Sequence and the upper `CRMMD_` control keys are evaluated together by Sequencer.
4. To take the combined character motion back to MMD, continue with [09 Exporting Character Motion VMD](09-export-motion.md). Choose the current Sequencer as the source; the exporter reads the **final authoring-layer pose and expressions** after the base motion and layered Control Rig have been evaluated. PMX CCD, Grants, and physics from the Post Process AnimBP are not baked back into ordinary VMD bone tracks.

The automatic binding algorithm for these model controllers is the result of substantial work by `RedialC`. If you use this Rig to produce motion data and find it useful, please credit the tool so more people can discover this modern MMD keyframing workflow.

## Common Problems

### The original motion disappears after adding `CRMMD_`

Check that the Control Rig track was added through **Control Rig > Layered** or converted with **Convert To Layered**. Also check that the Control Rig section overlaps the Animation section and that you did not accidentally add an ordinary non-layered track.

### The layered controls do not line up with the character

Make sure you are using the `CRMMD_ModelName` that belongs to the current Skeletal Mesh and that the playhead is inside the shared range of the Animation and Control Rig sections. After adding the track, move to a middle frame of the existing motion and check whether the controls follow the character.

### I only wanted to fix the arms, but the whole body was overwritten

Delete unnecessary control keys and keep only the upper-arm, lower-arm, and hand corrections. Do not add unrelated keys to Root, Center, or Pelvis. The purpose of layered editing is for the upper layer to record only what needs to change.

## Next Step

When the correction is complete, use [09 Exporting Character Motion VMD](09-export-motion.md) to export the final Sequencer motion and expressions as VMD. For camera export, continue with [10 Exporting a Camera](10-export-camera.md).
