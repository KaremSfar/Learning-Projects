import { PassportJwtStrategy } from './passport-jwt.strategy';

describe('PassportJwtStrategy', () => {
  it('should be defined', () => {
    expect(new PassportJwtStrategy()).toBeDefined();
  });
});
