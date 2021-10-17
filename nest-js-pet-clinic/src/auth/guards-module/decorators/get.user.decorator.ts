import { createParamDecorator, ExecutionContext } from '@nestjs/common';

//@User() user => gets the user from an authentified request
export const User = createParamDecorator(
  (data: unknown, ctx: ExecutionContext) => {
    const request = ctx.switchToHttp().getRequest();
    return request.user;
  },
);
