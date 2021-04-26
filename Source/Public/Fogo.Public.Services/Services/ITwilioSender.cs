using System.Threading.Tasks;

namespace Fogo.Services {

    public interface ITwilioSender {

        Task SendAsync(string phone, string body);
    }
}