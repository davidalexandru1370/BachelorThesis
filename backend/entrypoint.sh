#!/bin/bash
npm run typeorm migration:run -- -d ormconfig.ts
npm run start