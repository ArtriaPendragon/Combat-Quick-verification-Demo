// 文件功能：定义游戏运行目标的构建入口。
// 实现思路：继承 TargetRules，指定目标类型、构建设置、头文件包含顺序和需要加载的主模块。

using UnrealBuildTool;
using System.Collections.Generic;

// 游戏目标配置类：用于生成独立游戏或编辑器外运行时目标。
public class Combat_Agent_DemoTarget : TargetRules
{
	// 构造函数：接收 UnrealBuildTool 传入的目标信息并写入项目构建规则。
	public Combat_Agent_DemoTarget(TargetInfo Target) : base(Target)
	{
		// 目标类型：设置为 Game，表示这是运行时游戏目标。
		Type = TargetType.Game;
		// 默认构建设置：使用 UE5 第五版构建规则。
		DefaultBuildSettings = BuildSettingsVersion.V5;
		// 头文件包含顺序：固定为 UE 5.4 规则，避免版本升级造成包含顺序差异。
		IncludeOrderVersion = EngineIncludeOrderVersion.Unreal5_4;
		// 额外模块列表：加入本项目主运行时模块。
		ExtraModuleNames.Add("Combat_Agent_Demo");
	}
}
