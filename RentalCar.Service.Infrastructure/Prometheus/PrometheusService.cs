using RentalCar.Service.Core.Service;

namespace RentalCar.Service.Infrastructure.Prometheus;

public class PrometheusService : IPrometheusService
{
    //private static readonly Counter RequestAccountCounter = Metrics.CreateCounter("account_total", "Total requisições de criação de conta", ["status_code"]);
    //private static readonly Counter RequestRoleCounter = Metrics.CreateCounter("role_total", "Total requisições de criação de perfil", ["status_code"]);
    //private static readonly Counter RequestLoginCounter = Metrics.CreateCounter("login_total", "Total requisições de login (acesso dos utilizadores)", ["status_code"]);
    //private static readonly Counter RequestLoginCounter = Metrics.CreateCounter("login_total", "Total requisições de login (acesso dos utilizadores)", ["status_code"]);
    
    

    public void AddNewServiceCounter(string statusCodes)
    {
        System.Diagnostics.Debug.Print(statusCodes);
    }

    public void AddDeleteServiceCounter(string statusCodes)
    {
        System.Diagnostics.Debug.Print(statusCodes);
    }

    public void AddUpdateServiceCounter(string statusCodes)
    {
        System.Diagnostics.Debug.Print(statusCodes);
    }

    public void AddUpdateStatusServiceCounter(string statusCodes)
    {
        System.Diagnostics.Debug.Print(statusCodes);
    }

    public void AddFindByIdServiceCounter(string statusCodes)
    {
        System.Diagnostics.Debug.Print(statusCodes);
    }

    public void AddFindAllServicesCounter(string statusCodes)
    {
        System.Diagnostics.Debug.Print(statusCodes);
    }
    
}