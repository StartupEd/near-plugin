FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "UXs/Incubation/UX.Incubation.csproj"

WORKDIR "/src/UXs/Incubation"
RUN dotnet build "UX.Incubation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UX.Incubation.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY --from=build /src/nginx.conf /etc/nginx/nginx.conf