using UnrealBuildTool;

public class MMD2UnrealCore : ModuleRules
{
    public MMD2UnrealCore(ReadOnlyTargetRules Target) : base(Target)
    {
        PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;
        CppStandard = CppStandardVersion.Cpp20;

        PublicDependencyModuleNames.AddRange(new[]
        {
            "Core",
            "CoreUObject",
            "Engine",
            "CinematicCamera",
            "AnimationCore",
            "AnimGraphRuntime",
            "IKRig",
            "MovieScene",
            "MovieSceneTracks"
        });
    }
}
