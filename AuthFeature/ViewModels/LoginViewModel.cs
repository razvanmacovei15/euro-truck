using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EuroTruck.AuthFeature.Helpers;
using EuroTruck.AuthFeature.Model;
using EuroTruck.AuthFeature.Services;

namespace EuroTruck.AuthFeature.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthService authService = new AuthService();
        public event PropertyChangedEventHandler PropertyChanged;

        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(async _ => await Login());
        }

        public async Task<bool> Login()
        {
            var user = new User { Username = Username, Password = Password };
            return await authService.LoginAsync(user);
            // Add navigation or error display here
        }
    }
}
