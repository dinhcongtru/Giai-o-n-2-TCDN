using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB09.TCDN2.BL.BaseBL;
using MISA.WEB09.TCDN2.Common.Entities;
using MySqlConnector;
using System.Data;

namespace MISA.WEB09.TCDN2.Controllers
{
    [ApiController]
    public class DepartmentsController : BasesController<Department>
    {
        public DepartmentsController(IBaseBL<Department> baseBL) : base(baseBL)
        {

        }

    }
}
