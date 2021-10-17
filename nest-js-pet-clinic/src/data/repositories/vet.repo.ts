import { EntityRepository, Repository } from 'typeorm';
import { VetEntity } from '../../entities/vet.entity';

@EntityRepository(VetEntity)
export class VetRepository extends Repository<VetEntity> {}
