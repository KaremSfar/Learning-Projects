import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { ValidationPipe } from '@nestjs/common';

import * as dotenv from 'dotenv';
import { DocumentBuilder, SwaggerModule } from '@nestjs/swagger';
dotenv.config();

async function bootstrap() {
  const app = await NestFactory.create(AppModule);

  //DTO validation configuration
  app.useGlobalPipes(new ValidationPipe());

  //Swagger configuration
  const config = new DocumentBuilder()
    .setTitle('Gestion PFE Insat')
    .setDescription('API de gestion des pfes')
    .setVersion('1.0')
    .addSecurity('bearer', {
      in: 'header',
      type: 'http',
      scheme: 'bearer',
      bearerFormat: 'JWT',
    })
    .addSecurityRequirements('bearer', [])
    .build();
  const document = SwaggerModule.createDocument(app, config);
  SwaggerModule.setup('api', app, document);

  //App Start
  await app.listen(3000);
}
bootstrap();
