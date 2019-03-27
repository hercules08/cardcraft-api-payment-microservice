FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["../Cardcraft.Microservice.Payment/Cardcraft.Microservice.Payment.csproj", "../Cardcraft.Microservice.Payment/"]
RUN dotnet restore "../Cardcraft.Microservice.Payment/Cardcraft.Microservice.Payment.csproj"
COPY . .
WORKDIR "/src/../Cardcraft.Microservice.Payment"
RUN dotnet build "Cardcraft.Microservice.Payment.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Cardcraft.Microservice.Payment.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Cardcraft.Microservice.Payment.dll"]