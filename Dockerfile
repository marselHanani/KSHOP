# ===============================
# STEP 1: Build the application
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# انسخ ملفات الحل بالكامل
COPY *.sln ./
COPY KSHOP.PL/*.csproj ./KSHOP.PL/
COPY KSHOP.DAL/*.csproj ./KSHOP.DAL/
COPY KSHOP.BLL/*.csproj ./KSHOP.BLL/

# استعادة الحزم
RUN dotnet restore

# انسخ كل الملفات
COPY . .

# بناء المشروع
RUN dotnet publish KSHOP.PL/KSHOP.PL.csproj -c Release -o /app/publish

# ===============================
# STEP 2: Runtime
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "KSHOP.PL.dll"]
