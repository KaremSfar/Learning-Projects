import { Entity, OneToMany } from 'typeorm';
import { PetEntity } from './pet.entity';
import { UserEntity } from './generic-entities/user.entity';

@Entity('owner')
export class OwnerEntity extends UserEntity {
  @OneToMany((type) => PetEntity, (pet) => pet.owner)
  pets: PetEntity[];
}
