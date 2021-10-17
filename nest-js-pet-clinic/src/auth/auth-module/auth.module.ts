import { Module } from '@nestjs/common';
import { JwtModule } from '@nestjs/jwt';
import { PassportModule } from '@nestjs/passport';
import { AuthService } from './service/auth.service';
import { RepositoryFactory } from './service/repo.factory';
import { AuthController } from './controllers/auth.controller';
import { DataModule } from '../../data/data.module';
import { PassportJwtStrategy } from './strategies/passport-jwt.strategy';

@Module({
  imports: [
    //TypeormModule is imported from the Entities Module
    //Important Note, If the Repos were to also be exported from the Entities module, it would end in an error
    //I think it's because nest will try to inject the Repo by itself, not through the imported TypeOrm Module
    //While if all is exported/imported, and then typepeorm is redefined in here, it will be prioritized ?
    //TypeOrmModule.forFeature([OwnerRepository, VetRepository]),

    //Using Repos and Entities
    DataModule,
    //Passport JWT Strategy
    PassportModule.register({ defaultStrategy: 'jwt' }),
    //Signing and verifying Module
    JwtModule.register({
      secret: process.env.SECRET,
      signOptions: {
        expiresIn: 36000,
      },
    }),
  ],
  // Providers are the Services used from within the Module For the Module
  providers: [
    AuthService,
    RepositoryFactory,
    // Injected into the Authentication Service to Sign the payload
    PassportJwtStrategy,
  ],
  controllers: [AuthController],
  exports: [],
})

// Whenever Authentication is needed, use @UseGuards(AuthGuard('jwt'))
// Passport strategy used is JWT strategy
export class AuthModule {}
