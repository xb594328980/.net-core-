using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sansunt.MicroService.Demo.Domain.CommandHandlers;
using Sansunt.Domain.Core.Bus;
using Sansunt.Domain.Core.Notifications;
using Sansunt.MicroService.Demo.Domain.Interfaces;
using Sansunt.MicroService.Demo.Domain.Models;


namespace Sansunt.MicroService.Demo.Domain.CommandHandlers
{
    public class StaffCommandHandler : CommandHandler
    {
        private Lazy<IStaffRepository> _staffRepository;
        private static object lockObj = new object();
        public StaffCommandHandler(IUnitOfWork uow, IMediatorHandler bus, IStaffRepository staffRepository) : base(uow, bus)
        {
            _staffRepository = new Lazy<IStaffRepository>(staffRepository);
        }


    }
}
