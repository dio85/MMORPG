//
// Auto Generated Code By excel2json
// https://neil3d.gitee.io/coding/excel2json.html
// 1. 每个 Sheet 形成一个 Struct 定义, Sheet 的名称作为 Struct 的名称
// 2. 表格约定：第一行是变量名称，第二行是变量类型

// Generate From DialogueDefine.xlsx

public class DialogueDefine
{
	public int ID; // ID
	public string Decs; // 介绍
	public string Content; // 内容
	public string Options; // 选项id列表
	public int Jump; // 跳转
	public string AcceptTask; // 接取任务id，接取失败时跳转的对话id
	public string SubmitTask; // 提交任务id，提交失败时跳转的对话id
	public int SaveDialogueId; // 在npc说话时使用
	public string TipResource; // 头顶提示，疑问，感叹，星号
}


// End of Auto Generated Code
