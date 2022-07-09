using System.Threading.Tasks;

namespace ControleDeCaixa.Core.Compartilhado
{
    public interface ICommand<T>
    {
        T Payload { get; }
        Task ExecutarAsync();
    }
}
