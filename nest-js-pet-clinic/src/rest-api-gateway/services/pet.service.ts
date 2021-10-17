import { InjectRepository } from '@nestjs/typeorm';
import { PetEntity } from 'src/entities/pet.entity';
import { OwnerEntity } from 'src/entities/owner.entity';
import { PetRepository } from 'src/data/repositories/pet.repo';
import { CreatePetDto, UpdatePetDto } from '../dto/pet.dto';
import { NotFoundException } from '@nestjs/common';
import { UpdateResult } from 'typeorm';

export class PetService {
  constructor(
    @InjectRepository(PetRepository) private petRepository: PetRepository,
  ) {}

  getByOwner(ownerEntity: OwnerEntity): Promise<PetEntity[]> {
    return this.petRepository.find({
      where: { owner: { id: ownerEntity.id } },
      select: ['id', 'name', 'owner'],
    });
  }

  createPet(
    ownerEntity: OwnerEntity,
    petDto: CreatePetDto,
  ): Promise<PetEntity> {
    const pet = this.petRepository.create({
      owner: ownerEntity,
      ...petDto,
    });
    return this.petRepository.save(pet);
  }

  async updatePet(
    id: string,
    updatePetDto: UpdatePetDto,
    owner: OwnerEntity,
  ): Promise<PetEntity> {
    // Maybe Use Update Method next;

    const pet = await this.petRepository.findOne({
      relations: ['owner'],
      // Check that the user Logged in is the owner of the Pet
      where: { id: id, owner: { id: owner.id } },
    });

    // Check the pet
    if (pet === undefined) {
      throw new NotFoundException(`Inexistant Pet`);
    }

    // change values
    const updated = {
      ...pet,
      ...updatePetDto,
    };

    return this.petRepository.save(updated);
  }

  delete(id: string, owner: OwnerEntity): Promise<UpdateResult> {
    return this.petRepository.softDelete({
      owner: { id: owner.id },
      id: id,
    });
  }

  async recover(id: string, owner: OwnerEntity): Promise<PetEntity> {
    const deletedPet = await this.petRepository.findOne({
      where: { id: id, owner: { id: owner.id } },
      withDeleted: true,
    });

    if (deletedPet === undefined) {
      throw new NotFoundException();
    }

    return this.petRepository.recover({
      owner: { id: owner.id },
      id: id,
    });
  }
}
