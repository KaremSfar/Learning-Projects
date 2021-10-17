import { Column } from 'typeorm';
import { Role } from '../util/role.enum';
import { BaseEntity } from './base.entity';

export abstract class UserEntity extends BaseEntity {
  @Column({
    length: 50,
    unique: true,
  })
  username: string;

  @Column({
    unique: true,
  })
  email: string;

  @Column({})
  password: string;

  @Column({ nullable: true })
  address: string;

  @Column({ nullable: true })
  telephone: string;

  role: Role;
}
