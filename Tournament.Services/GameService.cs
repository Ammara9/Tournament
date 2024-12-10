using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Services.Constracts;
using Tournament.Core.Contracts;

namespace Tournament.Services
{
    public class GameService : IGameService
    {
        private IUnitOfWork uow;
        private readonly IMapper mapper;

        public GameService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
    }
}
