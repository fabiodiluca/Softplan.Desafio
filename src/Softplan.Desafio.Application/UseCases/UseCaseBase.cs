using AutoMapper;

namespace Softplan.Desafio.Application.UseCases
{
    public class UseCaseBase : IUseCaseBase
    {
        protected readonly IMapper _mapper;

        public UseCaseBase(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
