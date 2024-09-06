FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY *.sln .
#COPY */*.csproj ./
COPY WantToSell.Api/WantToSell.Api.csproj WantToSell.Api/
COPY WantToSell.Application/WantToSell.Application.csproj WantToSell.Application/
COPY WantToSell.Cache/WantToSell.Cache.csproj WantToSell.Cache/
COPY WantToSell.Domain/WantToSell.Domain.csproj WantToSell.Domain/
COPY WantToSell.Identity/WantToSell.Identity.csproj WantToSell.Identity/
COPY WantToSell.Logger/WantToSell.Logger.csproj WantToSell.Logger/
COPY WantToSell.Persistence/WantToSell.Persistence.csproj WantToSell.Persistence/
COPY WantToSell.Storage/WantToSell.Storage.csproj WantToSell.Storage/
#RUN dotnet restore

RUN dotnet restore WantToSell.Api/WantToSell.Api.csproj

COPY . .

RUN dotnet publish WantToSell.Api/WantToSell.Api.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 80

ENTRYPOINT ["dotnet", "WantToSell.Api.dll"]