using AutoMapper;
using Sansunt.MicroService.Demo.Application.Interfaces;
using Sansunt.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sansunt.MicroService.Demo.Application.ResponseModels;
using Sansunt.Infra.Tools.Maps;
using Sansunt.MicroService.Demo.Domain.Interfaces;

namespace Sansunt.MicroService.Demo.Application.Services
{
    /// <summary>
    /// 组织机构服务实现
    /// <remarks>create by xingbo 18/12/19</remarks>
    /// </summary>
    public class StaffAppService : IUserAppService
    {
        #region MyRegion

        // 注入仓储接口
        private readonly IStaffRepository _staffRepository;
        //用来进行DTO
        private readonly IMapper _mapper;
        //中介者 总线
        private readonly IMediatorHandler _bus;
        public StaffAppService(
            IStaffRepository staffRepository,
            IMapper mapper,
            IMediatorHandler bus)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
            _bus = bus;
        }
        public void Dispose()
        {
            _staffRepository.Dispose();
        }
        #endregion

    }
}
