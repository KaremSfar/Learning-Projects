import { PassportStrategy } from '@nestjs/passport';
import { ExtractJwt, Strategy } from 'passport-jwt';
import { RepositoryFactory } from '../service/repo.factory';
import * as dotenv from 'dotenv';
import { Repository } from 'typeorm';
import { UserEntity } from '../../../entities/generic-entities/user.entity';
import { Injectable } from '@nestjs/common';

// For getting the Token Secret Key
dotenv.config();

// The Strategy class gets called from a Guard
@Injectable()
export class PassportJwtStrategy extends PassportStrategy(Strategy) {
  constructor(private repoFactory: RepositoryFactory) {
    super({
      //For extracting the JWToken from the Header as a bearer Token
      jwtFromRequest: ExtractJwt.fromAuthHeaderAsBearerToken(),
      secretOrKey: process.env.SECRET,
    });
  }

  // Called after extracting the bearer token to try and inject the payload to request
  async validate(payload) {
    const repository: Repository<UserEntity> = this.repoFactory.getRepository(
      payload.role,
    );

    const user = await repository.findOne({
      id: payload.id,
    });

    if (user) {
      delete user.password;
      user.role = payload.role;
      return user; // this gets injected in request !
    }
  }
}
