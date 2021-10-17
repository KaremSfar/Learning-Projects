import { EntityRepository, Repository } from 'typeorm';
import { OwnerEntity } from '../../entities/owner.entity';

@EntityRepository(OwnerEntity)
export class OwnerRepository extends Repository<OwnerEntity> {}
