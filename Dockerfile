FROM python:3.6-alpine

WORKDIR /app

ENV BUILD_LIST git

COPY blockchain.py Pipfile /app/

RUN apk add --update $BUILD_LIST \
    && pip install pipenv \
    && pipenv --python=python3.6 \
    && pipenv install \
    && apk del $BUILD_LIST \
    && rm -rf /var/cache/apk/*

EXPOSE 5000

ENTRYPOINT [ "pipenv", "run", "python", "/app/blockchain.py", "--port", "5000"  ]
