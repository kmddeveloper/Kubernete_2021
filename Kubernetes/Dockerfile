#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Kubernetes/Web.csproj", "Kubernetes/"]
COPY ["TransferObjects/TransferObjects.csproj", "TransferObjects/"]
COPY ["GlobalConstants/GlobalConstants.csproj", "GlobalConstants/"]
COPY ["DataReaderMapper/DataReaderMapper.csproj", "DataReaderMapper/"]
COPY ["AppConfiguration/AppConfiguration.csproj", "AppConfiguration/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["EFData/EFData.csproj", "EFData/"]
COPY ["EFModel/EFModel.csproj", "EFModel/"]
COPY ["Extensions/Extensions.csproj", "Extensions/"]
COPY ["Repository/Repository.csproj", "Repository/"]
RUN dotnet restore "Kubernetes/Web.csproj"
COPY . .
WORKDIR "/src/Kubernetes"
RUN dotnet build "Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]