using Fogo.Configuration;

using Microsoft.Extensions.Options;

using System.Threading.Tasks;

using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Fogo.Services.Implementations {

    public class TwilioSender : ITwilioSender {
        private readonly TwilioSettings _settings;

        public TwilioSender(IOptions<TwilioSettings> settings) {
            _settings = settings.Value;
            Initialize();
        }

        private void Initialize() {
            TwilioClient.Init(_settings.AccountSid, _settings.AuthToken);
        }

        // TODO: Need to check
        public Task SendAsync(string phoneNumber, string body) {
            //ValidationRequestResource.Create(new PhoneNumber(phoneNumber));
            return MessageResource.CreateAsync(new CreateMessageOptions(new PhoneNumber(phoneNumber)) {
                From = new PhoneNumber(_settings.From),
                Body = body
            });
        }
    }
}