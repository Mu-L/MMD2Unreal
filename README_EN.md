# MMD2Unreal

[简体中文](README.md) · [English](README_EN.md) · [日本語](README_JA.md)

## Project Overview
![LOGO](Docs/UserManual/DocPic/Icon128.png)

**MMD2Unreal** is a **Unreal Engine 5.8** MikuMikuDance-compatible plugin.

It lets MMD users bring **PMX models, VMD character motion, and VMD cameras** directly into Unreal Engine without first going through Blender or another intermediate tool, and export content created in Unreal back to the MMD community.

## V2 Feature Screenshots

| Area | Screenshot and explanation |
| --- | --- |
| Arm Constraint Correction | ![Arm Constraint Correction](Docs/UserManual/DocPic/ArmCons.jpg) `M2U Arm Constraint Correction` in the Post Process AnimBP handles body, head, leg, and opposite-arm avoidance. |
| Skirt physics | ![KawaiiPhysics skirt chains](Docs/UserManual/DocPic/Kawaii_skirtNew.jpg) PMX rigid bodies, Joints, and usable lateral cross-chain constraints are converted into KawaiiPhysics settings. |
| SDEF shoulders | ![SDEF shoulder comparison](Docs/UserManual/DocPic/Sdef_Compare1.jpg) V2 (left) shows native SDEF pose correction at the bent shoulder. |
| SDEF legs | ![SDEF leg comparison](Docs/UserManual/DocPic/Sdef_Compare2.jpg) V2 (left) shows the improved result around the bent legs. |
| SDEF hands | ![SDEF hand comparison](Docs/UserManual/DocPic/Sdef_Compare3.jpg) V2 (left) shows the result around wrists and hands. |
| Complex mixed expressions | ![Expression Morph comparison](Docs/UserManual/DocPic/MorphCompare.jpg) V2 (left) supports complex mixed expressions; some special expressions now combine more closely to the MMD result. |

## V2 Update Highlights

V2 includes **376 bug fixes, feature improvements, and performance optimizations** in total. The following are the primary user-facing changes.

**Import, models, and assets**

1. Complete PMX 2.0 / 2.1 model import.
2. Native UE Skeletal Mesh and Skeleton asset generation.
3. Preservation of Japanese bone names and MMD root-bone semantics.
4. Texture import plus basic materials and material instances.
5. Fixed expression loss when a material is replaced immediately after import.
6. Imported model comments as readable `RM_<MeshName>` text assets.
7. Automatic IK Rig preparation for standard humanoid models.
8. Automatic creation and binding of `ABP_Post_<MeshName>`.
9. Duplicate generated graph nodes are repaired during repeated import.
10. Generated models, animation, and Rig content remain native UE assets.

**SDEF, QDEF, and expressions**

11. SDEF now uses native UE skinning and pose-correction data.
12. SDEF pose correction covers the imported LOD0.
13. Improved SDEF behavior around bent shoulders.
14. Improved SDEF behavior around bent legs.
15. Improved SDEF behavior around wrists and hands.
16. QDEF retains four-bone weights and is approximated through BDEF4 linear skinning.
17. Models mixing SDEF and QDEF can be imported.
18. The development-stage PMXD asset-generation path was removed for improved real-time-preview compatibility.
19. Vertex Morphs generate UE Morph Targets.
20. Group / Flip Morphs recursively expand their referenced Vertex Morphs.
21. Complex mixed expressions and some special expressions combine more closely to the MMD result.
22. Bone expressions and nested groups can be evaluated through Skeleton curves and the post-process blueprint.

**Control Rigs, IK Rigs, and bidirectional RTGs**

23. Automatic `CR_` hand-IK Control Rig generation.
24. Automatic `CRMMD_` three-segment arm-FK Control Rig generation.
25. Both Control Rigs create expression controllers for every importable Vertex Morph.
26. Expression controls are organized into brow, eye, mouth, other, and unclassified panels.
27. Control Rigs no longer require a first-use warm-up and are ready immediately after generation.
28. Automatic Manny → MMD `RTG_<MeshName>` generation.
29. Automatic MMD → Manny `RTG_<MeshName>_ToManny` generation.
30. Both RTG directions configure their own Source / Target IK Rigs and operation stacks.
31. Bidirectional RTGs include basic mouth-shape and blink Curve Remaps.
32. RTGs retain the MMD body-root, Root Motion, and foot-IK workflow.

**Motion, physics, and pose correction**

33. VMD bone motion is imported with MMD Bezier interpolation.
34. VMD Morph / expression curves import with character motion.
35. Character motion can be generated at 30 FPS or 60 FPS.
36. PMX Grants, CCD IK, and IK Link Limits are evaluated in the Post Process AnimBP.
37. Added the `M2U Arm Constraint Correction` node.
38. Arm Constraint Correction reduces penetration into the torso, head, legs, and opposite arm.
39. Crossed arms can use priority and a soft zone to avoid abrupt separation.
40. PMX rigid bodies, Joints, and dynamic bones are converted to the KawaiiPhysics workflow.
41. Usable lateral skirt Joints convert into cross-chain constraints.
42. Dynamic chains recognized as hair automatically remove their ordinary angle limit.

**Reimport, reliability, and performance**

43. Added a PMX model Reimport system.
44. Reimport can choose whether to overwrite the core mesh and Skeleton assets.
45. Materials and textures can be overwritten independently.
46. PhysicsAssets can be overwritten independently.
47. Post Process AnimBPs can be overwritten independently.
48. IK Rigs / RTGs and Control Rigs can be overwritten independently.
49. Existing companion assets not selected for overwrite are protected from plugin-driven rebuild or deletion.
50. Reimport improves update workflows, but author RedialC still recommends avoiding overwrite imports in UE whenever practical; back up or save important manual edits as separate assets first.
51. Numerous further import-stability, asset-generation, animation-evaluation, compatibility, and performance improvements.

## Featured Capabilities

MMD2Unreal is designed as more than a PMX/VMD file converter. It prepares the animation, rigging, physics, and round-trip workflow that MMD characters actually need inside Unreal:

- **Automatic full Rig workflow**: PMX import can automatically create an IK Rig, Manny ↔ MMD bidirectional IK Retargeters, plus `CR_` / `CRMMD_` Control Rigs for posing, retargeting, and editing motion directly in Sequencer.
- **Bidirectional VMD workflow**: import character motion, expressions, and cameras from MMD into Unreal, then export Animation Sequence / Sequencer results back to VMD for continued use in the MMD ecosystem.
- **Basic MMD ↔ ARKit expression transfer**: generated RTGs include approximate two-way mappings between MMD `あ / い / う / え / お / まばたき` and common ARKit mouth-shape / blink curves.
- **Arm Constraint Correction**: automatically reduces upper-arm, forearm, and hand penetration into the torso, neck, head, or opposite arm, with Debug Draw and tunable avoidance controls. Models with extra wrist-axis, elbow-axis, twist, or helper bones continue to follow their PMX dependency relationships.
- **Live PMX IK / Grant / helper-bone evaluation**: CCD IK, Append Rotation / Translation, and IK Link Limits remain active in the generated Post Process AnimBP instead of being flattened away during import.
- **Automatic KawaiiPhysics workflow**: PMX rigid bodies, Joints, and dynamic-bone data are converted into PhysicsAssets, collision groups, and KawaiiPhysics nodes. Multi-chain structures such as skirts also convert usable horizontal PMX Joints into cross-chain constraints so adjacent panels can retain a shared cloth structure.
- **SDEF, Morphs, and approximate QDEF support**: SDEF uses native pose corrections and Vertex / Group / Flip Morph relationships are retained; QDEF currently converts to BDEF4 linear skinning with deformation differences.
- **Selective Reimport**: when updating a PMX, choose independently whether to rebuild the mesh, materials, PhysicsAsset, Post Process AnimBP, IKR / RTG, Control Rigs, and other generated assets.
- **Native Unreal workflow**: generated content uses native Skeletal Mesh, Animation Sequence, Level Sequence, Control Rig, IK Rig, IK Retargeter, and Animation Blueprint assets without requiring a separate standalone runtime window.

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
- ✅ Automatically create `IKR_<MeshName>` after PMX import, including the Full Body IK solver stack and hand/foot/toe Goals, and use the plugin's bundled minimal UE5 Manny content to create `RTG_<MeshName>` for Manny → MMD and `RTG_<MeshName>_ToManny` for MMD → Manny.
- ✅ Automatically create and bind `ABP_Post_<MeshName>` post-process Animation Blueprints.
- ✅ Generate and bind `ABP_Post_<MeshName>` after PMX import to evaluate IK, Grants, and physics in real time; repeated imports repair and keep the graph unique.
- ✅ SDEF uses native UE skinning, generated corrective Morphs, and a final-pose driver. QDEF imports as BDEF4 linear skinning using its four-bone weights. Mixed SDEF+QDEF models can be imported; no PMXD assets are generated.
- ✅ Bone expressions and nested groups drive the evaluated pose through Skeleton curves and the post-process AnimBP.
- ✅ Both Control Rigs expose Vertex Morph sliders grouped by the PMX brow, eyes, mouth, other, and unclassified panels; other expression types remain available to the runtime expression system.
- ✅ The two generated RTGs independently configure their Source/Target IK Rigs, pelvis and root motion, chain mappings, and target IK. The reverse asset does not reuse operations that only apply when PMX is the target.
- ✅ Both RTG directions also include basic facial Curve Remap presets, allowing approximate two-way transfer between MMD `あ / い / う / え / お / まばたき` and common ARKit mouth-shape and blink curves.

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
- 🟡 Non-standard skeletons or unusual chain layouts may still require per-character IK Retargeter correction.

## Documentation

- **[User Manual](Docs/UserManual/English/README.md)** — A step-by-step guide covering installation, model, motion, and camera import, Control Rig motion creation and editing, and VMD export.
- [中文用户手册](Docs/UserManual/README.md)
- [日本語ユーザーマニュアル](Docs/UserManual/日本語/README.md)

## Known Limitations

- SDEF pose corrections currently cover imported LOD0 only; manually generated reduced LODs have no matching corrections. QDEF currently converts to BDEF4 linear skinning rather than reproducing dual-quaternion deformation, so twisting and bending can shrink or flatten the mesh compared with MMD. A PMXD Mesh Deformer solution existed during development, but has been temporarily removed due to real-time preview performance concerns and numerous compatibility issues. See [development and validation notes](Docs/DEVELOPMENT_AND_TESTING.md).
- Vertex Morphs and Group / Flip Morphs that can be recursively expanded into Vertex Morphs generate Unreal Morph Targets; Bone / UV / Material / Impulse Morphs do not yet generate corresponding UE features.
- Some models with special bones, complex IK, or non-standard structures may still need compatibility work and result correction.

## Acknowledgments

Many details of the MMD data format and Unreal workflow benefited from the public work of these open-source projects:

- **[MMD Tools](https://github.com/MMD-Blender/blender_mmd_tools)**
- **[OpenMMD](https://github.com/dskjal/OpenMMD)**
- **[VRM4U](https://github.com/ruyo/VRM4U)**
- **[AMAP5](https://github.com/hatoghx/AMAP5)**

MMD2Unreal is an independently implemented project; it is not a port, fork, or reproduction of the projects above.
