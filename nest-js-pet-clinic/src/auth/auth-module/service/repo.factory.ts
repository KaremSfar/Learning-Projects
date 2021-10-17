import { Injectable } from '@nestjs/common';
import { OwnerRepository } from 'src/data/repositories/owner.repo';
import { Role } from 'src/entities/util/role.enum';
import { Repository } from 'typeorm';
import { UserEntity } from '../../../entities/generic-entities/user.entity';
import { InjectRepository } from '@nestjs/typeorm';
import { VetRepository } from '../../../data/repositories/vet.repo';

// Repository Factory for authenticating
@Injectable()
export class RepositoryFactory {
  constructor(
    @InjectRepository(OwnerRepository) private ownerRepo: OwnerRepository,
    @InjectRepository(VetRepository) private vetRepo: VetRepository,
  ) {}

  public getRepository(role: Role): Repository<UserEntity> {
    if (role === Role.Owner) return this.ownerRepo;
    if (role === Role.Vet) return this.vetRepo;
  }
}
