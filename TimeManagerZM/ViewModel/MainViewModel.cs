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
        private UserViewModel _userVM;
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

        // User
        public ObservableCollection<User> Users { get; set; }

        // ActivityType

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

            // Init Lists
            Activities = new ObservableCollection<MyActivity>();
            Users = new ObservableCollection<User>();

            // Init Commands
            AddActivityCommand = new MyICommand(AddNewActivity);
            LoadActivitiesCommand = new MyICommand(AddNewActivity);

            LoadAllActivities();
            LoadAllUsers();
        }

        //TODO Дописать методы для работы с базой данных

        #region Activity Data Methods

        private void AddNewActivity()
        {
            try
            {
                _activityVM.AddNewActivity(NewActivityName, NewActivityStartTime, NewActivityEndTime, NewActivityTypeId, NewActivityUserId);
                Debug.WriteLine($"[INFO] Добавлена новая активность: {NewActivityName}, начало: {NewActivityStartTime}, тип: {NewActivityTypeId}, пользователь: {NewActivityUserId}");
                LoadAllActivities(); // Обновление списка активностей после добавления новой
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении активности: {ex.Message}");
            }
        }

        private void LoadAllActivities()
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

        #region User Data Methods

       

        private void LoadAllUsers()
        {
            try
            {
                Users.Clear();
                var allUsers = _userVM.LoadAllUsers();
                foreach (var activity in allUsers)
                {
                    Users.Add(activity);
                }
                Debug.WriteLine($"[INFO] Загружено {Users.Count} Юзеров");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при загрузке Юзеров: {ex.Message}");
            }
        }


        #endregion
    }
}
