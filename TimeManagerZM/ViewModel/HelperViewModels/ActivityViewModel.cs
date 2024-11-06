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

        public async Task AddNewActivity(string activityName, DateTime startTime, DateTime? endTime, int activityTypeId, int userId)
        {
            var newActivity = new MyActivity
            {
                ActivityName = activityName,
                StartTime = startTime,
                EndTime = endTime,
                ActivityTypeId = activityTypeId,
                UserId = userId
            };

            await _activityRepository.AddActivity(newActivity);
        }

        public async Task<List<MyActivity>> LoadAllActivitiesByUserId(int id)
        {
           return await _activityRepository.GetAllActivitiesByUserId(id);
        }

        public async Task<MyActivity> GetActivityById(int id)
        {
            return await _activityRepository.GetActivityById(id);
        }

        public async Task UpdateExistingActivity(MyActivity activity)
        {
            await _activityRepository.UpdateActivity(activity);
        }

        public async Task DeleteActivity(int id)
        {
            await _activityRepository.DeleteActivity(id);
        }
    }
}