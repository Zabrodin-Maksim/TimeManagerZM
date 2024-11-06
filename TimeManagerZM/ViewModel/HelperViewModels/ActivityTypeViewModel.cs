using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagerZM.Data;
using TimeManagerZM.Model;

namespace TimeManagerZM.ViewModel.HelperViewModels
{
    public class ActivityTypeViewModel
    {
        private ActivityTypeRepository _activityTypeRepository;

        public ActivityTypeViewModel()
        {
            _activityTypeRepository = new ActivityTypeRepository();
        }

        public async Task AddNewActivityType(string typeName, string colorAct, int userId)
        {
            var newActivityType = new ActivityType
            {
                TypeName = typeName,
                ColoActr = colorAct,
                UserId = userId
            };

            await _activityTypeRepository.AddActivityType(newActivityType);
        }
  
        public async Task<List<ActivityType>> GetAllActivitiesByUserId(int id)
        {
            return await _activityTypeRepository.GetAllActivityTypesByUserId(id);
        }

        public async Task UpdateExistingActivityType(ActivityType activityType)
        {
            await _activityTypeRepository.UpdateActivityType(activityType);
        }

        public async Task DeleteActivityType(int id)
        {
            await _activityTypeRepository.DeleteActivityType(id);
        }
    }
}
