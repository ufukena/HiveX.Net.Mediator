using AutoMapper;
using HiveX.Net.Mediator.Contracts.Handlers;
using HiveX.Net.TestConsole.Abstractions.Contracts.Repositories;
using HiveX.Net.TestConsole.Commands;
using HiveX.Net.TestConsole.Entites;
using HiveX.Net.TestConsole.Models;
using HiveX.Net.TestConsole.Queries;
using HiveX.Net.TestConsole.Results;


namespace HiveX.Net.TestConsole.Handlers
{


    internal class CustomerCreateCommandHandler : IRequestHandler<CustomerCreateCommand, bool>
    {
        private readonly ICustomerRepository _repositorysitory;
        private readonly IMapper _mapper;

        public CustomerCreateCommandHandler(ICustomerRepository repository, IMapper mapper)
        {
            _repositorysitory = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Customer>(request.CustomerModel);
            await _repositorysitory.Create(entity);

            return true;
        }

    }


    internal class CustomerUpdateCommandHandler : IRequestHandler<CustomerUpdateCommand, bool>
    {
        private readonly ICustomerRepository _repositorysitory;
        private readonly IMapper _mapper;

        public CustomerUpdateCommandHandler(ICustomerRepository repository, IMapper mapper)
        {
            _repositorysitory = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CustomerUpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Customer>(request.CustomerModel);
            await _repositorysitory.Update(entity);

            return true;
        }


    }


    internal class CustomerDeleteCommandHandler : IRequestHandler<CustomerDeleteCommand, bool>
    {
        private readonly ICustomerRepository _repositorysitory;

        public CustomerDeleteCommandHandler(ICustomerRepository repository, IMapper mapper)
        {
            _repositorysitory = repository;
        }

        public async Task<bool> Handle(CustomerDeleteCommand request, CancellationToken cancellationToken)
        {
            await _repositorysitory.Delete(request.CustomerModel.Id);

            // Test exception for logging behavior
            throw new Exception("Test exception for logging behavior");

            return true;
        }

    }


    internal class CustomerGetQueryHandler : IRequestHandler<CustomerGetQuery, CustomerModel?>
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerGetQueryHandler(ICustomerRepository repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        public async Task<CustomerModel?> Handle(CustomerGetQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.Get(request.Id);
            return entity == null ? null : _mapper.Map<CustomerModel>(entity);
        }

    }


    internal class CustomerGetAllQueryHandler : IRequestHandler<CustomerGetAllQuery, List<CustomerResult>>
    {
        private readonly ICustomerRepository _repositorysitory;

        public CustomerGetAllQueryHandler(ICustomerRepository repo)
        {
            _repositorysitory = repo;
        }

        public async Task<List<CustomerResult>> Handle(CustomerGetAllQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repositorysitory.GetAll();
            return entities;
        }
    }

}
