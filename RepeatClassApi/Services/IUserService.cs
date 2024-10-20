using RepeatClassApi.Dtos;
using RepeatClassApi.Types;

namespace RepeatClassApi.Services
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto user);

        Task<ServiceMessage<UserInfoDto>> LoginUser(LoginUserDto user);
    }
}
