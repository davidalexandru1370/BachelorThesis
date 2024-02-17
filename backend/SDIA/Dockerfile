FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /backend
EXPOSE 80
EXPOSE 443
WORKDIR /src
COPY ["./Presentation/*.csproj", "./Presentation/"]
COPY ["./Application/*.csproj" , "./Application/"]
COPY ["./Domain/*.csproj", "./Domain/"]
COPY ["./Infrastructure/*.csproj", "./Infrastructure/"]
RUN dotnet restore "Presentation/Presentation.csproj"

COPY . .
WORKDIR "/src/Presentation"
RUN dotnet build "Presentation.csproj" -c Release -o /backend/build

RUN dotnet publish "Presentation.csproj" -c Release -o /backend/out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as final-env
WORKDIR /backend

COPY --from=build-env /backend/out .

ENTRYPOINT ["dotnet", "Presentation.dll"]
