import { Injectable, NotFoundException } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { VetEntity } from 'src/entities/vet.entity';
import { VetRepository } from 'src/data/repositories/vet.repo';
import { UpdateResult } from 'typeorm';
import { UpdateVetDto } from '../dto/vet.dto';

@Injectable()
export class VetService {
  constructor(
    @InjectRepository(VetRepository) private vetRepository: VetRepository,
  ) {}

  getVets(): Promise<VetEntity[]> {
    return this.vetRepository.find();
  }

  getVet(id: string): Promise<VetEntity> {
    return this.vetRepository.findOne({ id: id });
  }

  async update(id: string, updateVetDto: UpdateVetDto): Promise<VetEntity> {
    const vet = await this.vetRepository.preload({
      id: id,
      ...updateVetDto,
    });
    if (vet === undefined) {
      new NotFoundException(`The Vet doesnt Exist`);
    }
    return this.vetRepository.save(vet);
  }

  delete(id: string): Promise<UpdateResult> {
    return this.vetRepository.softDelete(id);
  }

  restore(id: string) {
    return this.vetRepository.restore(id);
  }
}
