using StockManageDAL.Login;
using StockManageDTO.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageBLL.Login
{
    public class LoginBLL
    {
        LoginDAL _loginDALObject = new LoginDAL();
        LoginDTO _loginDTOCommonObject = new LoginDTO();
        private string UType = string.Empty;

        public bool CheckUserAuthentication(LoginDTO _loginDTOObject)
        {
            LoginDTO _loginDTOObjectResult = _loginDALObject.GetUserAuthentication(_loginDTOObject);

            if (_loginDTOObjectResult.UserName == _loginDTOObject.UserName && _loginDTOObjectResult.Password == _loginDTOObject.Password)
            {
                _loginDTOCommonObject = _loginDTOObjectResult;
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetUserType(LoginDTO _loginDTOObject)
        {
            LoginDTO _loginDTOObjectResult = _loginDALObject.GetUserAuthentication(_loginDTOObject);

            if (_loginDTOObjectResult.UserName == _loginDTOObject.UserName && _loginDTOObjectResult.Password == _loginDTOObject.Password)
            {
                UType = _loginDTOObjectResult.UserType;
            }

            return UType;
        }
    }
}
