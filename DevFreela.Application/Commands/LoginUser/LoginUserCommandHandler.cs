using System.Threading;
using System.Threading.Tasks;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.CrossCutting.Auth.Interfaces;
using MediatR;

namespace DevFreela.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            //Gera o Hash da senha passada pelo command.
            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            //Busca na base de dados algum usuário que tenha o mesmo e-mail e senha hasheada passada.
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);
            
            //Se não existir, erro no login
            if(user is null) return null;

            //Se existir, gera token com os dados do usuário e retorna uma saída com os dados.
            var token = _authService.GenerateJwtToken(user.Email, user.Role);
            return  new LoginUserViewModel(user.Email, token);
        }
    }
}