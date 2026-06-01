# 变更记录

## CHANGE-001 至 CHANGE-007 历史摘要

- 日期：2026-05-01
- 内容：建立玩家输入缓冲、玩家索敌、增强输入绑定和中文注释规则的初始工程骨架。
- 当前定位：这些内容保留为历史基础，不再作为当前怪物 AI Agent 的主标准。
- 验证结果：历史 C++ 骨架曾通过 UE 5.4 编译；运行时输入资产和编辑器播放测试仍未完成。

## CHANGE-008

- 日期：2026-05-10
- 模块：怪物 AI 文档、源码、构建配置、调试日志。
- 变更类型：新增和重构。
- 变更内容：
  - 新增怪物 AI 架构、数据模型、行为树、黑板、调试和上下文记忆文档。
  - 新增 `MonsterAITypes.h`。
  - 新增 `UMonsterAIDataAsset`。
  - 新增 `UMonsterAIComponent`、`UMonsterCombatComponent`、`UMonsterMoveComponent`、`UMonsterPartComponent`。
  - 新增 `AMonsterCharacter`。
  - 新增 `LogMonsterAI`、`LogMonsterCombat`、`LogMonsterPart` 日志分类。
  - 在构建规则中加入 `AIModule`、`GameplayTags` 和 `NavigationSystem` 依赖。
- 变更原因：用户提供新的《怪物猎人：世界》式战斗 AI Agent 文档，要求完全以新文档为标准。
- 影响范围：怪物 AI 数据层、状态层、战斗评分层、移动意图层、部位破坏层、调试层和文档工作流。
- 验证方式：使用 UE 5.4 构建 `Combat_Agent_DemoEditor Win64 Development`。
- 验证结果：通过。
- 风险说明：尚未创建行为树、黑板、EQS 和数据资产实例，当前验证仍停留在 C++ 编译层。

## CHANGE-009

- 日期：2026-05-10
- 模块：源码目录、Agent 文档目录、构建配置。
- 变更类型：重构。
- 变更内容：
  - 建立 `Core`、`Debug`、`Player`、`MonsterAI` 源码目录。
  - 建立 `Architecture`、`MonsterAI`、`Systems`、`Memory`、`QA`、`Status` 文档目录。
  - 将现有源码和文档移动到对应目录。
  - 将项目内 include 改为模块根路径，例如 `MonsterAI/Types/MonsterAITypes.h`。
  - 在 `Combat_Agent_Demo.Build.cs` 中加入模块根 include 路径。
- 变更原因：用户要求按标准游戏开发目录分类建立文件夹，并把现有文件归类。
- 影响范围：所有项目自有源码文件和 Agent 文档文件。
- 验证方式：构建 `Combat_Agent_DemoEditor Win64 Development`。
- 验证结果：通过。
- 风险说明：移动文件后 IDE 缓存可能需要刷新；UE 编译已验证路径可用。

## CHANGE-010

- 日期：2026-05-10
- 模块：怪物 AI 行为树桥接层。
- 变更类型：新增。
- 变更内容：
  - 新增 `Source/Combat_Agent_Demo/MonsterAI/BehaviorTree` 目录。
  - 新增 `UBTTask_MonsterSelectAttackMove`，用于行为树调用招式评分并写回黑板。
  - 新增 `UBTTask_MonsterBuildMoveIntent`，用于按评分失败原因生成移动回退意图。
  - 新增 `UBTTask_MonsterUpdateDebugSnapshot`，用于把黑板摘要、EQS 摘要和评分结果写入调试快照。
- 变更原因：让 C++ 怪物 AI 组件能被编辑器行为树资产直接使用。
- 影响范围：怪物 AI 行为树、黑板接线、招式评分调试、移动失败回退。
- 验证方式：构建 `Combat_Agent_DemoEditor Win64 Development`。
- 验证结果：通过。
- 风险说明：尚未在编辑器行为树资产中运行测试，黑板键仍需人工按文档接线。

## CHANGE-011

- 日期：2026-05-10
- 模块：怪物 AI 控制器、行为树服务、怪物角色默认 AI 设置。
- 变更类型：新增和修改。
- 变更内容：
  - 新增 `AMonsterAIController`，用于占有怪物后自动运行默认行为树。
  - 新增 `UBTService_MonsterUpdateCombatBlackboard`，用于刷新当前目标、目标距离、目标角度、参与玩家数量和状态布尔值。
  - 修改 `AMonsterCharacter`，默认使用 `AMonsterAIController`，并设置场景放置或生成时自动占有 AI。
- 变更原因：为编辑器行为树资产提供运行入口和持续黑板输入。
- 影响范围：怪物 AI 行为树运行、黑板刷新、怪物蓝图默认 AI 设置。
- 验证方式：构建 `Combat_Agent_DemoEditor Win64 Development`。
- 验证结果：通过。
- 风险说明：尚未在编辑器中配置真实行为树资产，运行时行为仍需播放测试。

## CHANGE-012

- 日期：2026-05-10
- 模块：怪物 AI 黑板键契约、行为树任务默认配置。
- 变更类型：新增和修改。
- 变更内容：
  - 新增 `MonsterBlackboardKeys.h`，集中定义怪物 AI 黑板键名常量。
  - 为招式选择任务、移动意图任务、调试快照任务和战斗黑板刷新服务设置默认黑板键。
  - 修正黑板文档中 `BB_CurrentMoveTag` 的类型为字符串。
- 变更原因：降低编辑器黑板和行为树资产手动接线风险。
- 影响范围：行为树任务默认配置、黑板资产创建规范、运行时调试字段。
- 验证方式：构建 `Combat_Agent_DemoEditor Win64 Development`。
- 验证结果：通过。
- 风险说明：黑板资产仍需在编辑器中创建同名键，否则默认键名无法找到对应键。
