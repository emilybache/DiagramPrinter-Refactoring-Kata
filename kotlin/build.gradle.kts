plugins {
    kotlin("jvm") version "2.2.0"
}

group = "org.sammancoaching"
version = "1.0-SNAPSHOT"

repositories {
    mavenCentral()
}

dependencies {
    testImplementation(kotlin("test"))
    testImplementation("com.approvaltests", "approvaltests", "24.2.0")
}

tasks.test {
    useJUnitPlatform()
}
kotlin {
    jvmToolchain(22)
}