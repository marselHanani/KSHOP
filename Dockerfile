# ===============================
# STEP 1: Build the application
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# انسخ ملف الحل
COPY *.sln ./

# انسخ ملفات المشاريع فقط أولًا لاستعادة الحزم
COPY KASHOP.PL/*.csproj ./KASHOP.PL/
COPY KASHOP.BLL/*.csproj ./KASHOP.BLL/
COPY KASHOP.DAL/*.csproj ./KASHOP.DAL/

# استعادة الحزم لكل الحل
RUN dotnet restore

# انسخ كل الملفات الآن
COPY . .

# بناء المشروع الرئيسي ونشره
RUN dotnet publish KASHOP.PL/KASHOP.PL.csproj -c Release -o /app/publish

# ===============================
# STEP 2: Runtime
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# نسخ الملفات المنشورة
COPY --from=build /app/publish .

# إعدادات ASP.NET Core
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# تشغيل المشروع
ENTRYPOINT ["dotnet", "KASHOP.PL.dll"]
