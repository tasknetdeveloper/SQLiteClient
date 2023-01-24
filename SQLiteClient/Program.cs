using SecurityLayer;
using LoggerSpace;
using Microsoft.Extensions.Configuration;
using Model;

Ini();

void Ini()
{
    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    if (config != null)
    {
      var  settings = config.GetSection("SystemSettings").Get<SystemSettings>();
      if (settings!=null && !string.IsNullOrEmpty(settings.ConnectionDB))
      {
          var su = new SecurityUser(settings.ConnectionDB, new Log(settings.isLogEnable));
      }
    }    
}