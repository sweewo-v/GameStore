using System;
using GameStore.Domain.Common;

namespace GameStore.Application.Infrastructure
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
