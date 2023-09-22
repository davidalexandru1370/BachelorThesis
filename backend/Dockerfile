FROM node:20

WORKDIR /app 

COPY package*.json ./

RUN npm install

COPY . .

EXPOSE 3000

COPY ./entrypoint.sh /entrypoint.sh

RUN chmod +x /entrypoint.sh

CMD [ "npm", "run", "start" ]

ENTRYPOINT [ "/entrypoint.sh" ]
