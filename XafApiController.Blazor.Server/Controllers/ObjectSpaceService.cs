using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XafApiController.Blazor.Server.Controllers
{
    public interface IObjectSpaceService
    {
        IObjectSpace GetObjectSpace();
        IEnumerable<Type> Types { get; }
    }
    public class ObjectSpaceService : IObjectSpaceService
    {
        public  IEnumerable<Type> Types { get; }
        XPObjectSpaceProvider osProvider;
        public ObjectSpaceService(IEnumerable<Type> Types, string Cnx)
        {
            this.Types = Types;
            XpoTypesInfoHelper.GetXpoTypeInfoSource();
            foreach (Type type in Types)
            {
                XafTypesInfo.Instance.RegisterEntity(type);
            }

            osProvider = new XPObjectSpaceProvider(Cnx, null);

        }
        public IObjectSpace GetObjectSpace()
        {
            return osProvider.CreateObjectSpace();
        }
    }
}
