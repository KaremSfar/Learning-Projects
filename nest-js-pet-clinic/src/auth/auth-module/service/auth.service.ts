import { ConflictException, Injectable, NotFoundException } from "@nestjs/common";
import { JwtService } from '@nestjs/jwt';
import { UserEntity } from 'src/entities/generic-entities/user.entity';
import { UserLoginDto, UserSubscribeDto } from '../dtos/user.dto';
import { RepositoryFactory } from './repo.factory';

//To encrypt the users' passwords
import * as bcrypt from 'bcrypt';
import { Repository } from 'typeorm';

@Injectable()
export class AuthService {
  constructor(
    private repoFactory: RepositoryFactory,
    private jwtService: JwtService,
  ) {}

  //Registers a user
  async register(registerDto: UserSubscribeDto): Promise<Partial<UserEntity>> {
    const repository: Repository<UserEntity> = this.repoFactory.getRepository(
      registerDto.role,
    );

    const user = repository.create({
      ...registerDto,
    });
    const salt = await bcrypt.genSalt();

    user.password = await bcrypt.hash(user.password, salt);

    try {
      await repository.save(user);
    } catch (e) {
      throw new ConflictException('username or email already existing');
    }
    delete user.password;
    delete user.createdAt;
    delete user.deletedAt;
    return user;
  }

  //For a regsitered user return his access token
  async login(credentials: UserLoginDto) {
    const repository: Repository<UserEntity> = this.repoFactory.getRepository(
      credentials.role,
    );

    const { email, password } = credentials;
    const user = await repository.findOne({ email });

    if (!user) throw new NotFoundException('email or password incorrect');

    if (await bcrypt.compare(password, user.password)) {
      const payload = {
        id: user.id,
        username: user.username,
        email: user.email,
        role: credentials.role,
      };
      const jwt = this.jwtService.sign(payload);
      return { 'access-token': jwt };
    }
    throw new NotFoundException('email or password incorrect');
  }
}
