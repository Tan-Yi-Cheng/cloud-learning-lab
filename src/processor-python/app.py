import os

# 路径指向我们在 Docker 运行时挂载的那个“共享保险箱”
data_path = "/app/data/inspiration.txt"

print("🐍 Python 助手正在读取 C# 留下的遗产...")

if os.path.exists(data_path):
    with open(data_path, "r", encoding="utf-8") as file:
        lines = file.readlines()
        if lines:
            last_quote = lines[-1].strip()
            print(f"✨ 发现最新金句: {last_quote}")
            
            # 顺便做个简单的算法处理：统计单词数（LeetCode 基础思维）
            word_count = len(last_quote.split())
            print(f"📊 这句话包含 {word_count} 个单词。")
else:
    print("📂 找不到数据文件，请检查 Docker 挂载路径是否为 /app/data")