using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Health.Repository.Dto;

namespace Health.Repository.Interfaces
{
    public interface INotificationInfoRepository
    {
        Task<IEnumerable<NotificationInfoDto>> SearchAsync(NotificationInfoSearchFilter filter);

        Task<int> Create(NotificationInfoDto notificationInfo);

        Task<int> Delete(NotificationInfoDto notificationInfo);

        Task<int> Update(NotificationInfoDto notificationInfo);
    }
}
