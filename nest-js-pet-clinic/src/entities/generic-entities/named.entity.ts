import { Column } from 'typeorm';
import { BaseEntity } from './base.entity';

export abstract class NamedEntity extends BaseEntity {
  @Column({
    length: 50,
  })
  name: string;
}
