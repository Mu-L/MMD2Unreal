# 09 Exporting Character Motion VMD

The previous chapters brought MMD content into Unreal. This chapter goes the other way: save character motion you adjusted in Unreal as a character-motion `.vmd` file that MMD can open directly. Body motion and facial expressions can be taken back together.

> **Note:** This manual was translated by AI from the Chinese original, which is the authoritative version. Some expressions may not be fully accurate.

## Before You Start

- You have already imported the matching `.pmx` model with this plugin.
- To export a Sequencer performance, place the character in a Level Sequence and open that sequence in Sequencer.
- To export a standalone Animation Sequence, set the matching MMD Skeletal Mesh as its **Preview Mesh** first. The mesh must come from the same MMD model as the animation.

## Two Ways to Export

### Export Character Motion from Sequencer

![Motion export from Sequencer](../DocPic/Motion_Out1.jpg)

1. In the Content Browser, **double-click** the Level Sequence you want to export so it opens in Sequencer.
2. Make sure the character you want is present on the timeline.
3. Click the **M2U** button in the Sequencer toolbar and choose **Character Motion...**.
4. Set the options as explained later in this chapter.
![Motion export settings](../DocPic/Motion_Out2.jpg)
5. Click **Export VMD**, choose a location and file name. The default name is the character name followed by `.vmd`.
6. Wait for the progress bar to finish. A result report appears when the export is done.

### IK, Post Process, and VMD compatibility

Character VMD export preserves **MMD authoring data** instead of baking the already post-processed Unreal leg pose back into VMD.

- Sequencer export temporarily disables the Post Process AnimBP while skeletal transforms are sampled.
- Animation Sequence export reads the animation's own bone tracks.
- Foot/toe IK controller tracks are therefore exported as controls, while PMX CCD, Grants, and physics are evaluated again by the model after the VMD returns to MMD.
- This avoids solving the same IK once in Unreal and then a second time in MMD.
### Export Character Motion from an Animation Sequence

![Motion export from Animation Sequence](../DocPic/Motion_Out3.jpg)

1. Select **one** Animation Sequence in the Content Browser.
2. Right-click it and choose **Export Motion VMD...**.
3. In the dialog, confirm that the Preview Mesh is the matching MMD model, then set the options later in this chapter.
![Animation export settings](../DocPic/Motion_Out4.jpg)
4. Click **Export VMD**, choose a location and file name. The default name is the Animation Sequence name followed by `.vmd`.
5. Wait for sampling to finish and read the result report.

If **Export Motion VMD...** is not in the context menu, make sure you selected one Animation Sequence rather than a folder, model, or multiple animations. Also assign the matching **Preview Mesh** first.

## What Each Option Means

### Character / Animation

- When exporting from Sequencer, the **Character** dropdown lists the Skeletal Mesh bindings in the sequence. Pick the character you actually want to export.
- When exporting from an Animation Sequence, the dialog shows the selected animation and does not require another character selection.

### Reference Actor (Sequencer only)

A character may already be placed somewhere in the scene. The reference settings tell the plugin which object defines the character's overall position while exporting. In most cases, choose the same Actor as the selected character.

#### Reference Source

- **Actor Transform**: use the whole Actor's position and rotation. This is the simplest choice and is recommended for a first export.
- **Component Transform**: use the position and rotation of a named component on the Actor.
- **Bone / Socket Transform**: use a named bone or Socket on the character. The name field defaults to `全ての親`, the model's root bone.

#### Reference Evaluation

- **Lock at Export Start**: use the reference position at the beginning of the export as the baseline. This is useful when the character was merely placed in the scene before performing; later movement on the timeline can still be recorded.
- **Follow Every Frame**: use the reference object as the baseline again on every frame. This is useful when the character Actor itself moves and you only want the relative performance exported.

When unsure, use the same character, **Actor Transform**, and **Lock at Export Start**.

### Frame Range (Sequencer only)

- **Current Sequencer Range**: export the timeline's playback range.
- **Custom Range**: enter a start and end frame to export only part of it. Custom frames must be inside the current playback range.

When exporting an Animation Sequence, the complete animation length is used and no separate frame range is needed.

### Key Generation Algorithm

This controls how many keys the VMD contains and how long the export takes:

- **Curve Fitting Solver**: reconstruct the motion with relatively few keys. Use this when you care about fidelity and expect to keep using the motion in MMD. It takes longer to calculate and is the default for Sequencer exports.
- **Fast Linear Interpolation**: exports quickly and produces a moderate number of keys. It is useful for a quick check; later fine-tuning in MMD may be less natural than with curve fitting. It is the default for Animation Sequence exports.
- **Complete Per-frame Baking**: writes data on every frame. The file becomes much larger and may be too large for MMD to load, so it is generally not recommended.

#### Advanced Curve Fitting (Curve Fitting Solver only)

When **Curve Fitting Solver** is selected, you can expand the initially collapsed **Advanced Curve Fitting** section. These four controls define the trade-off between fewer keys and a closer reproduction of the original motion. The default values are normally the safest choice. The section is hidden and its controls are not used with **Fast Linear Interpolation** or **Complete Per-frame Baking**.

| Setting | Default (range) | What it does and how to adjust it |
|---|---:|---|
| **Position Tolerance (cm)** | 0.10 (0.01–10) | The allowed position fitting error between retained keys. Lower it to preserve more translation detail, at the cost of more keys, a larger file, and a slower export; raise it to reduce more keys. The value is in Unreal centimetres and is adjusted internally for the model's import scale. |
| **Rotation Tolerance (deg)** | 0.20 (0.01–10) | The allowed bone-rotation fitting error. Lower it for fingers, the head, or other subtle rotations; raise it carefully when you mainly want to compress ordinary body motion. |
| **Morph Fitting Tolerance** | 0.01 (0.0001–0.25) | The allowed error while linearly fitting morph weights. A lower value preserves more mouth, blink, and expression changes; a higher value merges more nearly identical morph keys. |
| **Morph Zero Threshold** | 0.005 (0–0.10) | Morph weights whose absolute value is at or below this threshold are treated as zero, which removes tiny floating-point noise. A higher value reduces meaningless morph data, but can remove very subtle expressions. |

Export with the defaults and inspect the result in MMD first. If a detail was simplified, lower only the related tolerance. If the file is too large or export is too slow, raise the position, rotation, or morph fitting tolerance gradually. Avoid raising every control substantially at once, because it makes quality problems difficult to identify.

### Bone Groups Participating in Export

The dialog lists bone groups from the model's PMX file and groups you created yourself. Tick the groups you want written to the VMD; at least one group must remain selected. Unclassified bones are excluded by default.

The defaults avoid groups whose names look like hair, skirt, or physics groups, because those parts are usually calculated by the physics system during playback.

- **Include Physics Bones**: off by default. When off, dynamic PMX physics bones are excluded even if their group is selected. In the current version, turning it on only allows existing animation tracks on those bones to be exported; it **does not bake live KawaiiPhysics / Post Process simulation**.
- **Include Morph Motion**: on by default. Includes model expressions such as blinking and mouth shapes. Turn it off to export bone motion only.
- **Always export whole-body displacement bones**: on by default. In MMD the character's whole-body motion is carried by `全ての親` / `センター` / `グルーブ` / `腰`. While it is on, those bones are written to the VMD even when their group is not ticked, so the character's overall displacement is not lost. Turn it off to exclude them; the report then warns when they still carry displacement.

The **Model Data** status at the bottom must be available. It comes from the information created when the model was imported and ensures that bones and expressions are exported with the correct MMD names.

### Work Estimate

The dialog shows the estimated number of bones and frames involved, along with an approximate duration. It is only an estimate: model complexity and computer speed affect the actual time. Curve fitting and complete per-frame baking generally take longer.

Character motion exports use MMD's usual 30 FPS time base; there is no separate export frame-rate setting.

## Reading the Result

After a successful export, the VMD file is saved at the location you chose. The report contains:

| Number | Meaning |
|---|---|
| Dense samples | Motion frames actually checked by the plugin |
| Candidate bones | Bones found in the groups you selected |
| Excluded physics bones | Dynamic physics bones left out by the default filter |
| Exported bone tracks | Bones finally written to the VMD |
| Bone keys | Character-motion keys in the output file |
| Morph keys | Expression keys in the output file |

The file may still have been written successfully when warnings appear. Common examples are a bone missing from the mesh, an expression name that does not meet VMD naming limits, or bone scaling that VMD cannot store. Those parts are skipped; see the Output Log for details.

## Common Problems

### The M2U menu is missing when exporting character motion

- Before exporting from Sequencer, open a Level Sequence in Sequencer and make sure it contains a Skeletal Mesh character.
- To export a standalone animation, select exactly one Animation Sequence in the Content Browser and right-click to find **Export Motion VMD...**.

### Character-motion export says Model Data is unavailable or the Preview Mesh does not match

Character-motion export needs the Model Data created when the model was imported. Make sure the Animation Sequence **Preview Mesh** is the Skeletal Mesh from the same MMD model. If Model Data is missing or unavailable, restore the original `.pmx`, re-import the model, and export again.

### Expressions are missing from the exported motion

Turn on **Include Morph Motion** in the character-motion export dialog. If the report still shows zero morph keys, check that **Import Expressions** was enabled for the model and that the source motion actually contains expression curves.

### Hair or skirt motion is missing after export

Dynamic physics bones are excluded from character-motion exports by default. When you take the file back to MMD, hair and skirt motion is normally recalculated by MMD's physics system; this does not mean ordinary bone motion was lost. If those physics bones already contain authored animation tracks, **Include Physics Bones** can write those tracks into the VMD. The current version does not automatically bake live KawaiiPhysics results.

### Character-motion export is slow or the VMD is very large

In **Key Generation Algorithm**, use **Curve Fitting Solver** when you want fewer keys, and **Fast Linear Interpolation** when you want a quick check. Avoid **Complete Per-frame Baking** unless you need it: it writes every frame and may make the VMD too large for MMD to load.

### Motion is stable after changing interpolation to linear in MMD, but jitters with curve interpolation

The current version writes bone Bézier interpolation using the standard MMD VMD layout. VMD files exported by an older version may show a specific symptom: changing the keys to linear interpolation in MMD makes the motion stable, while keeping the non-linear curves causes visible jitter. Re-export the VMD with the current version. You do not need to keep the motion linear or switch to Complete Per-frame Baking as a workaround.

### Feet still slide or ankles still twist in an old VMD after updating the plugin

If the VMD was produced from a Retarget Animation that had already been exported / baked before the update, its Animation Sequence still contains the old foot IK / toe IK controller tracks. Updating the plugin does not rewrite an existing animation. Export / bake the Animation Sequence again from the current `RTG_<ModelName>`, then export the VMD from that new animation.

For these foot-target fixes alone, an existing RTG normally does not need to be rebuilt. If you also want **Leg Prebend → Enable Knee Prebend** and the older RTG does not show that option or reports missing knee data, reimport the PMX and overwrite **IK Rig / IK Retargeter**.

### The exported bone count is lower than expected

Check that all required bone groups are ticked. Unclassified bones are excluded by default, and dynamic physics bones are also excluded while **Include Physics Bones** is off. The “Excluded physics bones” number in the report can help confirm this.

### The character's overall position is wrong in MMD while the limbs look correct

Check the notes in the export report first. MMD carries whole-character motion on bones such as `全ての親` and `センター`. When those bones are missing from the VMD, the character stays in place or only sways on the spot, while the limb motion still looks right. Keep **Always export whole-body displacement bones** on, or make sure the groups that contain those bones are ticked.

If the report says foot / toe IK controllers stay static over the whole animation, the foot goals were never produced for that animation (common when the retargeted animation was not exported again after rebuilding the RTG). Rebuild the RTG in Unreal, export the character animation again, and then export the VMD.

### What can a character-motion VMD contain?

It contains character bone motion and expressions that exist on the model. For camera data, see [10 Exporting a Camera](10-export-camera.md). Other MMD content such as materials and stages cannot currently be exported back to MMD.

## Notes When Taking the File Back to MMD

- Open the VMD with the same model used for export so Japanese bone and expression names match correctly.
- Dynamic physics bones are excluded by default, so hair and skirt motion is normally recalculated by MMD's physics system during playback. This does not affect ordinary bone motion.
- This feature exports character motion and expressions, not camera data. For cameras, see [10 Exporting a Camera](10-export-camera.md).

## Next Step

After exporting character motion, continue with [10 Exporting a Camera](10-export-camera.md) to take the Unreal camera back to MMD.
