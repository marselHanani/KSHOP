# ===============================
# STEP 1: Build the application
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY KSHOP.PL/*.csproj ./KSHOP.PL/
RUN dotnet restore KSHOP.PL/KSHOP.PL.csproj

# Copy everything and build
COPY . .
RUN dotnet publish KSHOP.PL/KSHOP.PL.csproj -c Release -o /app/publish

# ===============================
# STEP 2: Run the application
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Tell Render what port to expose
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "KSHOP.PL.dll"]
