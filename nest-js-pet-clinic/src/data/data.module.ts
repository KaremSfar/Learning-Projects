import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { OwnerRepository } from './repositories/owner.repo';
import { PetRepository } from './repositories/pet.repo';
import { VetRepository } from './repositories/vet.repo';

@Module({
  imports: [
    TypeOrmModule.forFeature([OwnerRepository, VetRepository, PetRepository]),
  ],
  providers: [],
  exports: [TypeOrmModule],
})
export class DataModule {}
