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

        public void AddNewActivityType(string typeName, string colorAct, int userId)
        {
            var newActivityType = new ActivityType
            {
                TypeName = typeName,
                ColoActr = colorAct,
                UserId = userId
            };

            _activityTypeRepository.AddActivityType(newActivityType);
        }
  
        public List<ActivityType> GetAllActivitiesByUserId(int id)
        {
            return _activityTypeRepository.GetAllActivityTypesByUserId(id);
        }

        public void UpdateExistingActivityType(ActivityType activityType)
        {
            _activityTypeRepository.UpdateActivityType(activityType);
        }

        public void DeleteActivityType(int id)
        {
            _activityTypeRepository.DeleteActivityType(id);
        }
    }
}
