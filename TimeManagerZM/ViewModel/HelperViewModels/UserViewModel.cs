using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagerZM.Data;
using TimeManagerZM.Model;

namespace TimeManagerZM.ViewModel
{
    public class UserViewModel
    {
        private UserRepository _userRepository;

        public UserViewModel()
        {
            _userRepository = new UserRepository();
        }

        // Метод для добавления нового пользователя
        public async Task AddNewUser(string userName, string password)
        {
            var newUser = new User
            {
                UserName = userName,
                Password = password
            };

            await _userRepository.AddUser(newUser);
        }

        // Метод для получения всех пользователей
        public async Task<List<User>> LoadAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        // Метод для получения пользователя по Id
        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        // Метод для обновления данных пользователя
        public async Task UpdateExistingUser(User user)
        {
            await _userRepository.UpdateUser(user);
        }

        // Метод для удаления пользователя
        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }

        public async Task<User> GetUserByNameAndPassword(string username, string password)
        {
           return await _userRepository.GetUserByNameAndPassword(username, password);
        }
    }
}
