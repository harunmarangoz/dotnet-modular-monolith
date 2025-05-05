FROM mcr.microsoft.com/dotnet/sdk:9.0 as publish
ARG APP_PATH="Api/Api.csproj"

WORKDIR /
COPY . .
RUN dotnet publish $APP_PATH -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Api.dll"]