FROM maven:3.6.3-jdk-11 AS build
WORKDIR /app
COPY pom.xml .
RUN mvn verify clean --fail-never
COPY src src
RUN mvn package
RUN java -Djarmode=layertools -jar target/api.mortgage.jar extract

FROM openjdk:11
WORKDIR /app
COPY --from=build /app/dependencies/ ./
COPY --from=build /app/snapshot-dependencies/ ./
COPY --from=build /app/spring-boot-loader/ ./
COPY --from=build /app/application/ ./
EXPOSE 5002
ENTRYPOINT ["java", "org.springframework.boot.loader.JarLauncher"]