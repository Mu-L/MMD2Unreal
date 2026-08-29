# MMD2Unreal

[简体中文](README.md) · [English](README_EN.md) · [日本語](README_JA.md)

## Project Overview
![LOGO](Docs/UserManual/DocPic/Icon128.png)

**MMD2Unreal** is a **Unreal Engine 5.8** MikuMikuDance-compatible plugin.

It lets MMD users bring **PMX models, VMD character motion, and VMD cameras** directly into Unreal Engine without first going through Blender or another intermediate tool, and export content created in Unreal back to the MMD community.

## Goals and Features

> ✅ Implemented　　🟡 Basic implementation available

### PMX Models

- ✅ Import PMX 2.0 / 2.1 models.
- ✅ Read the main PMX data: models, vertices, faces, textures, materials, bones, Morphs, display frames, rigid bodies, and Joints.
- ✅ Generate native Unreal Engine Skeletal Mesh and Skeleton assets.
- ✅ Preserve MMD Japanese bone names and generate one unique root bone, `全ての親`.
- ✅ Support BDEF1 / BDEF2 / BDEF4 bone weights.
- ✅ Import main textures and generate basic Materials / Material Instances.
- ✅ Convert Vertex Morphs into Unreal Morph Targets; recursively expand Vertex Morphs referenced by Group / Flip Morphs while preserving combined weights.
- ✅ Automatically create `IKR_<MeshName>` IK Rig assets after PMX import, including the Full Body IK solver stack and hand/foot/toe Goals. The import flow no longer creates a `RTG_<MeshName>` IK Retargeter; create one manually from the IK Rig when needed.
- ✅ Automatically create and bind `ABP_Post_<MeshName>` post-process Animation Blueprints.
- ✅ Generate and bind `ABP_Post_<MeshName>` after PMX import to evaluate IK, Grants, and physics in real time; repeated imports repair and keep the graph unique.
- 🟡 Read SDEF / QDEF data; display currently uses an approximate Unreal linear-weight result.

### VMD Character Motion

- ✅ Import VMD bone motion.
- ✅ Import VMD Morph / expression curves.
- ✅ Support CP932 / Japanese names.
- ✅ Support 30 FPS and 60 FPS motion generation.
- ✅ Evaluate motion using MMD Bezier interpolation instead of only copying keyframes.
- ✅ Sample original bone tracks frame by frame while preserving independent bone tracks, reference-pose local translation, Bezier interpolation, and quaternion continuity.
- ✅ Evaluate PMX Append Translation / Rotation (Grant), CCD IK, and IK Link Limit in the post-process Animation Blueprint instead of baking them into the Animation Sequence.
- ✅ Preserve separate translation tracks for `全ての親` / `センター` / `グルーブ` as well as `root` / `center` / `groove`.
- ✅ Split character motion and expressions into a main motion asset and an independent `_Expression` animation.
- ✅ Export character motion VMD from an Unreal Animation Sequence or the current Sequencer.

### VMD Camera

- ✅ Import VMD camera data.
- ✅ Automatically generate Unreal Level Sequences and Cine Cameras.
- ✅ Generate camera position, rotation, view angle / focal length, perspective / orthographic mode, and Camera Cuts.
- ✅ Support 30 FPS and 60 FPS camera sequences.
- ✅ Preserve MMD camera Bezier interpolation semantics.
- ✅ Protect adjacent MMD source-frame cuts at 60 FPS so no incorrect intermediate frame is generated between hard cuts.
- ✅ Support VMD orthographic cameras and generate Ortho Width.
- ✅ Export VMD from the final evaluated result of the current Sequencer Camera Cut.

### Unreal Workflow

- ✅ Import PMX, character VMD, and camera VMD directly from the Content Browser.
- ✅ Generated models, animations, and cameras use native Unreal assets and can continue through the normal UE workflow.
- ✅ Generated assets retain the necessary MMD source information.
- ✅ Reimporting the same PMX rebuilds the post-process AnimBP IK / physics graph and removes duplicate nodes.
- ✅ Export the currently open Level Sequence as a standard Camera VMD from `M2U > Export Camera VMD...` in the Sequencer toolbar.
- 🟡 IK Retargeter target-chain mapping may still require manual correction for individual project characters.

## Documentation

- **[User Manual](Docs/UserManual/English/README.md)** — A step-by-step guide covering installation, model, motion, and camera import, Control Rig motion creation and editing, and VMD export.
- [中文用户手册](Docs/UserManual/README.md)
- [日本語ユーザーマニュアル](Docs/UserManual/日本語/README.md)

## Known Limitations

- SDEF / QDEF currently use approximate linear weights, so some models may deform differently from the original MMD result.
- Vertex Morphs and Group / Flip Morphs that can be recursively expanded into Vertex Morphs generate Unreal Morph Targets; Bone / UV / Material / Impulse Morphs do not yet generate corresponding UE features.
- Some models with special bones, complex IK, or non-standard structures may still need compatibility work and result correction.

## Acknowledgments

Many details of the MMD data format and Unreal workflow benefited from the public work of these open-source projects:

- **[MMD Tools](https://github.com/MMD-Blender/blender_mmd_tools)**
- **[OpenMMD](https://github.com/dskjal/OpenMMD)**
- **[VRM4U](https://github.com/ruyo/VRM4U)**
- **[AMAP5](https://github.com/hatoghx/AMAP5)**

MMD2Unreal is an independently implemented project; it is not a port, fork, or reproduction of the projects above.
