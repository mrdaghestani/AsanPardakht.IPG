FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY . .

ARG VERSION=1.0.0
ARG APIKEY=APIKEY
RUN dotnet restore && dotnet build -c Release \
    && dotnet pack -c Release -p:PackageVersion=${VERSION} -o /app/build AsanPardakht.IPG \
    && dotnet nuget push -k $APIKEY -s https://www.nuget.org/api/v2/package /app/build/AsanPardakht.IPG.${VERSION}.nupkg \
    && dotnet pack -c Release -p:PackageVersion=${VERSION} -o /app/build AsanPardakht.IPG.AspNetCore \
    && dotnet nuget push -k $APIKEY -s https://www.nuget.org/api/v2/package /app/build/AsanPardakht.IPG.AspNetCore.${VERSION}.nupkg

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

#--------------------------------
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS final
ARG VERSION=1.0
ENV AppConfigs__Version=${VERSION}
ENV AppConfigs__IsDevelopment=false
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "ApIpgSample.dll"]
