# MMD2Unreal

[简体中文](README.md) · [English](README_EN.md) · [日本語](README_JA.md)

## 项目简介
![LOGO](Docs/UserManual/DocPic/Icon128.png)

**MMD2Unreal** 是一个面向 **Unreal Engine 5.8** 的 MikuMikuDance MMD兼容插件。

目标是让 MMD 用户不需要先经过 Blender 或其他中间软件，就可以把 **PMX 模型、VMD 角色动作和 VMD 相机**直接带入 Unreal Engine，并把 在UE中创作的内容从UE导出回归到MMD社区。

## V2 功能截图

| 内容 | 截图与说明 |
| --- | --- |
| 手臂约束修正 | ![手臂约束修正](Docs/UserManual/DocPic/ArmCons.jpg) `M2U Arm Constraint Correction` 在 Post Process AnimBP 中处理身体、头部、腿部与对侧手臂避让。 |
| 裙摆物理 | ![KawaiiPhysics 裙摆链](Docs/UserManual/DocPic/Kawaii_skirtNew.jpg) PMX 刚体、Joint 与可用的横向链间约束会被转换为 KawaiiPhysics 设置。 |
| SDEF 肩部 | ![SDEF 肩部对比](Docs/UserManual/DocPic/Sdef_Compare1.jpg) V2（左）展示原生 SDEF 姿态修正在肩部弯曲处的效果。 |
| SDEF 腿部 | ![SDEF 腿部对比](Docs/UserManual/DocPic/Sdef_Compare2.jpg) V2（左）展示姿态修正对腿部弯曲区域的改善。 |
| SDEF 手部 | ![SDEF 手部对比](Docs/UserManual/DocPic/Sdef_Compare3.jpg) V2（左）展示姿态修正对手腕与手部区域的改善。 |
| 复杂混合表情 | ![表情 Morph 对比](Docs/UserManual/DocPic/MorphCompare.jpg) V2（左）支持复杂混合表情；部分特殊表情现在能得到更接近 MMD 的组合效果。 |

## V2 更新摘要

V2 累计包含 **376 项 BUG 修复、功能提升与性能优化**。以下列出其中面向使用者的主要更新。

**导入、模型与资产**

1. 支持 PMX 2.0 / 2.1 的完整模型导入。
2. 生成 UE 原生 Skeletal Mesh 与 Skeleton 资产。
3. 保留 PMX 的日文骨骼名称和 MMD 根骨骼语义。
4. 导入贴图并生成基础材质与材质实例。
5. 修复导入后立即更换材质会导致表情消失的错误。
6. 导入模型注释并生成可阅读的 `RM_<MeshName>` 文本资产。
7. 为标准人体模型自动准备 IK Rig。
8. 自动生成并绑定 `ABP_Post_<MeshName>` 后处理动画蓝图。
9. 重复导入时修复自动生成图中的重复节点。
10. 生成的模型、动画与 Rig 都保持 UE 原生资产形态。

**SDEF、QDEF 与表情**

11. SDEF 改用 UE 原生蒙皮与姿态修正数据。
12. SDEF 姿态修正覆盖导入时的 LOD0。
13. 肩部弯曲处的 SDEF 表现得到改善。
14. 腿部弯曲处的 SDEF 表现得到改善。
15. 手腕与手部弯曲处的 SDEF 表现得到改善。
16. QDEF 保留四骨权重并按 BDEF4 线性蒙皮近似处理。
17. SDEF 与 QDEF 混合的模型可以导入。
18. 移除开发期 PMXD 资产生成路径，改善实时预览兼容性。
19. Vertex Morph 会生成 UE Morph Target。
20. Group / Flip Morph 会递归展开其引用的 Vertex Morph。
21. 复杂混合表情与部分特殊表情的组合效果更接近 MMD。
22. 骨骼表情和嵌套组合可通过 Skeleton 曲线与后处理蓝图实时驱动。

**Control Rig、IK Rig 与双向 RTG**

23. 自动生成 `CR_` 手部 IK Control Rig。
24. 自动生成 `CRMMD_` 三段手臂 FK Control Rig。
25. 两套 Control Rig 都可为全部可导入的 Vertex Morph 创建表情控制器。
26. 表情控制器按眉、眼、口、其他与未分类面板组织。
27. 修复 Control Rig 首次使用需要预热的问题，生成后即可直接使用。
28. 自动生成 Manny → MMD 的 `RTG_<模型名>`。
29. 自动生成 MMD → Manny 的 `RTG_<模型名>_ToManny`。
30. 两个方向的 RTG 分别配置 Source / Target IK Rig 与独立操作栈。
31. 双向 RTG 自动包含基础口型与眨眼的 Curve Remap。
32. RTG 保留 MMD 身体根、Root Motion 与足部 IK 的工作流。

**动作、物理与姿态修正**

33. VMD 骨骼动作按 MMD Bezier 插值导入。
34. VMD Morph / 表情曲线可随角色动作导入。
35. 支持 30 FPS 与 60 FPS 的角色动作生成。
36. PMX Grant、CCD IK 与 IK Link Limit 在 Post Process AnimBP 中实时求值。
37. 新增 `M2U Arm Constraint Correction` 手臂约束修正节点。
38. 手臂约束修正可减少手臂与躯干、头部、腿部及对侧手臂的穿插。
39. 双臂交叉时可使用优先级与柔和区间降低突兀推开。
40. 自动将 PMX 刚体、Joint 与动态骨骼转换为 KawaiiPhysics 工作流。
41. 裙摆的可用横向 Joint 会转换为链间约束。
42. 识别为头发的动态链会自动取消普通角度上限。

**Reimport、可靠性与性能**

43. 新增 PMX 模型 Reimport 系统。
44. Reimport 可选择是否覆盖模型与 Skeleton 核心资产。
45. 材质与贴图可作为独立类别选择覆盖。
46. PhysicsAsset 可作为独立类别选择覆盖。
47. Post Process AnimBP 可作为独立类别选择覆盖。
48. IK Rig / RTG 与 Control Rig 可作为独立类别选择覆盖。
49. 未选择覆盖的已有附属资产会被保护，不由插件主动重建或删除。
50. Reimport 改善了更新体验；但作者 RedialC 仍建议尽可能避免在 UE 中使用覆盖导入，重要手工修改应先备份或另存资产。
51. 其余若干项导入稳定性、资产生成、动画求值、兼容性与性能提升。

## 特色功能

MMD2Unreal 不只是把 PMX / VMD 文件转换成 UE 资产，而是尽量把 MMD 角色真正需要的动画、Rig、物理和往返工作流一起准备好：

- **自动创建完整 Rig 工作流**：导入 PMX 后自动生成 IK Rig、Manny ↔ MMD 双向 IK Retargeter，以及 `CR_` / `CRMMD_` 两套 Control Rig，直接用于 Sequencer 调姿势、重定向和二次编辑动作。
- **VMD 数据双向兼容**：支持角色动作、表情与相机从 MMD 导入 UE，也支持把 UE 中的 Animation Sequence / Sequencer 结果重新导出为 VMD，方便继续回到 MMD 生态使用。
- **MMD ↔ ARKit 基础表情互转**：生成的双向 RTG 会预设 `あ / い / う / え / お / まばたき` 与常用 ARKit 口型、眨眼曲线的粗略双向映射。
- **手臂约束修正**：自动减少上臂、前臂和手掌穿进躯干、颈部、头部或另一侧手臂的情况，并保留 Debug Draw 和可调避让参数；复杂腕轴、肘轴、捩骨和辅助骨会继续按照模型自身的 PMX 依赖关系求值。
- **PMX IK / Grant / 辅助骨实时求值**：不把模型作者设计的 CCD IK、追加旋转 / 位移和 IK Link Limit 简单烘焙丢失，而是在自动生成的 Post Process AnimBP 中继续实时执行。
- **KawaiiPhysics 自动物理工作流**：根据 PMX 刚体、Joint 与动态骨骼自动生成 PhysicsAsset、碰撞组和 KawaiiPhysics 节点；裙摆等多骨链结构还会把可用的横向 Joint 转换为链间约束，让相邻裙片共同保持布料结构。
- **SDEF 与 Morph、QDEF 近似支持**：SDEF 使用原生姿态修正，保留 Vertex / Group / Flip Morph 关系；QDEF 暂转换为 BDEF4 线性蒙皮，存在变形差异。
- **可控 Reimport**：重新导入 PMX 时可分别选择是否覆盖模型、材质、PhysicsAsset、Post Process AnimBP、IKR / RTG、Control Rig 等生成资产，不需要为了更新一个模块重做整套工程。
- **尽量保持 UE 原生体验**：生成 Skeletal Mesh、Animation Sequence、Level Sequence、Control Rig、IK Rig、IK Retargeter 与 Animation Blueprint 等 UE 原生资产，不需要额外维护一套独立运行窗口。

## 目标功能

> ✅ 已实现　　🟡 已有基础实现

### PMX 模型

- ✅ 导入 PMX 2.0 / 2.1 模型。
- ✅ 读取模型、顶点、面、纹理、材质、骨骼、Morph、显示枠、刚体、Joint 等主要 PMX 数据。
- ✅ 生成 Unreal Engine 原生 Skeletal Mesh 与 Skeleton。
- ✅ 保留 MMD 日语骨骼名称，并统一生成唯一根骨骼 `全ての親`。
- ✅ 支持 BDEF1 / BDEF2 / BDEF4 骨骼权重。
- ✅ 导入主纹理并生成基础 Material / Material Instance。
- ✅ 将 Vertex Morph 生成为 Unreal Morph Target；Group / Flip Morph 会递归展开其中引用的 Vertex Morph，保持组合权重。
- ✅ PMX 导入后自动创建 `IKR_<MeshName>` IK Rig（含 Full Body IK 解算器堆栈与手/足/趾 Goal），并使用插件内置的精简 UE5 Manny 资源直接生成 `RTG_<MeshName>`（Manny → MMD）与 `RTG_<MeshName>_ToManny`（MMD → Manny）。
- ✅ 自动创建并绑定 `ABP_Post_<MeshName>` 后处理动画蓝图。
- ✅ PMX 导入后自动生成并绑定 `ABP_Post_<MeshName>`，实时处理 IK、Grant 与物理效果；重复导入会修复并保持节点唯一。
- ✅ SDEF 使用 UE 原生蒙皮、生成的 Corrective Morph 和最终姿态驱动；QDEF 按四骨权重转换为 BDEF4 线性蒙皮。SDEF+QDEF 混合模型可以导入，不再生成 PMXD 资产。
- ✅ 骨骼表情及嵌套组合通过 Skeleton 曲线和后处理动画蓝图实时驱动。
- ✅ 两套 Control Rig 按 PMX 眉、眼、口、其他、未分类面板为 Vertex Morph 生成表情滑块；其他类型表情仍保留在运行时表情系统中。
- ✅ 两个方向的 RTG 会分别设置 Source/Target IK Rig、骨盆与 Root Motion、链映射和目标 IK；反向资产不会沿用只适用于 MMD Target 的操作。
- ✅ 两个方向的 RTG 同时预设基础表情 Curve Remap，可在 MMD `あ / い / う / え / お / まばたき` 与 ARKit 常用口型、眨眼曲线之间做粗略的双向表情传递。


### VMD 角色动作

- ✅ 导入 VMD 骨骼动作。
- ✅ 导入 VMD Morph / 表情曲线。
- ✅ 支持 CP932 / 日语名称。
- ✅ 支持 30 FPS 与 60 FPS 动作生成。
- ✅ 按 MMD Bezier 插值计算动作，而不是只复制关键帧。
- ✅ 逐帧采样原始骨骼轨道，保留每骨骼独立轨道、参考姿势局部位移、Bezier 插值与四元数连续性。
- ✅ PMX Append Translation / Rotation（Grant）、CCD IK 与 IK Link Limit 在后处理 AnimBP 中实时求值，不烘焙进 Animation Sequence。
- ✅ 保留 全ての親 / センター / グルーブ（以及 root/center/groove）各自位移，不合并轨道。
- ✅ 可将角色动作与表情拆分为主动作和独立 `_Expression` 表情动画。
- ✅ 可从 Unreal Animation Sequence 或当前 Sequencer 导出 VMD 角色动作。

### VMD 相机

- ✅ 导入 VMD Camera 数据。
- ✅ 自动生成 Unreal Level Sequence 与 Cine Camera。
- ✅ 生成相机位置、旋转、视角 / 焦距、透视 / 正交模式和 Camera Cut。
- ✅ 支持 30 FPS 与 60 FPS 相机序列。
- ✅ 保留 MMD 相机 Bezier 插值语义。
- ✅ 60 FPS 下对 MMD 相邻源帧切镜进行保护，避免在硬切之间生成额外的错误中间帧。
- ✅ 支持 VMD 正交相机并生成 Ortho Width。
- ✅ 从当前 Sequencer Camera Cut 的最终求值结果导出 VMD 。


### Unreal 工作流

- ✅ PMX、角色 VMD、相机 VMD 都可以从 Content Browser 直接导入。
- ✅ 生成的模型、动画和相机使用 Unreal 原生资产，可继续参与普通 UE 制作流程。
- ✅ 生成的资产中保留必要的 MMD 来源信息。
- ✅ 再次导入同一 PMX 时，会重建后处理 AnimBP 中的 IK / 物理节点并清除重复项。
- ✅ 当前打开的 Level Sequence 可以从 Sequencer 顶部 `M2U > Export Camera VMD...` 输出为标准 Camera VMD。
- 🟡 非标准骨架或特殊链结构仍可能需要按具体角色校正 IK Retargeter。


## 文档与手册


- **[用户手册](Docs/UserManual/README.md)** —— 从下载安装到导入模型、动作与镜头、使用 Control Rig 创作和编辑动作，再到导出 VMD 的分步教程，无需任何技术背景

## 已知限制

- SDEF 姿态修正目前只覆盖导入 LOD0，后续手工生成的简化 LOD 尚无对应修正数据。QDEF 暂转换为 BDEF4 线性蒙皮，不能完整还原双四元数变形，扭转和弯曲时可能比 MMD 更容易收缩或压扁。开发过程中曾采用 PMXD Mesh Deformer 方案，但考虑到实时预览性能与大量兼容性问题，目前暂时移除了该方案。详见 [开发与验证说明](Docs/DEVELOPMENT_AND_TESTING.md)。
- Vertex Morph，以及可递归展开为 Vertex Morph 的 Group / Flip Morph，会生成 Unreal Morph Target；Bone / UV / Material / Impulse Morph 尚未生成对应 UE 功能。
- 不同模型使用的特殊骨骼、复杂 IK 或非标准结构可能仍需要继续做兼容与结果校正。


## 致谢

MMD 数据格式与 Unreal 工程实践中的许多细节，受益于以下开源项目的公开成果：
- **[MMD Tools](https://github.com/MMD-Blender/blender_mmd_tools)**
- **[OpenMMD](https://github.com/dskjal/OpenMMD)**
- **[VRM4U](https://github.com/ruyo/VRM4U)**
- **[AMAP5](https://github.com/hatoghx/AMAP5)**

需要强调：MMD2Unreal 是独立实现的项目，并非上述项目的移植、Fork 或复刻。
