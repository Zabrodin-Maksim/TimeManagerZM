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

        public List<MyActivity> LoadAllActivitiesByUserId(int id)
        {
           return _activityRepository.GetAllActivitiesByUserId(id);
        }

        public MyActivity GetActivityById(int id)
        {
            return _activityRepository.GetActivityById(id);
        }

        public void UpdateExistingActivity(MyActivity activity)
        {
            _activityRepository.UpdateActivity(activity);
        }

        public void DeleteActivity(int id)
        {
            _activityRepository.DeleteActivity(id);
        }
    }
}