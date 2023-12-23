FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /app
EXPOSE 8080

# copy .csproj and restore as distinct layers
COPY "book-project.sln" "book-project.sln"
COPY "book-project/book-project.csproj" "book-project/book-project.csproj"

RUN dotnet restore "book-project.sln"

# copy everything else
COPY . .
WORKDIR /app
RUN dotnet publish -c Release -o out

# build a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "book-project.dll"]


