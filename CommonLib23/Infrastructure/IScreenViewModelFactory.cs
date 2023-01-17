using Stylet;

namespace CommonLib23.Infrastructure
{
    public interface IScreenViewModelFactory
    {
        IScreen CreateScreenViewModel(string key);
    }
}
