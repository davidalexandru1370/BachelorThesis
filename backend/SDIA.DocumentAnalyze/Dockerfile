FROM python:3.9-bullseye

WORKDIR /app

COPY requirements.txt requirements.txt
RUN pip3 install -r requirements.txt

COPY . .

RUN apt-get update && apt-get install -y python3-opencv

CMD [ "python3", "-m", "flask", "run", "--port=5001", "--host=0.0.0.0"]