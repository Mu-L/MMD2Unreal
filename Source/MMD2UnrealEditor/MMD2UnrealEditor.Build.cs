using UnrealBuildTool;

public class MMD2UnrealEditor : ModuleRules
{
    public MMD2UnrealEditor(ReadOnlyTargetRules Target) : base(Target)
    {
        PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;
        CppStandard = CppStandardVersion.Cpp20;

        PublicDependencyModuleNames.AddRange(new[]
        {
            "Core",
            "CoreUObject",
            "Engine",
            "MMD2UnrealCore",
            "AnimGraphRuntime"
        });

        PrivateDependencyModuleNames.AddRange(new[]
        {
            "UnrealEd",
            "AssetRegistry",
            "AssetTools",
            "AnimationCore",
            "MeshDescription",
            "MeshDescriptionOperations",
            "StaticMeshDescription",
            "SkeletalMeshDescription",
            "MovieScene",
            "MovieSceneTracks",
            "MovieSceneTools",
            "LevelSequence",
            "LevelSequenceEditor",
            "Sequencer",
            "SequencerScripting",
            "CinematicCamera",
            "ToolMenus",
            "ContentBrowser",
            "DesktopPlatform",
            "InputCore",
            "MaterialEditor",
            "IKRig",
            "IKRigEditor",
            "AnimGraph",
            "AnimGraphRuntime",
            "BlueprintGraph",
            "InterchangeCore",
            "Json",
            "Kismet",
            "PropertyEditor",
            "RenderCore",
            "RHI",
            "Slate",
            "SlateCore",
            "UMG",
            "Projects"
        });

        PrivateDependencyModuleNames.AddRange(new[]
        {
            "ControlRig",
            "ControlRigDeveloper",
            "ControlRigEditor",
            "RigVM",
            "RigVMDeveloper"
        });

        // KawaiiPhysics is an external required project plugin. The editor
        // graph and importer always build against its real v1.21 API.
        PrivateDependencyModuleNames.Add("KawaiiPhysics");
        PrivateDependencyModuleNames.Add("KawaiiPhysicsEd");
    }
}
