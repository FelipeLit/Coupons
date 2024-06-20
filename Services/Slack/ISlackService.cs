using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coupons.Services.Slack
{
    public interface ISlackService
    {
        Task SlackNotifier (string SlackMessage);
    }
}