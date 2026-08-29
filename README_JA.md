# MMD2Unreal

[简体中文](README.md) · [English](README_EN.md) · [日本語](README_JA.md)

## プロジェクト概要
![ロゴ](Docs/UserManual/DocPic/Icon128.png)

**MMD2Unreal** は、**Unreal Engine 5.8** 向けのMikuMikuDance互換プラグインです。

Blenderなどの中間ソフトを経由せずに、MMDユーザーが **PMXモデル、VMDキャラクターモーション、VMDカメラ** をUnreal Engineへ直接持ち込み、Unrealで作成した内容をMMDコミュニティへ書き戻せるようにします。

## 目標と機能

> ✅ 実装済み　　🟡 基本実装あり

### PMXモデル

- ✅ PMX 2.0 / 2.1モデルをインポート。
- ✅ モデル、頂点、面、テクスチャ、マテリアル、ボーン、Morph、表示枠、剛体、Jointなど主要なPMXデータを読み込み。
- ✅ Unreal EngineネイティブのSkeletal MeshとSkeletonを生成。
- ✅ MMDの日本語ボーン名を保持し、固有のルートボーン `全ての親` を生成。
- ✅ BDEF1 / BDEF2 / BDEF4ボーンウェイトに対応。
- ✅ メインテクスチャをインポートし、基本Material / Material Instanceを生成。
- ✅ Vertex MorphをUnreal Morph Targetへ変換。Group / Flip Morphから参照されるVertex Morphも再帰的に展開し、合成ウェイトを保持。
- ✅ PMXインポート後に `IKR_<MeshName>` IK Rigを自動生成。Full Body IKソルバースタックと手・足・つま先のGoalを含みます。インポート処理では `RTG_<MeshName>` IK Retargeterを作成せず、必要な場合はIK Rigを基にエディターで手動作成します。
- ✅ `ABP_Post_<MeshName>` ポストプロセスAnimation Blueprintを自動生成してバインド。
- ✅ PMXインポート後に `ABP_Post_<MeshName>` を生成・バインドし、IK、Grant、物理をリアルタイムで処理。再インポート時はグラフを修復して重複ノードを維持しません。
- 🟡 SDEF / QDEFデータを読み込み。表示は現在Unrealの線形ウェイトによる近似です。

### VMDキャラクターモーション

- ✅ VMDボーンモーションをインポート。
- ✅ VMD Morph / 表情カーブをインポート。
- ✅ CP932 / 日本語名に対応。
- ✅ 30 FPSと60 FPSのモーション生成に対応。
- ✅ キーフレームの単純なコピーではなく、MMD Bezier補間でモーションを評価。
- ✅ 元のボーントラックをフレームごとにサンプリングし、ボーンごとの独立トラック、基準ポーズのローカル移動、Bezier補間、クォータニオンの連続性を保持。
- ✅ PMX Append Translation / Rotation（Grant）、CCD IK、IK Link LimitをAnimation Sequenceへベークせず、ポストプロセスAnimation Blueprintでリアルタイム評価。
- ✅ `全ての親` / `センター` / `グルーブ` および `root` / `center` / `groove` の移動をそれぞれ保持し、トラックを統合しません。
- ✅ キャラクターモーションと表情を、メインモーションと独立した `_Expression` アニメーションに分離可能。
- ✅ Unreal Animation Sequenceまたは現在のSequencerからキャラクターモーションVMDを書き出し。

### VMDカメラ

- ✅ VMDカメラデータをインポート。
- ✅ Unreal Level SequenceとCine Cameraを自動生成。
- ✅ カメラ位置、回転、画角 / 焦点距離、透視 / 正投影モード、Camera Cutを生成。
- ✅ 30 FPSと60 FPSのカメラシーケンスに対応。
- ✅ MMDカメラのBezier補間セマンティクスを保持。
- ✅ 60 FPSでMMDの隣接する元フレームのカットを保護し、ハードカット間に誤った中間フレームを生成しません。
- ✅ VMD正投影カメラに対応し、Ortho Widthを生成。
- ✅ 現在のSequencer Camera Cutの最終評価結果からVMDを書き出し。

### Unrealワークフロー

- ✅ PMX、キャラクターVMD、カメラVMDをContent Browserから直接インポート。
- ✅ 生成されるモデル、アニメーション、カメラはUnrealネイティブアセットで、通常のUE制作フローを続けて利用可能。
- ✅ 生成アセットに必要なMMD元データを保持。
- ✅ 同じPMXを再インポートすると、ポストプロセスAnimBPのIK / 物理グラフを再構築し、重複ノードを削除。
- ✅ Sequencer上部の `M2U > Export Camera VMD...` から、現在開いているLevel Sequenceを標準Camera VMDとして書き出し。
- 🟡 IK Retargeterのターゲットチェーン対応は、プロジェクトのキャラクターごとに手動修正が必要になる場合があります。

## ドキュメント

- **[日本語ユーザーマニュアル](Docs/UserManual/日本語/README.md)** — インストール、モデル・モーション・カメラのインポート、Control Rigによるモーション作成と編集、VMD書き出しを順番に説明します。
- [中文用户手册](Docs/UserManual/README.md)
- [English User Manual](Docs/UserManual/English/README.md)

## 既知の制限

- SDEF / QDEFは現在線形ウェイトによる近似のため、一部モデルではMMD本来の結果と変形が異なる場合があります。
- Vertex Morph、および再帰的にVertex Morphへ展開できるGroup / Flip MorphはUnreal Morph Targetを生成します。Bone / UV / Material / Impulse Morphは対応するUE機能へまだ変換されません。
- 特殊なボーン、複雑なIK、非標準構造を持つモデルでは、互換性対応や結果の調整が必要になる場合があります。

## 謝辞

MMDデータ形式とUnrealでの実装に関する多くの細部は、次のオープンソースプロジェクトの公開成果から学びました：

- **[MMD Tools](https://github.com/MMD-Blender/blender_mmd_tools)**
- **[OpenMMD](https://github.com/dskjal/OpenMMD)**
- **[VRM4U](https://github.com/ruyo/VRM4U)**
- **[AMAP5](https://github.com/hatoghx/AMAP5)**

MMD2Unrealは独立して実装されたプロジェクトであり、上記プロジェクトの移植、Fork、複製ではありません。
