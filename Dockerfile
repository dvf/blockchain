FROM python:3.6-alpine

WORKDIR /app

ENV BUILD_LIST git

RUN apk add --update $BUILD_LIST \
    && git clone https://github.com/dvf/blockchain.git /app \
    && pip install pipenv \
    && pipenv --python=python3.6 \
    && pipenv install \
    && apk del $BUILD_LIST \
    && rm -rf /var/cache/apk/*

EXPOSE 5000

ENTRYPOINT [ "pipenv", "run", "python", "/app/blockchain.py", "--port", "5000"  ]
