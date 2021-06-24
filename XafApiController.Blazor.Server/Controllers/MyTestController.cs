using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DevExpress.Blazor.Internal;
using DevExpress.Data.Filtering;
using DevExpress.Data.Helpers;
using DevExpress.ExpressApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ReflectionMagic;
using XafApiController.Module.BusinessObjects;

namespace XafApiController.Blazor.Server.Controllers
{
    [JwtAuthenticationAttribute]
    [Route("api/[controller]")]
    [ApiController]
    public class MyTestController : ControllerBase
    {
        IObjectSpaceService _objectSpaceService;
        public MyTestController(IObjectSpaceService objectSpaceService)
        {
            _objectSpaceService = objectSpaceService;
        }
        [HttpGet]
        public string Get([FromQuery] string PropertiesList = "", [FromQuery] string Type = "", [FromQuery] string Criteria = "")
        {
            try
            {
                var os = _objectSpaceService.GetObjectSpace();
                var ObjectType = _objectSpaceService.Types.FirstOrDefault(t=>t.Name==Type);


                XafDataView dataView = (XafDataView)os.CreateDataView(ObjectType, PropertiesList, CriteriaOperator.Parse(Criteria), null);
                //dynamic objects = dataView[0].DataView.AsDynamic().objects.Instance;

                var expressions = dataView.Expressions;
                List<List<object>> values = new List<List<object>>();
                foreach (XafDataViewRecord xafDataViewRecord in dataView)
                {
                    List<object> properties = new List<object>();
                    foreach (var item in expressions)
                    {
                        properties.Add(xafDataViewRecord[item.Name]);
                    }
                    values.Add(properties);


                }


                var jsonString = JsonSerializer.Serialize(values);

                return jsonString;
            }
            catch (Exception)
            {

                
            }
            return "";
           
        }
      
    }
}
