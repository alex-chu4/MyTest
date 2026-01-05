using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Health.Repository.Dto;
using Health.Repository.Interfaces;
using Health.Service.Interfaces;

namespace Health.Service.Services
{
    public class HealthService : IHealthService
    {
        private readonly IHealthCurrentRepository _HealthCurrentRepository;
        private readonly IHealthTargetRepository _HealthTargetRepository;
        private readonly IHealthHisRepository _HealthHisRepository;
        private readonly IHealthHis2Repository _HealthHis2Repository;
        private readonly IUserTokenRepository _UserTokenRepository;
        private readonly ISystemInfoRepository _SystemInfoRepository;
        private readonly IFunctionRepository _FunctionRepository;
        private readonly INotificationInfoRepository _NotificationInfoRepository;
        private readonly IVMInfoRepository _VMInfoRepository;
        private readonly IMessageHisRepository _MessageHisRepository;

        public HealthService(IHealthCurrentRepository healthCurrentRepository,
            IHealthTargetRepository healthTargetRepository,
            IHealthHisRepository healthHisRepository,
            IHealthHis2Repository healthHis2Repository,
            IUserTokenRepository userTokenRepository,
            ISystemInfoRepository systemInfoRepository,
            IFunctionRepository functionRepository,
            INotificationInfoRepository notificationInfoRepository,
            IVMInfoRepository vmInfoRepository,
            IMessageHisRepository messageHisRepository)
        {
            _HealthCurrentRepository = healthCurrentRepository;
            _HealthTargetRepository = healthTargetRepository;
            _HealthHisRepository = healthHisRepository;
            _HealthHis2Repository = healthHis2Repository;
            _UserTokenRepository = userTokenRepository;
            _SystemInfoRepository = systemInfoRepository;
            _FunctionRepository = functionRepository;
            _NotificationInfoRepository = notificationInfoRepository;
            _VMInfoRepository = vmInfoRepository;
            _MessageHisRepository = messageHisRepository;
        }

        public async Task CreateHealthTargetAsync(HealthTargetDto healthTarget)
        {
            await _HealthTargetRepository.Create(healthTarget);
        }

        public async Task DeleteHealthTargetAsync(HealthTargetDto healthTarget)
        {
            await _HealthTargetRepository.Delete(healthTarget);
        }

        public async Task UpdateHealthTargetAsync(HealthTargetDto healthTarget)
        {
            await _HealthTargetRepository.Update(healthTarget);
        }

        public async Task<IEnumerable<HealthTargetDto>> SearchHealthTargetAsync(HealthTargetSearchFilter filter)
        {
            return await _HealthTargetRepository.Search(filter);
        }

        public async Task<IEnumerable<HealthCurrentViewDto>> GetCurrentViewAsync()
        {
            return await _HealthCurrentRepository.GetCurrentView();
        }

        public async Task<IEnumerable<HealthTargetDto>> GetActiveTargetAsync()
        {
            return await _HealthTargetRepository.GetActiveTarget();
        }

        public async Task InsertOrUpdateCurrentAsync(HealthCurrentDto healthCurrent)
        {
            if (await _HealthCurrentRepository.IsExist(healthCurrent))
                await _HealthCurrentRepository.Update(healthCurrent);
            else
                await _HealthCurrentRepository.Insert(healthCurrent);
        }

        public async Task InsertHisAsync(HealthHisDto healthHis)
        {
            await _HealthHisRepository.Insert(healthHis);
        }

        public async Task<IEnumerable<HealthHisDto>> SearchHealthHisAsync(HealthHisSearchFilter filter)
        {
            return await _HealthHisRepository.Search(filter);
        }

        public async Task<IEnumerable<HealthHis2Dto>> SearchHealthHis2Async(HealthHis2SearchFilter filter)
        {
            return await _HealthHis2Repository.Search(filter);
        }

        public async Task<int> GetCurrentUsersAsync()
        {
            return await _UserTokenRepository.GetCurrentUsers();
        }

        public async Task<IEnumerable<EventDto>> GetHealthHis2OverThresholdAsync(int eventTimeInterval, float defaultThreshold)
        {
            IList<EventDto> events = new List<EventDto>();

            SystemInfoSearchFilter systemInfoSearchFilter = new SystemInfoSearchFilter() { KEY_FUNCTION = true };
            IEnumerable<SystemInfoDto> systemInfos = await _SystemInfoRepository.Search(systemInfoSearchFilter);
            HealthHis2Dto updateHealthHis2 = new HealthHis2Dto();

            foreach (SystemInfoDto systemInfo in systemInfos)
            {
                updateHealthHis2.SYSTEM_ID = systemInfo.ID;
                updateHealthHis2.THRESHOLD = systemInfo.THRESHOLD ?? defaultThreshold;
                await _HealthHis2Repository.UpdateThreshold(updateHealthHis2);
            }

            foreach (SystemInfoDto systemInfo in systemInfos)
            {
                HealthHis2SearchFilter healthHis2SearchFilter =
                    new HealthHis2SearchFilter()
                    {
                        SYSTEM_ID = systemInfo.ID,
                        START_TIME = DateTime.Now.AddSeconds(-1 * eventTimeInterval),
                        END_TIME = DateTime.Now,
                        OVER_THRESHOLD = true
                    };

                IEnumerable<HealthHis2Dto> healthHis2s = await _HealthHis2Repository.Search(healthHis2SearchFilter);

                FunctionSearchFilter functionSearchFilter =
                    new FunctionSearchFilter()
                    {
                        FUNCTION_CODES = healthHis2s.Select(p => p.FUNCTION_CODE).Distinct().ToArray()
                    };
                IDictionary<string, FunctionDto> functions = (await _FunctionRepository.Search(functionSearchFilter)).ToDictionary(f => f.FUNCTION_CODE, f => f);
                ISet<string> eventSet = new HashSet<string>();

                foreach (HealthHis2Dto healthHis2 in healthHis2s)
                {
                    string key = systemInfo.ID + "_" + healthHis2.FUNCTION_CODE + "_" + healthHis2.IPv4;
                    if (!eventSet.Contains(key))
                    {
                        eventSet.Add(key);

                        EventDto @event =
                            new EventDto()
                            {
                                SYSTEM_ID = systemInfo.ID,
                                SYSTEM_NAME = systemInfo.NAME,
                                SYSTEM_DESC = systemInfo.DESC,
                                KEY_FUNCTION = systemInfo.KEY_FUNCTION,
                                THRESHOLD = systemInfo.THRESHOLD ?? defaultThreshold,
                                FUNCTION_CODE = healthHis2.FUNCTION_CODE,
                                FUNCTION_NAME = functions[healthHis2.FUNCTION_CODE].FUNCTION_NAME,
                                IPv4 = healthHis2.IPv4
                            };

                        events.Add(@event);
                    }
                }
            }

            return events;
        }

        public async Task CreateSystemInfoAsync(SystemInfoDto systemInfo)
        {
            await _SystemInfoRepository.Create(systemInfo);
        }

        public async Task DeleteSystemInfoAsync(SystemInfoDto systemInfo)
        {
            await _SystemInfoRepository.Delete(systemInfo);
        }

        public async Task UpdateSystemInfoAsync(SystemInfoDto systemInfo)
        {
            await _SystemInfoRepository.Update(systemInfo);
        }

        public async Task<IEnumerable<SystemInfoDto>> SearchSystemInfoAsync(SystemInfoSearchFilter filter)
        {
            return await _SystemInfoRepository.Search(filter);
        }

        public async Task<IEnumerable<FunctionDto>> SearchFunctionAsync(FunctionSearchFilter filter)
        {
            return await _FunctionRepository.Search(filter);
        }

        public async Task CreateNotificationInfoAsync(NotificationInfoDto notificationInfo)
        {
            await _NotificationInfoRepository.Create(notificationInfo);
        }

        public async Task DeleteNotificationInfoAsync(NotificationInfoDto notificationInfo)
        {
            await _NotificationInfoRepository.Delete(notificationInfo);
        }

        public async Task UpdateNotificationInfoAsync(NotificationInfoDto notificationInfo)
        {
            await _NotificationInfoRepository.Update(notificationInfo);
        }

        public async Task<IEnumerable<NotificationInfoDto>> SearchNotificationInfoAsync(NotificationInfoSearchFilter filter)
        {
            return await _NotificationInfoRepository.SearchAsync(filter);
        }

        public async Task UpdateThresholdAsync(string SYSTEM_ID, float THRESHOLD)
        {
            await _SystemInfoRepository.UpdateThreshold(SYSTEM_ID, THRESHOLD);
        }

        public async Task CreateVMInfoAsync(VMInfoDto vmInfo)
        {
            await _VMInfoRepository.Create(vmInfo);
        }

        public async Task DeleteVMInfoAsync(VMInfoDto vmInfo)
        {
            await _VMInfoRepository.Delete(vmInfo);
        }

        public async Task UpdateVMInfoAsync(VMInfoDto vmInfo)
        {
            await _VMInfoRepository.Update(vmInfo);
        }

        public async Task<IEnumerable<VMInfoDto>> SearchVMInfoAsync(VMInfoSearchFilter filter)
        {
            return await _VMInfoRepository.Search(filter);
        }

        public async Task CreateMessageHisAsync(MessageHisDto messageHis)
        {
            await _MessageHisRepository.Create(messageHis);
        }

        public async Task<MessageHisDto> GetLatestMessageHisAsync(string messageType)
        {
            return await _MessageHisRepository.GetLatestMessage(messageType);
        }

        public void CreateHealthTarget(HealthTargetDto healthTarget)
        {
            CreateHealthTargetAsync(healthTarget).Wait();
        }

        public void DeleteHealthTarget(HealthTargetDto healthTarget)
        {
            DeleteHealthTargetAsync(healthTarget).Wait();
        }

        public void UpdateHealthTarget(HealthTargetDto healthTarget)
        {
            UpdateHealthTargetAsync(healthTarget).Wait();
        }

        public IEnumerable<HealthTargetDto> SearchHealthTarget(HealthTargetSearchFilter filter)
        {
            return SearchHealthTargetAsync(filter).Result;
        }

        public IEnumerable<HealthTargetDto> GetActiveTarget()
        {
            return GetActiveTargetAsync().Result;
        }

        public IEnumerable<HealthCurrentViewDto> GetCurrentView()
        {
            return GetCurrentViewAsync().Result;
        }

        public IEnumerable<EventDto> GetHealthHis2OverThreshold(int eventTimeInterval, float defaultThreshold)
        {
            return GetHealthHis2OverThresholdAsync(eventTimeInterval, defaultThreshold).Result;
        }

        public IEnumerable<HealthHisDto> SearchHealthHis(HealthHisSearchFilter filter)
        {
            return SearchHealthHisAsync(filter).Result;
        }

        public IEnumerable<HealthHis2Dto> SearchHealthHis2(HealthHis2SearchFilter filter)
        {
            return SearchHealthHis2Async(filter).Result;
        }

        public int GetCurrentUsers()
        {
            return GetCurrentUsersAsync().Result;
        }

        public void CreateSystemInfo(SystemInfoDto systemInfo)
        {
            CreateSystemInfoAsync(systemInfo).Wait();
        }

        public void DeleteSystemInfo(SystemInfoDto systemInfo)
        {
            DeleteSystemInfoAsync(systemInfo).Wait();
        }

        public void UpdateSystemInfo(SystemInfoDto systemInfo)
        {
            UpdateSystemInfoAsync(systemInfo).Wait();
        }

        public IEnumerable<SystemInfoDto> SearchSystemInfo(SystemInfoSearchFilter filter)
        {
            return SearchSystemInfoAsync(filter).Result;
        }

        public IEnumerable<FunctionDto> SearchFunction(FunctionSearchFilter filter)
        {
            return SearchFunctionAsync(filter).Result;
        }

        public void CreateNotificationInfo(NotificationInfoDto notificationInfo)
        {
            CreateNotificationInfoAsync(notificationInfo).Wait();
        }

        public void DeleteNotificationInfo(NotificationInfoDto notificationInfo)
        {
            DeleteNotificationInfoAsync(notificationInfo).Wait();
        }

        public void UpdateNotificationInfo(NotificationInfoDto notificationInfo)
        {
            UpdateNotificationInfoAsync(notificationInfo).Wait();
        }

        public IEnumerable<NotificationInfoDto> SearchNotificationInfo(NotificationInfoSearchFilter filter)
        {
            return SearchNotificationInfoAsync(filter).Result;
        }

        public void CreateVMInfo(VMInfoDto vmInfo)
        {
            CreateVMInfoAsync(vmInfo).Wait();
        }

        public void DeleteVMInfo(VMInfoDto vmInfo)
        {
            DeleteVMInfoAsync(vmInfo).Wait();
        }

        public void UpdateVMInfo(VMInfoDto vmInfo)
        {
            UpdateVMInfoAsync(vmInfo).Wait();
        }

        public IEnumerable<VMInfoDto> SearchVMInfo(VMInfoSearchFilter filter)
        {
            return SearchVMInfoAsync(filter).Result;
        }

        public void CreateMessageHis(MessageHisDto messageHis)
        {
            CreateMessageHisAsync(messageHis).Wait();
        }

        public MessageHisDto GetLatestMessageHis(string messageType)
        {
            return GetLatestMessageHisAsync(messageType).Result;
        }
    }
}
