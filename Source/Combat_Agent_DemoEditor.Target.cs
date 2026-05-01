// 文件功能：定义编辑器目标的构建入口。
// 实现思路：继承 TargetRules，指定编辑器目标类型和项目主模块，供 UE 编辑器加载项目代码。

using UnrealBuildTool;
using System.Collections.Generic;

// 编辑器目标配置类：用于生成可在 UE 编辑器中加载的项目目标。
public class Combat_Agent_DemoEditorTarget : TargetRules
{
	// 构造函数：接收 UnrealBuildTool 传入的目标信息并写入编辑器构建规则。
	public Combat_Agent_DemoEditorTarget( TargetInfo Target) : base(Target)
	{
		// 目标类型：设置为 Editor，表示该目标用于编辑器环境。
		Type = TargetType.Editor;
		// 默认构建设置：使用 UE5 第五版构建规则。
		DefaultBuildSettings = BuildSettingsVersion.V5;
		// 头文件包含顺序：固定为 UE 5.4 规则，避免版本升级造成包含顺序差异。
		IncludeOrderVersion = EngineIncludeOrderVersion.Unreal5_4;
		// 额外模块列表：加入本项目主运行时模块，供编辑器编译和加载。
		ExtraModuleNames.Add("Combat_Agent_Demo");
	}
}
