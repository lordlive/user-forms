using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace UsersForms
{
    // класс в котором описываются пользователи с правами (админ/юзер) типом (юзер по умочанию или нет) и их пароли
    [Serializable]
    public class UserClass
    {
        public String Name;
        bool defaultUser, adminUser; //свойства - пользователь по умолчанию / права администратора 
        public String Password;

        //конструктор по умолчанию
        public UserClass() { }
        //полный конструктор
        public UserClass(String name, string pass, bool def, bool adm)
        {
            Name = name;
            Password = pass;
            defaultUser = def;
            adminUser = adm;
            //count++;
        }
        //частичный конструктор 
        public UserClass(String name, string pass)
        {
            Name = name;
            Password = pass;
            defaultUser = adminUser = false;
        }
       
        // доступ к приватным переменным класа UserClass
        public bool dUser  
        {
            get
            {
                return defaultUser;
            }
            set            
            {
                defaultUser = value;
            }
            
        }
        public bool aUser
        { 
            get
            {
                return adminUser;
            }
            set
            {
                adminUser = value;
            }
        }
    }
}
