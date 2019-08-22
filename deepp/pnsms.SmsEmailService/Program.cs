using Microsoft.Practices.Unity;
using pnsms.Entities.Models;
using pnsms.Service;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pnsms.SmsEmailService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            IUnityContainer container = new UnityContainer();
            container
                .RegisterType<IDataContextAsync, PNSMSContext>()
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>()

                .RegisterType<IStoredProcedures, PNSMSContext>()
                .RegisterType<IStoredProcedureService, StoredProcedureService>()

                .RegisterType<IRepositoryAsync<ShortMessage>, Repository<ShortMessage>>()
                .RegisterType<IRepositoryAsync<ShortMessageDetail>, Repository<ShortMessageDetail>>()
                .RegisterType<IShortMessageOuterService, ShortMessageOuterService>()                
            ;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(container));
        }
    }
}
