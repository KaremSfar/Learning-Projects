import { Body, Controller, Get, Post, UseGuards } from '@nestjs/common';
import { UserEntity } from 'src/entities/generic-entities/user.entity';
import { UserLoginDto, UserSubscribeDto } from '../dtos/user.dto';
import { AuthService } from '../service/auth.service';
import { AuthGuard } from '@nestjs/passport';
import { ApiTags } from '@nestjs/swagger';

@ApiTags('Authentication')
@Controller('auth')
export class AuthController {
  constructor(private authService: AuthService) {}

  // registering to the app route
  @Post('register')
  register(
    @Body() userSubscribeDto: UserSubscribeDto,
  ): Promise<Partial<UserEntity>> {
    return this.authService.register(userSubscribeDto);
  }

  // loggin in route
  @Post('login')
  login(@Body() userLoginDto: UserLoginDto): Promise<any> {
    return this.authService.login(userLoginDto);
  }

  // test route for testing UseGuards; TODO Delete
  @Get('test')
  @UseGuards(AuthGuard('jwt'))
  test() {
    return 'alou';
  }
}
