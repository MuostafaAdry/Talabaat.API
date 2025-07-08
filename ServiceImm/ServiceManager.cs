using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbs;
using ServiceImm;

public class ServiceManager  
{
    private readonly IMapper _mapper;
    private readonly IMainUnitOfWork _mainUnitOfWork;
    private readonly IBasketRepository _basketRepository;
    private readonly IConfiguration _configration;

    private readonly Lazy<IAuthenticationService> _LazyAuthenticationService;
    private readonly Lazy<IProductService> _LazyProductService;
    private readonly Lazy<IBasketService> _LazyBasketService;
    private readonly Lazy<IOrderServices> _LazyOrderServices;

    public ServiceManager(
        IMapper mapper,
        IMainUnitOfWork mainUnitOfWork,
        IBasketRepository basketRepository,
        IConfiguration configration,
        UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _mainUnitOfWork = mainUnitOfWork;
        _basketRepository = basketRepository;
        _configration = configration;

        _LazyAuthenticationService = new Lazy<IAuthenticationService>(
            () => new AuthenticationService(userManager, _configration, _mapper));

        _LazyProductService = new Lazy<IProductService>(
            () => new ProductService(_mapper, _mainUnitOfWork));

        _LazyBasketService = new Lazy<IBasketService>(
            () => new BasketService(_mapper, _basketRepository));

        _LazyOrderServices = new Lazy<IOrderServices>(
            () => new OrderServices(_mapper, _basketRepository, _mainUnitOfWork));
    }

    public IProductService ProductService => _LazyProductService.Value;
    public IBasketService BasketService => _LazyBasketService.Value;
    public IOrderServices OrderServices => _LazyOrderServices.Value;
    public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;
}
