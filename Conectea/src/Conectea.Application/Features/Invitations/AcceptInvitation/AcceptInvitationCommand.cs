using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conectea.Application.Features.Invitations.AcceptInvitation
{
    public class AcceptInvitationCommand
    {
        public string Code { get; set; } = string.Empty;
    }
}
