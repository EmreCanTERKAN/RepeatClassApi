using RepeatClassApi.Data;
using RepeatClassApi.Dtos;
using RepeatClassApi.Entities;
using RepeatClassApi.Services;
using RepeatClassApi.Types;

namespace RepeatClassApi.Managers
{
    public class UserManager : IUserService
    {
        private readonly RepeatClassDbContext _context;

        public UserManager(RepeatClassDbContext context)
        {
            _context = context;
        }


        public async Task<ServiceMessage> AddUser(AddUserDto userDto)
        {

            var userEntity = _context.Users.Where(x => x.Email.ToLower() == userDto.Email).FirstOrDefault();

            if (userEntity is null)
            {
                var user = new UserEntity
                {
                    Email = userDto.Email,
                    Password = userDto.Password

                };

                _context.Users.Add(user);

                await _context.SaveChangesAsync();


                return new ServiceMessage
                {
                    IsSucceed = true,
                    Message = "Kayıt Başarıyla Oluşturuldu"
                };

            }
            else
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Kullanıcı Email'i mevcut"
                };
            }


        }

        public async Task<ServiceMessage<UserInfoDto>> LoginUser(LoginUserDto userDto)
        {
            var userEntity = _context.Users.Where(e => e.Email.ToLower() == userDto.Email.ToLower()).FirstOrDefault();

            if (userEntity is null)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Kullanıcı Adı Veya Şifre Hatalı"

                };
            }

            if (userEntity.Password == userDto.Password)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = true,
                    Data = new UserInfoDto
                    {

                        Id = userEntity.Id,
                        Email = userEntity.Email,
                        UserType = userEntity.UserType
                    }
                };
            }
            else
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Kullanıcı Adı Veya Şifre Hatalı"
                };
            }


        }
    }
}
