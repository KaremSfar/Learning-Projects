import { Module } from '@nestjs/common';
import { TypeOrmModule, TypeOrmModuleOptions } from '@nestjs/typeorm';
import { AppController } from './app.controller';
import * as dotenv from 'dotenv';
import { AuthModule } from './auth/auth-module/auth.module';
import { DataModule } from './data/data.module';
import { ConfigModule } from '@nestjs/config';
import { RestApiGatewayModule } from './rest-api-gateway/rest-api-gateway.module';
import { GuardsModule } from './auth/guards-module/guards.module';

//Config environment variables
dotenv.config();

//Typeorm Options
const typeOrmOpts: TypeOrmModuleOptions = {
  type: 'mysql',
  host: process.env.DB_HOST,
  port: parseInt(process.env.DB_PORT),
  username: process.env.DB_USER,
  password: process.env.DB_PASSWORD,
  database: process.env.DB_NAME,
  entities: ['dist/**/*.entity{.ts,.js}'],
  synchronize: true,
};

@Module({
  imports: [
    AuthModule,
    ConfigModule.forRoot({
      isGlobal: true,
    }),
    DataModule,
    TypeOrmModule.forRoot(typeOrmOpts),
    RestApiGatewayModule,
    GuardsModule,
  ],
  controllers: [AppController],
  providers: [],
})
export class AppModule {}
