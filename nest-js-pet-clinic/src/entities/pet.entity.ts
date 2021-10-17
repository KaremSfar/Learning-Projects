import { Entity, ManyToOne } from 'typeorm';
import { NamedEntity } from './generic-entities/named.entity';
import { OwnerEntity } from './owner.entity';

@Entity('pet')
export class PetEntity extends NamedEntity {
  @ManyToOne((type) => OwnerEntity, {
    nullable: false,
    createForeignKeyConstraints: true,
  })
  owner: OwnerEntity;
}
