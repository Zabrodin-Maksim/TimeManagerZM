using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagerZM.Data;
using TimeManagerZM.Model;

namespace TimeManagerZM.ViewModel
{
    public class ActivityViewModel
    {
        private ActivityRepository _activityRepository;

        public ActivityViewModel()
        {
            _activityRepository = new ActivityRepository();
        }

        // Метод для добавления новой активности
        public void AddNewActivity(string activityName, DateTime startTime, DateTime? endTime, int activityTypeId, int userId)
        {
            var newActivity = new MyActivity
            {
                ActivityName = activityName,
                StartTime = startTime,
                EndTime = endTime,
                ActivityTypeId = activityTypeId,
                UserId = userId
            };

            _activityRepository.AddActivity(newActivity);
        }

        // Метод для получения всех активностей
        public List<MyActivity> LoadAllActivities()
        {
            return _activityRepository.GetAllActivities();
        }

        // Метод для получения активности по её Id
        public MyActivity GetActivityById(int id)
        {
            return _activityRepository.GetActivityById(id);
        }

        // Метод для обновления существующей активности
        public void UpdateExistingActivity(MyActivity activity)
        {
            _activityRepository.UpdateActivity(activity);
        }

        // Метод для удаления активности
        public void DeleteActivity(int id)
        {
            _activityRepository.DeleteActivity(id);
        }
    }
}