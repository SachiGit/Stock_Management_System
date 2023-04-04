using StockManageDAL.User;
using StockManageDTO.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageBLL.User
{
    public class UserBLL
    {
        UserDAL _userDALObject;

        public UserBLL()
        {
            _userDALObject = new UserDAL();
        }

        public int Save(UserDTO _userDTOObject)
        {
            int _result = 0;
            if (_userDALObject.GetDuplicateForSave(_userDTOObject.UserName) <= 0)
            {
                if (_userDALObject.Save(_userDTOObject))
                {
                    _result = 1;
                }
            }
            else
            {
                _result = 2;
            }

            return _result;
        }

        public bool Update(UserDTO _userDTOObject)
        {
            bool _isupdate = _userDALObject.Update(_userDTOObject);
            return _isupdate;
        }

        public bool DeleteUserDetails(int _id)
        {
            return _userDALObject.Delete(_id);
        }

        public UserDTO LoadUserDetails(int _id)
        {
            return _userDALObject.GetUserData(_id);
        }

        public DataTable LoadUserList()
        {
            return _userDALObject.GetUserList();
        }
    }
}
