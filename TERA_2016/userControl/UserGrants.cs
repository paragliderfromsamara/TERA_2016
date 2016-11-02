using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TERA_2016
{
    class UserGrants
    {
        string role = "undefined";
        public  UserGrants(string usrRole)
        {
            this.role = usrRole;
        }

        /// <summary>
        /// Может ли пользователь смотреть таблицу пользователей
        /// </summary>
        /// <returns></returns>
        public bool userCouldSeeUserDb() //может ли видеть БД Пользователей
        {
            return (role == "Администратор БД" || role == "Метролог");
        }

    }
}
