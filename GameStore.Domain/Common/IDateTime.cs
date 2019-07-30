using System;

namespace GameStore.Domain.Common
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
