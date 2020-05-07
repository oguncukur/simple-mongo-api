FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine as build-ENV
COPY /BooksApi ./app/BooksApi
WORKDIR /app/BooksApi
RUN dotnet publish -c Release -r linux-musl-x64 -o publish-folder

FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.1-alpine as runtime
COPY --from=build-env /app/BooksApi/publish-folder ./
RUN apk add --update \
    iputils \
    curl \
    icu-libs \
    && rm -rf /var/cache/apk/*
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_ENVIRONMENT=Kubernetes
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["./BooksApi"]