import { UserEntity } from './generic-entities/user.entity';
import { Entity } from 'typeorm';

@Entity('vet')
export class VetEntity extends UserEntity {}
