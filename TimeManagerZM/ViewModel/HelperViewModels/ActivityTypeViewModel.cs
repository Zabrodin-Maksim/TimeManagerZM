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

        // Метод для добавления нового типа активности
        public void AddNewActivityType(string typeName, int userId)
        {
            var newActivityType = new ActivityType
            {
                TypeName = typeName,
                UserId = userId
            };

            _activityTypeRepository.AddActivityType(newActivityType);
        }

        // Метод для получения всех типов активности
        public List<ActivityType> LoadAllActivityTypes()
        {
            return _activityTypeRepository.GetAllActivityTypes();
        }

        // Метод для получения типа активности по Id
        public ActivityType GetActivityTypeById(int id)
        {
            return _activityTypeRepository.GetActivityTypeById(id);
        }

        // Метод для обновления существующего типа активности
        public void UpdateExistingActivityType(ActivityType activityType)
        {
            _activityTypeRepository.UpdateActivityType(activityType);
        }

        // Метод для удаления типа активности
        public void DeleteActivityType(int id)
        {
            _activityTypeRepository.DeleteActivityType(id);
        }
    }
}
