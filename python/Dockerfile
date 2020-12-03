FROM python:3.9-alpine

WORKDIR /app

# Install dependencies.
ADD requirements.txt /app
RUN cd /app && \
    pip install -r requirements.txt

# Add actual source code.
ADD blockchain.py /app
ADD api.py /app

EXPOSE 5000

CMD ["python", "api.py", "--port", "5000"]
