FROM java:openjdk-8-jdk-alpine

# リポジトリをdockerコンテナ内にコピー
COPY . /kotlin-blockchain

# JVMのメトリクスを取れるようにjolokiaもいれてる
RUN apk update && \
    apk add --virtual build-dependencies build-base bash curl && \
    cd /kotlin-blockchain && ./gradlew clean && \
    cd /kotlin-blockchain && ./gradlew build && \
    mkdir -p /usr/local/kotlin-blockchain/lib && \
    cp -R /kotlin-blockchain/build/libs/* /usr/local/kotlin-blockchain/lib/ && \
    curl -o /usr/local/kotlin-blockchain/lib/jolokia-jvm-agent.jar https://repo1.maven.org/maven2/org/jolokia/jolokia-jvm/1.3.5/jolokia-jvm-1.3.5-agent.jar && \
    apk del build-dependencies && \
    rm -rf /var/cache/apk/* && \
    rm -rf ~/.gradle && \
    rm -rf /kotlin-blockchain

ENTRYPOINT java $JAVA_OPTS -javaagent:/usr/local/kotlin-blockchain/lib/jolokia-jvm-agent.jar=port=8778,host=0.0.0.0 -jar /usr/local/kotlin-blockchain/lib/kotlin-blockchain-1.0-SNAPSHOT.jar

EXPOSE 4567 8778