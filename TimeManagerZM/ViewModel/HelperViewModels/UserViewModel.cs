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
        public void AddNewUser(string userName, string password)
        {
            var newUser = new User
            {
                UserName = userName,
                Password = password
            };

            _userRepository.AddUser(newUser);
        }

        // Метод для получения всех пользователей
        public List<User> LoadAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        // Метод для получения пользователя по Id
        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        // Метод для обновления данных пользователя
        public void UpdateExistingUser(User user)
        {
            _userRepository.UpdateUser(user);
        }

        // Метод для удаления пользователя
        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }
    }
}
