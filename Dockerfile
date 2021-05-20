FROM python:3.6-alpine

WORKDIR /app

# Install dependencies.
ADD requirements.txt /app
RUN cd /app && \
    pip install -r requirements.txt

# Add actual source code.
ADD transaction.py /app
ADD blockchain.py /app
ADD server.py /app

EXPOSE 5000

CMD ["python", "server.py", "--port", "5000"]
