using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EuroTruck.AuthFeature.Helpers;
using EuroTruck.AuthFeature.Model;
using EuroTruck.AuthFeature.Services;

namespace EuroTruck.AuthFeature.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly AuthService authService = new AuthService();
        public event PropertyChangedEventHandler PropertyChanged;

        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(async _ => await Register());
        }

        private async Task Register()
        {
            if (Password != ConfirmPassword) return;
            var user = new User { Username = Username, Password = Password };
            await authService.RegisterAsync(user);
        }
    }
}
