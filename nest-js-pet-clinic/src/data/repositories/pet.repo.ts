import { EntityRepository, Repository } from 'typeorm';
import { PetEntity } from '../../entities/pet.entity';

@EntityRepository(PetEntity)
export class PetRepository extends Repository<PetEntity> {}
