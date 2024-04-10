using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CryptLearn.Shared.Infrastructure.Auth
{
    public class CryptLearnJwtBearerEvents : JwtBearerEvents
    {
        private readonly IJwtBearerEvents _events;

        public CryptLearnJwtBearerEvents(IJwtBearerEvents events)
        {
            _events = events;
        }

        public override Task AuthenticationFailed(AuthenticationFailedContext context)
            => _events.AuthenticationFailed(context);

        public override Task Challenge(JwtBearerChallengeContext context)
            => _events.Challenge(context);

        public override Task Forbidden(ForbiddenContext context)
            => _events.Forbidden(context);

        public override Task MessageReceived(MessageReceivedContext context)
            => _events.MessageReceived(context);

        public override Task TokenValidated(TokenValidatedContext context)
            => _events.TokenValidated(context);
    }
}