FROM amazoncorretto:17-alpine-jdk

WORKDIR /app

COPY . .

EXPOSE 8080

RUN ./gradlew build -x test

CMD ["java", "-jar", "./build/libs/backend-0.0.1-SNAPSHOT.jar"]