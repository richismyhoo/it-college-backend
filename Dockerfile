FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /source

COPY ItCollege.csproj . 
RUN dotnet restore ItCollege.csproj

COPY . .
RUN dotnet publish ItCollege.csproj -c Release -o /app --runtime linux-musl-x64 --self-contained false 

FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-chiseled AS runtime
WORKDIR /app

COPY --from=build /app ./

EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "ItCollege.dll"]
