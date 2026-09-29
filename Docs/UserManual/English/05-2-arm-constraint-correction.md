# 05-2 Arm Constraint Correction

After you import a character motion, **Arm Constraint Correction** can reduce cases where an arm or hand passes through the body, the head, the legs, or the opposite arm.

> **Note:** This manual was translated by AI from the Chinese original, which is the authoritative version. Some expressions may not be fully accurate.

This feature is mainly intended for:

- upper arms or forearms entering the torso;
- hands entering the chest, neck, head, or legs;
- both arms intersecting when they cross;
- obvious penetration during fast arm motion.

It only corrects the arm pose when avoidance is needed. It does not replace the original animation and it is not a substitute for hair, skirt, or other secondary-motion physics.

## Is It Enabled by Default?

When importing a **Character** PMX, **Rig → Arm Constraint Correction** is enabled by default.

When importing a **Scene** PMX, this option is disabled by default because scene assets normally do not need character-arm correction.

For most characters, leave it enabled first, play the motion, and only tune it if necessary.

## Enabling It on an Existing Model

If the model was imported with an older version, or the node was not generated at the time, reimport the PMX:

1. Reimport the model.
2. Enable **Overwrite existing Post Process AnimBP**.
3. Make sure **Arm Constraint Correction** underneath it is enabled.
4. Complete the reimport.

**Arm Constraint Correction is a child option of the Post Process AnimBP overwrite option.** If **Overwrite existing Post Process AnimBP** is off, MMD2Unreal leaves the existing ABP untouched and does not append this feature separately.

## Where to Adjust It

In the Content Browser, find:

`ABP_Post_<ModelName>`

Open its **AnimGraph** and find the **M2U Arm Constraint Correction (Arm Constraint Correction)** node. In the generated graph it sits after the MMD pose processing and before KawaiiPhysics.

The generated nodes around it preserve model-specific MMD arm, append-bone, and IK relationships. In normal use, do not delete, bypass, or manually reorder these generated nodes.

The node's **Alpha** is the overall strength:

- `1.0`: fully enabled;
- `0.0`: correction disabled;
- values in between: reduced overall correction strength.

Normally, keep it at `1.0`.

## Most Useful Parameters

Most models do not need manual tuning. When a particular motion causes problems, start with these settings.

| Parameter | Default | What it does |
| --- | ---: | --- |
| **Contact Margin (cm)** | `0.8` | Minimum spacing kept between the arm probe and a blocker surface. Higher values keep the arm farther from the body. |
| **Avoidance Soft Zone (cm)** | `0.65` | Range in which correction gradually starts before hard penetration. Smaller values preserve the authored pose longer; larger values start avoidance earlier. |
| **Crossing Guard Distance (cm)** | `32` | Maximum distance the elbow or wrist may move away from the authored pose during one correction. Raise this first when fast or deep motion still snaps through. |
| **Solver Iterations** | `4` | Number of correction passes per frame. Normally leave this unchanged. |
| **Pose Preservation Weight** | `0.35` | How strongly the node tries to keep the authored pose. Higher values reduce unnecessary changes to normal motion. |
| **Max Elbow Bend (deg)** | `155°` | Maximum elbow flexion used during correction, helping reduce elbow flips and excessive folding. |
| **Upper Arm Probe Start** | `0.32` | Fraction of the upper arm ignored near the shoulder so the normal shoulder-to-torso attachment is not treated as penetration. |
| **Hand Rotation Follow** | `0.0` | How much wrist orientation follows the avoidance result. `0` preserves the authored hand orientation best and is the recommended default. |
| **Cross Arm Response Scale** | `0.65` | Overall strength of left/right arm avoidance. Lower values let authored crossings yield more gently; body-collision correction is unchanged. |
| **Cross Arm Soft Zone (cm)** | `3.0` | Distance over which opposite-arm avoidance fades in before contact and the push direction becomes smoother near coincident probes. |
| **Cross Arm Priority** | `0.2` | Which authored arm pose wins during contact, from `-1` to `1`: negative values favor the left arm, `0` splits correction evenly, and positive values favor the right arm. |

Inside the left- and right-arm groups you can also see **Upper Arm Radius / Forearm Radius / Hand Radius**. These are prepared automatically from the model during import and usually do not need manual changes.

If only the hand still penetrates, increase that side's **Hand Radius** slightly, around `10–20%` at a time. Avoid making it much larger at once, otherwise the hand may start avoiding the body too early.

## Collision Toggles

You can disable individual types of checks on the node:

- **Upper Arm Collision**: upper arm;
- **Forearm Collision**: forearm;
- **Hand Collision**: hand;
- **Cross Arm Collision**: the two arms block each other;
- **Neck Collision**: neck;
- **Head Collision**: head;
- **Leg Collision**: legs.

The defaults suit most characters. For troubleshooting, temporarily disabling one item can help identify which range is affecting the pose, but it is usually better not to leave a large collision category disabled just to fix one motion.

## Using Debug Draw

If you are not sure which collision range is affecting the arm, enable **Debug Draw** on the node.

While playing or scrubbing the motion, the viewport shows:

- **red**: actual body, neck, head, and leg blockers;
- **yellow**: the outer activation range where avoidance begins;
- **orange**: the authored arm path;
- **green**: solved upper-arm and forearm probe ranges;
- **cyan sphere**: hand probe range;
- **gray hand sphere**: Hand Collision is currently disabled.

The debug shapes use normal scene depth, so parts behind the mesh are hidden normally. Viewing from several angles makes the spatial relationship easier to understand.

**Debug Draw Alpha** controls the opacity of all debug shapes. Debug Draw is only for inspection and can be turned off for final work.

## Common Tuning Cases

### The arm still suddenly passes through at a certain point

Raise **Crossing Guard Distance** first.

For example:

`32 → 40 → 50`

Increase it gradually until the fast or deep motion is blocked. Very high values allow the pose to move much farther away from the original animation, so do not max it out immediately.

### Normal motion is changed even when there is no obvious penetration

Try these in order:

1. raise **Pose Preservation Weight**, for example `0.35 → 0.45`;
2. reduce **Avoidance Soft Zone**, for example `0.65 → 0.4`;
3. if the problem is mainly near the shoulder, raise **Upper Arm Probe Start** slightly.

Change one parameter at a time and replay the motion.

### The elbow folds too far or feels like it flips

Lower **Max Elbow Bend**, for example:

`155° → 145°` or `135°`

It is also recommended to keep **Hand Rotation Follow = 0** so the wrist does not twist strongly together with forearm correction.

### The hand still enters the body

Enable **Debug Draw** and first check whether the cyan Hand Sphere covers the part of the hand you want to protect.

If the sphere is clearly too small, increase that arm's **Hand Radius** slightly. If the sphere is already large enough but fast motion still crosses through, raise **Crossing Guard Distance** instead of continuing to enlarge the hand sphere.

### The arms penetrate each other when crossed

Make sure **Cross Arm Collision** remains enabled. While the authored pose keeps the normal left/right order, both sides avoid each other according to **Cross Arm Priority** rather than treating one arm as permanently fixed. If the authored motion explicitly crosses the arms, the node smoothly reduces cross-arm avoidance at the crossing so the motion can pass without jitter; arm-to-body collision correction still applies.

## Relationship with MMD IK and KawaiiPhysics

Arm Constraint Correction runs after the model's own MMD pose relationships have been evaluated and before KawaiiPhysics.

That means:

- you do not need to disable MMD IK to use it;
- KawaiiPhysics does not need to be retuned just to make arm correction work;
- models with extra wrist-axis, elbow-axis, twist, or helper bones continue to follow their own model relationships;
- do not delete the generated conversion or MMD constraint nodes surrounding the correction node.

If you only want to compare the result on and off, changing **Arm Constraint Correction → Alpha** is the safest method.

## Limitations

Arm Constraint Correction is designed to reduce common self-penetration while keeping the original animation as much as possible.

Very extreme motion — for example, an arm jumping from one side of the body to the other in a single frame, or an animation that is already severely folded — may still require a higher **Crossing Guard Distance** or a manual animation fix.

It also does not handle collision between different characters.

## Next Step

Once the arm motion looks clean, continue with the camera: [06 Importing a Camera](06-import-camera.md).
