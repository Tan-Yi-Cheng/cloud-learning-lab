using System;
using System.IO;         // 新增：用于处理文件
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("🚀 正在从Github获取今日代码金句...");
        
        // 1. 网络请求部分
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "CSharp-Console-App"); // 规范：访问GitHub API建议加上User-Agent
        var response = await client.GetStringAsync("https://api.github.com/zen");
        
        Console.WriteLine("\n--- 今日代码金句 ---");
        Console.WriteLine(response);
        Console.WriteLine("--------------------\n");

        // 2. 持久化存储部分 (将金句存入 Docker 挂载的卷中)
        string folderPath = "data"; // 对应容器内的路径
        string filePath = Path.Combine(folderPath, "inspiration.txt");

        // 获取绝对路径（这能让你在日志里看到它在容器内的真实身份）
        string absolutePath = Path.GetFullPath(filePath);

        try {
            // 确保文件夹存在
            if (!Directory.Exists(folderPath)) {
                Directory.CreateDirectory(folderPath);
            }

            // 将金句和时间追加到文件末尾
            string logEntry = $"[{DateTime.Now}] {response}";
            await File.AppendAllTextAsync(filePath, logEntry + Environment.NewLine);
            
            Console.WriteLine($"💾 代码金句已同步到本地文件: {filePath}");

            Console.WriteLine($"💾 数据已持久化！");
            Console.WriteLine($"📍 容器内绝对路径: {absolutePath}"); 
            Console.WriteLine($"🏠 对应宿主机位置: RootFolder/data/inspiration.txt (由 Docker Compose 映射)");
        }
        catch (Exception ex) {
            Console.WriteLine($"❌ 存储失败: {ex.Message}");
        }

        Console.WriteLine("\n任务完成！按回车键退出...");
    }
}