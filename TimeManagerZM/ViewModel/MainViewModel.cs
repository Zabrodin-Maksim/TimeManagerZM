using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagerZM.Enums;
using TimeManagerZM.Services;
using TimeManagerZM.Model;
using System.Windows.Input;
using TimeManagerZM.View;
using System.Diagnostics;
using TimeManagerZM.ViewModel.HelperViewModels;

namespace TimeManagerZM.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Private Fields
        // Navigation
        private readonly MyNavigationService _navigationService;
        private ViewModelBase _currentViewModel;

        // Data
        private ActivityViewModel _activityVM;
        private ActivityTypeViewModel _activityTypeVM;
        private UserViewModel _userVM;

        private User _authorizedUser;
        #endregion

        #region Properties
        // Navigation
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value, nameof(CurrentViewModel));
        }

        // Activity
        public ObservableCollection<MyActivity> Activities { get; set; }
        public string NewActivityName { get; set; }
        public DateTime NewActivityStartTime { get; set; } = DateTime.Now;
        public DateTime? NewActivityEndTime { get; set; }
        public int NewActivityTypeId { get; set; }
        public int NewActivityUserId { get; set; }

        // ActivityType
        public ObservableCollection<ActivityType> ActivityTypes { get; set; }

        // User
        public ObservableCollection<User> Users { get; set; }
        public User AuthorizedUser { get => _authorizedUser; set => _authorizedUser = value; }

        #endregion

        #region Commands
        // Navigation
        public ICommand NavigateToDashboardCommand { get; }
        public ICommand NavigateToAuthCommand { get; }

        // Activty
        public ICommand AddActivityCommand { get; }
        public ICommand LoadActivitiesCommand { get; }
        #endregion

        public MainViewModel()
        {
            #region Navigation

            _navigationService = new MyNavigationService(this);

            // Navigation Commands
            NavigateToAuthCommand = new MyICommand(() => _navigationService.Navigate(ViewType.Authorization));

            // First Page
            _navigationService.Navigate(ViewType.Authorization);
            #endregion

            // Init DataViewModels
            _activityVM = new ActivityViewModel();
            _userVM = new UserViewModel();
            _activityTypeVM = new ActivityTypeViewModel();

            // Init Lists
            Activities = new ObservableCollection<MyActivity>();
            ActivityTypes = new ObservableCollection<ActivityType>();
            Users = new ObservableCollection<User>();

            // Init Commands
            AddActivityCommand = new MyICommand(AddNewActivity);
            LoadActivitiesCommand = new MyICommand(LoadAllActivities);


            LoadAllActivities();
            LoadAllUsers();
            LoadAllActivityTypes();
        }

        //TODO 1. поменять debug in english, реализовать метод из репозитория GetUserByNameAndPassword, продумать над реализацией авторитизации (загрузка данных)
        //TODO 2. Добавить методы нахождения данных по юзеру (ActivityRepository, ActivityTypeRepository)
        //TODO 3. В Authorization реализовать Login()
        #region Activity Data Methods
        public void AddNewActivity()
        {
            try
            {
                _activityVM.AddNewActivity(NewActivityName, NewActivityStartTime, NewActivityEndTime, NewActivityTypeId, NewActivityUserId);
                Debug.WriteLine($"[INFO] Добавлена новая активность: {NewActivityName}, начало: {NewActivityStartTime}, тип: {NewActivityTypeId}, пользователь: {NewActivityUserId}");
                LoadAllActivities(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении активности: {ex.Message}");
            }
        }

        public void LoadAllActivities()
        {
            try
            {
                Activities.Clear();
                var allActivities = _activityVM.LoadAllActivities();
                foreach (var activity in allActivities)
                {
                    Activities.Add(activity);
                }
                Debug.WriteLine($"[INFO] Загружено {Activities.Count} активностей");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при загрузке активностей: {ex.Message}");
            }
        }

        public void UpdateExistingActivity(MyActivity activity)
        {
            try
            {
                _activityVM.UpdateExistingActivity(activity);
                Debug.WriteLine($"Обновлена активность с ID {activity.Id}");
                LoadAllActivities();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при обновлении активности: {ex.Message}");
            }
        }

        public void DeleteActivity(int id)
        {
            try
            {
                _activityVM.DeleteActivity(id);
                Debug.WriteLine($"Удалена активность с ID {id}");
                LoadAllActivities();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при удалении активности: {ex.Message}");
            }
        }

        #endregion


        #region ActivityType Data Methods

        public void AddNewActivityType(string typeName, int userId)
        {
            try
            {
                _activityTypeVM.AddNewActivityType(typeName, userId);
                Debug.WriteLine($"[INFO] Добавлен новый тип активности: {typeName}, пользователь: {userId}");
                LoadAllActivityTypes(); 
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при добавлении типа активности: {ex.Message}");
            }
        }

        public void LoadAllActivityTypes()
        {
            try
            {
                ActivityTypes.Clear();
                var allActivityTypes = _activityTypeVM.LoadAllActivityTypes();
                foreach (var activityType in allActivityTypes)
                {
                    ActivityTypes.Add(activityType);
                }
                Debug.WriteLine($"[INFO] Загружено {ActivityTypes.Count} типов активностей");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при загрузке типов активностей: {ex.Message}");
            }
        }

        public void UpdateExistingActivityType(ActivityType activityType)
        {
            try
            {
                _activityTypeVM.UpdateExistingActivityType(activityType);
                Debug.WriteLine($"Обновлен тип активности с ID {activityType.Id}");
                LoadAllActivityTypes();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при обновлении типа активности: {ex.Message}");
            }
        }

        public void DeleteActivityType(int id)
        {
            try
            {
                _activityTypeVM.DeleteActivityType(id);
                Debug.WriteLine($"Удален тип активности с ID {id}");
                LoadAllActivityTypes();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при удалении типа активности: {ex.Message}");
            }
        }

        #endregion


        #region User Data Methods

        public void AddNewUser(string userName, string password)
        {
            try
            {
                _userVM.AddNewUser(userName, password);
                Debug.WriteLine($"[INFO] Добавлен новый пользователь: {userName}");
                LoadAllUsers(); // Обновление списка пользователей после добавления нового
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при добавлении пользователя: {ex.Message}");
            }
        }

        public void LoadAllUsers()
        {
            try
            {
                Users.Clear();
                var allUsers = _userVM.LoadAllUsers();
                foreach (var user in allUsers)
                {
                    Users.Add(user);
                    Debug.WriteLine($"[INFO] Загружено {Users.Count} пользователей {Users[Users.Count - 1].UserName}");
                }
                Debug.WriteLine($"[INFO] Загружено {Users.Count} пользователей ");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при загрузке пользователей: {ex.Message}");
            }
        }

        public void UpdateExistingUser(User user)
        {
            try
            {
                _userVM.UpdateExistingUser(user);
                Debug.WriteLine($"Обновлен пользователь с ID {user.Id}");
                LoadAllUsers();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при обновлении пользователя: {ex.Message}");
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                _userVM.DeleteUser(id);
                Debug.WriteLine($"Удален пользователь с ID {id}");
                LoadAllUsers();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при удалении пользователя: {ex.Message}");
            }
        }



        #endregion

    }
}
