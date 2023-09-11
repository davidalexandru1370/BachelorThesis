import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
    id("org.springframework.boot") version "3.1.2"
    id("io.spring.dependency-management") version "1.1.2"
    id("org.liquibase.gradle") version "2.2.0"
    kotlin("jvm") version "1.8.22"
    kotlin("plugin.spring") version "1.8.22"
    kotlin("plugin.jpa") version "1.8.22"
}

group = "project"
version = "0.0.1-SNAPSHOT"

java {
    sourceCompatibility = JavaVersion.VERSION_17
}

repositories {
    mavenCentral()
}

configurations {
    liquibase {
        activities.register("main")
        runList = "main"
    }

    liquibaseRuntime {
        extendsFrom(project.configurations.compileClasspath.get())
    }
}

dependencies {
    implementation("org.springframework.boot:spring-boot-starter-data-jpa")
    implementation("org.springframework.boot:spring-boot-starter-data-redis")
    implementation("org.springframework.boot:spring-boot-starter-web:3.1.0")
    implementation("org.springframework.boot:spring-boot-starter-validation")
    implementation("jakarta.validation:jakarta.validation-api:3.0.2")
    implementation("org.springframework.boot:spring-boot-starter-jooq:3.1.3")
    implementation("org.springframework.boot:spring-boot-starter-security")
    implementation("org.jetbrains.kotlin:kotlin-reflect")
    implementation("org.modelmapper:modelmapper:3.1.1")
    implementation("com.h2database:h2:2.1.214")
    implementation("org.jooq:jooq:3.18.6")
    runtimeOnly("org.postgresql:postgresql")
    annotationProcessor("org.springframework.boot:spring-boot-configuration-processor")
    testImplementation("org.springframework.boot:spring-boot-starter-test")
    implementation("io.jsonwebtoken:jjwt-api:0.11.5")
    implementation("io.jsonwebtoken:jjwt-impl:0.11.5")
    implementation("io.jsonwebtoken:jjwt-jackson:0.11.5")
    implementation("org.liquibase.ext:liquibase-hibernate6:4.22.0")
    liquibaseRuntime("org.liquibase:liquibase-core:4.22.0")
    liquibaseRuntime("org.liquibase:liquibase-groovy-dsl:3.0.3")
    liquibaseRuntime("org.postgresql:postgresql")
    liquibaseRuntime("org.liquibase.ext:liquibase-hibernate6:4.22.0")
    liquibaseRuntime("info.picocli:picocli:4.7.4")
    liquibaseRuntime(sourceSets.getByName("main").compileClasspath)
    liquibaseRuntime(sourceSets.getByName("main").runtimeClasspath)
    liquibaseRuntime(sourceSets.getByName("main").output)
}

tasks.withType<KotlinCompile> {
    kotlinOptions {
        freeCompilerArgs += "-Xjsr305=strict"
        jvmTarget = "17"
    }
}

tasks.withType<Test> {
    useJUnitPlatform()
}
