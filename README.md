# MMD2Unreal

[简体中文](README.md) · [English](README_EN.md) · [日本語](README_JA.md)

## 项目简介
![LOGO](Docs/UserManual/DocPic/Icon128.png)

**MMD2Unreal** 是一个面向 **Unreal Engine 5.8** 的 MikuMikuDance MMD兼容插件。

目标是让 MMD 用户不需要先经过 Blender 或其他中间软件，就可以把 **PMX 模型、VMD 角色动作和 VMD 相机**直接带入 Unreal Engine，并把 在UE中创作的内容从UE导出回归到MMD社区。


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
- ✅ PMX 导入后自动创建 `IKR_<MeshName>` IK Rig（含 Full Body IK 解算器堆栈与手/足/趾 Goal），可用于把角色重定向到其他骨架；导入流程不再创建 `RTG_<MeshName>` IK Retargeter，需要时可在编辑器中基于该 IK Rig 手动生成。
- ✅ 自动创建并绑定 `ABP_Post_<MeshName>` 后处理动画蓝图。
- ✅ PMX 导入后自动生成并绑定 `ABP_Post_<MeshName>`，实时处理 IK、Grant 与物理效果；重复导入会修复并保持节点唯一。
- 🟡 SDEF / QDEF 数据可以读取，目前使用 UE 线性权重近似显示。


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
- 🟡 IK Retargeter 的目标骨架链映射仍可能需要按项目角色人工校正。


## 文档与手册


- **[用户手册](Docs/UserManual/README.md)** —— 从下载安装到导入模型、动作与镜头、使用 Control Rig 创作和编辑动作，再到导出 VMD 的分步教程，无需任何技术背景

## 已知限制

- SDEF / QDEF 目前使用线性权重近似，因此部分模型的变形结果可能和 MMD 原版存在差异。
- Vertex Morph，以及可递归展开为 Vertex Morph 的 Group / Flip Morph，会生成 Unreal Morph Target；Bone / UV / Material / Impulse Morph 尚未生成对应 UE 功能。
- 不同模型使用的特殊骨骼、复杂 IK 或非标准结构可能仍需要继续做兼容与结果校正。


## 致谢

MMD 数据格式与 Unreal 工程实践中的许多细节，受益于以下开源项目的公开成果：
- **[MMD Tools](https://github.com/MMD-Blender/blender_mmd_tools)**
- **[OpenMMD](https://github.com/dskjal/OpenMMD)**
- **[VRM4U](https://github.com/ruyo/VRM4U)**
- **[AMAP5](https://github.com/hatoghx/AMAP5)**

需要强调：MMD2Unreal 是独立实现的项目，并非上述项目的移植、Fork 或复刻。
