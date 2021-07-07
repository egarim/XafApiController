using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
namespace XafApiController.Blazor.Server.Services
{
    public interface IJwtService
    {


        string JwtPayloadToToken(JwtPayload Payload);
        JwtPayload TokenToJwtPayload(string StringToken);
        bool VerifyToken(string token);
        long DateToNumber(DateTime dateTime);
        DateTime NumberToDate(long date);
    }
}
