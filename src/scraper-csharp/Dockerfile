# 1. 编译阶段 (给它起个别名叫 build)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# --- 优化点在这里 ---
# 先只复制项目文件（清单）
COPY LeetCodeHelper.csproj .
# 先下载依赖（只要清单不变，这层就会被缓存，不用每次都等进度条）
RUN dotnet restore
# ------------------

# 2. 复制并还原/ 现在再复制剩下的所有源码
COPY . .

# 3. 编译发布到 out 文件夹
RUN dotnet publish -c Release -o out

# 4. 运行阶段 (这是最终生成的镜像)
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app

# 5. 关键点：从名为 build 的阶段拷贝文件在/app/out 因为#1创建/app加上#3把编译好的文件发布在/app/out
COPY --from=build /app/out .

# 6. 启动程序
ENTRYPOINT ["dotnet", "LeetCodeHelper.dll"]