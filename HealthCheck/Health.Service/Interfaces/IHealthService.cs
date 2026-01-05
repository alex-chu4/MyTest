using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Health.Repository.Dto;

namespace Health.Service.Interfaces
{
    public interface IHealthService
    {
        Task CreateHealthTargetAsync(HealthTargetDto healthTarget);

        Task DeleteHealthTargetAsync(HealthTargetDto healthTarget);

        Task UpdateHealthTargetAsync(HealthTargetDto healthTarget);

        Task<IEnumerable<HealthTargetDto>> SearchHealthTargetAsync(HealthTargetSearchFilter filter);

        Task<IEnumerable<HealthTargetDto>> GetActiveTargetAsync();

        Task<IEnumerable<HealthCurrentViewDto>> GetCurrentViewAsync();

        Task InsertOrUpdateCurrentAsync(HealthCurrentDto healthCurrent);

        Task InsertHisAsync(HealthHisDto healthHis);

        Task<IEnumerable<HealthHisDto>> SearchHealthHisAsync(HealthHisSearchFilter filter);

        Task<IEnumerable<HealthHis2Dto>> SearchHealthHis2Async(HealthHis2SearchFilter filter);

        Task<int> GetCurrentUsersAsync();

        Task<IEnumerable<EventDto>> GetHealthHis2OverThresholdAsync(int eventTimeInterval, float defaultThreshold);

        Task CreateSystemInfoAsync(SystemInfoDto systemInfo);

        Task DeleteSystemInfoAsync(SystemInfoDto systemInfo);

        Task UpdateSystemInfoAsync(SystemInfoDto systemInfo);

        Task<IEnumerable<SystemInfoDto>> SearchSystemInfoAsync(SystemInfoSearchFilter filter);

        Task<IEnumerable<FunctionDto>> SearchFunctionAsync(FunctionSearchFilter filter);

        Task CreateNotificationInfoAsync(NotificationInfoDto notificationInfo);

        Task DeleteNotificationInfoAsync(NotificationInfoDto notificationInfo);

        Task UpdateNotificationInfoAsync(NotificationInfoDto notificationInfo);

        Task<IEnumerable<NotificationInfoDto>> SearchNotificationInfoAsync(NotificationInfoSearchFilter filter);

        Task UpdateThresholdAsync(string SYSTEM_ID, float THRESHOLD);

        Task CreateVMInfoAsync(VMInfoDto vmInfo);

        Task DeleteVMInfoAsync(VMInfoDto vmInfo);

        Task UpdateVMInfoAsync(VMInfoDto vmInfo);

        Task<IEnumerable<VMInfoDto>> SearchVMInfoAsync(VMInfoSearchFilter filter);

        Task CreateMessageHisAsync(MessageHisDto messageHis);

        Task<MessageHisDto> GetLatestMessageHisAsync(string messageType);

        void CreateHealthTarget(HealthTargetDto healthTarget);

        void DeleteHealthTarget(HealthTargetDto healthTarget);

        void UpdateHealthTarget(HealthTargetDto healthTarget);

        IEnumerable<HealthTargetDto> SearchHealthTarget(HealthTargetSearchFilter filter);

        IEnumerable<HealthTargetDto> GetActiveTarget();

        IEnumerable<HealthCurrentViewDto> GetCurrentView();

        IEnumerable<HealthHisDto> SearchHealthHis(HealthHisSearchFilter filter);

        IEnumerable<HealthHis2Dto> SearchHealthHis2(HealthHis2SearchFilter filter);

        int GetCurrentUsers();

        IEnumerable<EventDto> GetHealthHis2OverThreshold(int eventTimeInterval, float defaultThreshold);

        void CreateSystemInfo(SystemInfoDto systemInfo);

        void DeleteSystemInfo(SystemInfoDto systemInfo);

        void UpdateSystemInfo(SystemInfoDto systemInfo);

        IEnumerable<SystemInfoDto> SearchSystemInfo(SystemInfoSearchFilter filter);

        void CreateNotificationInfo(NotificationInfoDto notificationInfo);

        void DeleteNotificationInfo(NotificationInfoDto notificationInfo);

        void UpdateNotificationInfo(NotificationInfoDto notificationInfo);

        IEnumerable<NotificationInfoDto> SearchNotificationInfo(NotificationInfoSearchFilter filter);

        IEnumerable<FunctionDto> SearchFunction(FunctionSearchFilter filter);

        void CreateVMInfo(VMInfoDto vmInfo);

        void DeleteVMInfo(VMInfoDto vmInfo);

        void UpdateVMInfo(VMInfoDto vmInfo);

        IEnumerable<VMInfoDto> SearchVMInfo(VMInfoSearchFilter filter);

        void CreateMessageHis(MessageHisDto messageHis);

        MessageHisDto GetLatestMessageHis(string messageType);
    }
}
