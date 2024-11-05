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
using System.Windows;

namespace TimeManagerZM.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Private Fields
        // Menu Visibility
        private Visibility _isMenuVisibility;

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
        // Menu Visibility
        public Visibility IsMenuVisible { get => _isMenuVisibility; set { SetProperty(ref _isMenuVisibility, value, nameof(IsMenuVisible)); } }

        // Navigation
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value, nameof(CurrentViewModel));
        }

        // Activity
        public ObservableCollection<MyActivity> Activities { get; set; }

        // ActivityType
        public ObservableCollection<ActivityType> ActivityTypes { get; set; }

        // User
        public User AuthorizedUser { get => _authorizedUser; private set => _authorizedUser = value; }

        #endregion

        #region Commands
        // Navigation
        public ICommand NavigateToCreateActivityCommand { get; }
        public ICommand NavigateToAuthCommand { get; }

        // Menu
        public ICommand VisibilityMenu {  get; }


        #endregion

        public MainViewModel()
        {
            #region Navigation

            _navigationService = new MyNavigationService(this);

            // Navigation Commands
            NavigateToAuthCommand = new MyICommand(() => _navigationService.Navigate(ViewType.Authorization));
            NavigateToCreateActivityCommand = new MyICommand(() => _navigationService.Navigate(ViewType.CreateActivity));

            // First Page
            NavigateToAuthCommand.Execute(null);
            #endregion

            // Init Commands
            VisibilityMenu = new MyICommand(ChangeMenuVisibility);

            // Init DataViewModels
            _activityVM = new ActivityViewModel();
            _userVM = new UserViewModel();
            _activityTypeVM = new ActivityTypeViewModel();

            // Init Lists
            Activities = new ObservableCollection<MyActivity>();
            ActivityTypes = new ObservableCollection<ActivityType>();

            // Menu Visibility
            IsMenuVisible = Visibility.Visible;
        }


        #region Activity Data Methods
        public void AddNewActivity(string activityName, DateTime startTime, DateTime? endTime, int activityTypeId)
        {
            if (AuthorizedUser != null)
            {
                try
                {
                    _activityVM.AddNewActivity(activityName, startTime, endTime, activityTypeId, AuthorizedUser.Id);
                    Debug.WriteLine($"[INFO] New activity added: {activityName}, start time: {startTime}, type: {activityTypeId}, user: {AuthorizedUser.Id}");
                    LoadAllActivitiesByUserId();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding activity: {ex.Message}");
                }
            }
            else MassageWindow(MessageBoxImage.Error, "Not authorized user!", "HO IS THIS?");
        }

        public void LoadAllActivitiesByUserId()
        {
            if (AuthorizedUser != null)
            {
                try
                {
                    Activities.Clear();
                    var allActivities = _activityVM.LoadAllActivitiesByUserId(AuthorizedUser.Id);
                    foreach (var activity in allActivities)
                    {
                        Activities.Add(activity);
                    }
                    Debug.WriteLine($"[INFO] {Activities.Count} activities loaded");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error loading activities: {ex.Message}");
                }
            }
            else MassageWindow(MessageBoxImage.Error, "Not authorized user!", "HO IS THIS?");
        }

        public void UpdateExistingActivity(MyActivity activity)
        {
            try
            {
                _activityVM.UpdateExistingActivity(activity);
                Debug.WriteLine($"Activity with ID {activity.Id} updated");
                
                LoadAllActivitiesByUserId();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating activity: {ex.Message}");
            }
        }

        public void DeleteActivity(int id)
        {
            try
            {
                _activityVM.DeleteActivity(id);
                Debug.WriteLine($"Activity with ID {id} deleted");
                LoadAllActivitiesByUserId();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting activity: {ex.Message}");
            }
        }

        #endregion


        #region ActivityType Data Methods

        public void AddNewActivityType(string typeName, string colorAct)
        {
            if (AuthorizedUser != null)
            {
                try
                {
                    _activityTypeVM.AddNewActivityType(typeName, colorAct, AuthorizedUser.Id);
                    Debug.WriteLine($"[INFO] New activity type added: {typeName}, user: {AuthorizedUser.Id}");
                    LoadAllActivityTypes();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error adding activity type: {ex.Message}");
                }
            }
            else MassageWindow(MessageBoxImage.Error, "Not authorized user!", "HO IS THIS?");
        }

        public void LoadAllActivityTypes()
        {
            if (AuthorizedUser != null)
            {
                try
                {
                    ActivityTypes.Clear();
                    var allActivityTypes = _activityTypeVM.GetAllActivitiesByUserId(AuthorizedUser.Id);
                    foreach (var activityType in allActivityTypes)
                    {
                        ActivityTypes.Add(activityType);
                    }
                    Debug.WriteLine($"[INFO] {ActivityTypes.Count} activity types loaded");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error loading activity types: {ex.Message}");
                }
            }
            else MassageWindow(MessageBoxImage.Error, "Not authorized user!", "HO IS THIS?");
        }

        public void UpdateExistingActivityType(ActivityType activityType)
        {
            try
            {
                _activityTypeVM.UpdateExistingActivityType(activityType);
                Debug.WriteLine($"Activity type with ID {activityType.Id} updated");
                LoadAllActivityTypes();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating activity type: {ex.Message}");
            }
        }

        public void DeleteActivityType(int id)
        {
            try
            {
                _activityTypeVM.DeleteActivityType(id);
                Debug.WriteLine($"Activity type with ID {id} deleted");
                LoadAllActivityTypes();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting activity type: {ex.Message}");
            }
        }

        #endregion


        #region User Data Methods

        public void AddNewUser(string username, string password)
        {
            try
            {
                _userVM.AddNewUser(username, password);
                Debug.WriteLine($"[INFO] New user added: {username}");
                //LoadAllUsers(); // Обновление списка пользователей после добавления нового
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding user: {ex.Message}");
            }
        }

        public bool GetUser(string username, string password)
        {
            try
            {
                AuthorizedUser = _userVM.GetUserByNameAndPassword(username, password);
                Debug.WriteLine($"[INFO] User {username} loaded");
                if (AuthorizedUser != null) { return true; }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading user: {ex.Message}");
            }
            return false;
        }

        public void UpdateExistingUser(User user)
        {
            try
            {
                _userVM.UpdateExistingUser(user);
                Debug.WriteLine($"User with ID {user.Id} updated");
                //LoadAllUsers();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating user: {ex.Message}");
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                _userVM.DeleteUser(id);
                Debug.WriteLine($"User with ID {id} deleted");
                //LoadAllUsers();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting user: {ex.Message}");
            }
        }
        #endregion


        public void ChangeMenuVisibility()
        {
            if (IsMenuVisible == Visibility.Visible)
            {
                IsMenuVisible = Visibility.Collapsed;
            }
            else { IsMenuVisible = Visibility.Visible; }
        }

        public void MassageWindow(MessageBoxImage messageBoxImage, string textMessage, string title)
        {
            MessageBox.Show(textMessage, title, MessageBoxButton.OK, messageBoxImage);
        }
    }
}
