using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderWebApi.Services.CountersServices.ClickCouterService
{
    public interface IClickCounter : ICounter<ConcurrentDictionary<string, int>>
    {

    }
}
