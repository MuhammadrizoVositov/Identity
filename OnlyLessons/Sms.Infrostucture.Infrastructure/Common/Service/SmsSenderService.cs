using Sms.Infrustructure.Application.Common.Notification.Broker;

namespace Sms.Infrostucture.Infrastructure.Common.Service
{
    public class SmsSenderService : ISmsSenderBroker
    {
        private readonly IEnumerable<ISmsSenderBroker> _smsSenderBrokers;

        public SmsSenderService(IEnumerable<ISmsSenderBroker> smsSenderBrokers)
        {
            _smsSenderBrokers = smsSenderBrokers;
        }

        public async ValueTask<bool> SendAsync(string senderPhoneNumber


            , string recivePhoneNumber,
            string massage,
            CancellationToken cancellationToken)
        {
            var result = false;
            foreach(var smsSender in _smsSenderBrokers)
            {
                try
                {
                    result = await smsSender.SendAsync(senderPhoneNumber, recivePhoneNumber, massage, cancellationToken);
                    if (result) return result;
                }
                catch (Exception ex)
                {

                }
            }
                
            return result;
            
        }
    }
}
