using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bookshelf_assignment2.Startup))]
namespace Bookshelf_assignment2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
