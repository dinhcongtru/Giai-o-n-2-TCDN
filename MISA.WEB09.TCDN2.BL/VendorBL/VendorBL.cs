using MISA.WEB09.TCDN2.Common.Entities;
using MISA.WEB09.TCDN2.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB09.TCDN2.BL
{
    public class VendorBL : BaseBL<Vendor>, IVendorBL
    {
        #region Field

        private IVendorDL _vendorDL;
        #endregion

        #region Constructor
        public VendorBL(IVendorDL vendorDL) : base(vendorDL)
        {
            _vendorDL = vendorDL;
        } 
        #endregion
    }
}
