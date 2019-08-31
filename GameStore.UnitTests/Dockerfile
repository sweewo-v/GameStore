FROM microsoft/dotnet:2.2-sdk
WORKDIR /app

COPY . ./
RUN dotnet build -c Release
ENTRYPOINT ["dotnet", "test", "--logger:trx"]