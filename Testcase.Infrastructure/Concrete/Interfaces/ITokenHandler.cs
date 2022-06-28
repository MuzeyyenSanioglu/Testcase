using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.User.Domain.Entites;

namespace Testcase.Infrastructure.Concrete.Interfaces
{
    public interface ITokenHandler
    {
        AccessToken CreateToken(TokenRequestModel user, List<OperationClaim> operationClaims = null);
    }
}
